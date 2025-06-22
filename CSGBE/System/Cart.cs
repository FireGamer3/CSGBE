using CSGBE.System.Extra;
using CSGBE.System.Mappers;
using CSGBE.System.Mappers.MBC;
using CSGBE.System.Mappers.None;

namespace CSGBE.System {
    internal class Cart {
        public string RomFilePath { get; private set; }
        public byte[] RomData { get; private set; }

        private IMapper mapper;

        public Cart(string romFilePath) {
            RomFilePath = romFilePath;
            RomData = LoadRom();
            if (RomData.Length == 0) {
                throw new InvalidOperationException("ROM data is empty.");
            }
            try {
                mapper = InitializeMapper();
            } catch (Exception) {
                throw;
            }
        }

        private byte[] LoadRom() {
            if (string.IsNullOrEmpty(RomFilePath)) {
                throw new ArgumentException("ROM file path cannot be null or empty.");
            }
            try {
                return File.ReadAllBytes(RomFilePath);
            } catch (Exception ex) {
                throw new IOException($"Failed to load ROM from '{RomFilePath}': {ex.Message}", ex);
            }
        }

        private IMapper InitializeMapper() {
            switch(RomData[0x0147]) {
                case 0x00:
                    return new NoneMapper(); // ROM only, no RAM
                case 0x08:
                    return new RamMapper(); // ROM with RAM
                case 0x01:
                    return new MBC1(RomData[0x0148]); // MBC1, no RAM
                case 0x02:
                    return new MBC1Ram(RomData[0x0148]); // MBC1 with RAM
                default:
                    throw new NotSupportedException($"Mapper type {RomData[0x0147]:X2} is not supported.");
            }
        }

        public byte Read(ushort address) {
            return mapper.Read(RomData, address);  
        }

        public void Write(ushort address, byte data) {
            mapper.Write(RomData, address, data);
        }

        public byte RAMRead(ushort address) {
            return mapper.RAMRead(address);
        }

        public void RAMWrite(ushort address, byte data) {
            mapper.RAMWrite(address, data);
        }

        public string GetRomName() {
            string s = "";
            for (int i = 308; i <= 323; i++) {
                s += (char)RomData[i];
            }
            return s.TrimEnd('\0'); // Trim any null characters at the end
        }

        public string GetLicensee() {
            byte oldLicensee = RomData[0x014B];
            byte newLicenseeA = RomData[0x0144];
            byte newLicenseeB = RomData[0x0145];
            if (oldLicensee == 0x33) {
                return CartUtils.NewLicenseeCode(newLicenseeA, newLicenseeB);
            } else {
                return CartUtils.OldLicenseeCode(oldLicensee);
            }
        }

        public bool Checksum() {
            byte checksum = 0;
            for (ushort address = 0x0134; address <= 0x014C; address++) {
                checksum = (byte)(checksum - RomData[address] - 1);
            }
            return checksum == RomData[0x014D];
        }

        public string GetRomType() {
            return CartUtils.DecodeCartType(RomData[0x0147]);
        }

        public int GetFullRomSize() {
            return RomData.Length;
        }

        public string GetCartRomSize() {
            return CartUtils.DecodeRomSize(RomData[0x0148]);
        }
    }
}
