using CSGBE.System.Extra;
using CSGBE.System.MEM;

namespace CSGBE.System.Mappers.MBC {
    internal class MBC1Ram : MBC1, IMapper {
        byte ramSizeByte;
        StatefulMemory ram;
        byte ramBank = 0;

        public MBC1Ram(byte cartSizeByte, byte ramSizeByte) : base(cartSizeByte) {
            this.ramSizeByte = ramSizeByte;
            ram = new StatefulMemory(CartUtils.GetRamSize(cartSizeByte));
        }

        public override void Write(byte[] rom, ushort address, byte data) {
            if (address <= 0x1FFF) { // RAM Enable
                ram.Enabled = (data & 0x0F) == 0x0A; // Enable RAM if data is 0x0A, otherwise disable it
            } else if (address >= 0x4000 && address <= 0x5FFF) { // RAM Bank Number
                if(ramSizeByte != 0x03) { // Treat as normal MBC1 if not 4 banks of RAM
                    base.Write(rom, address, data);
                }else {
                    ramBank = (byte)(data & 0x03);
                }
            } else base.Write(rom, address, data);
        }

        public new byte RAMRead(ushort address) {
            if (address < 0xA000 || address > 0xBFFF) return 0xFF;
            if(!bankingMode) return ram.Read((ushort)(address - 0xA000));
            else return ram.Read((ushort)(address - 0xA000 + (ramBank << 13)));
        }

        public new void RAMWrite(ushort address, byte data) {
            if (address < 0xA000 || address > 0xBFFF) return;
            if (!bankingMode) ram.Write((ushort)(address - 0xA000), data);
            else ram.Write((ushort)(address - 0xA000 + (ramBank << 13)), data);
        }

        protected ushort RamInitializationSize(byte cartSizeByte) {
            if (cartSizeByte <= 0x04) return 0x8000; // 32 KB RAM
            else return 0x2000; // 8 KB RAM
        }
    }
}
