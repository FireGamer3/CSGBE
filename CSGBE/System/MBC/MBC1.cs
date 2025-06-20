using CSGBE.System.Extra;

namespace CSGBE.System.MBC {
    internal class MBC1 : IMapper {
        byte cartSizeByte; // Size code of the cartridge
        byte romBank = 0; // Current ROM bank
        byte highBank = 0; // High bank for MBC1M
        byte[]? ram;
        bool ramPresent;
        bool ramEnabled = false;
            
        public MBC1(bool ramPresent, byte cartSizeByte) {
            this.ramPresent = ramPresent;
            this.cartSizeByte = cartSizeByte;

            if (ramPresent) {
                ram = new byte[RamInitializationSize(cartSizeByte)];
            }
        }

        public byte Read(byte[] rom, ushort address) {
            throw new NotImplementedException();
        }

        public void Write(byte[] rom, ushort address, byte data) {
            if(address <= 0x1FFF) { // RAM Enable
                ramEnabled = ((data & 0x0F) == 0x0A) && ramPresent; // Enable RAM if data is 0x0A, otherwise disable it
            } if(address >= 0x2000 && address <= 0x3FFF) { // ROM Bank Number
                romBank = (byte)(data & CartUtils.BankBitMask(cartSizeByte));
            } else if (address >= 0x4000 && address <= 0x5FFF) { // RAM Bank Number (if present)
                
            } else if (address >= 0x6000 && address <= 0x7FFF) { // Mode Select
                
            }
        }

        public byte RAMRead(ushort address) {
            if (!ramEnabled || !ramPresent || ram == null) return 0xFF;
            ushort ramAddress = (ushort)(address - 0xA000);
            return ram[ramAddress];
        }

        public void RAMWrite(ushort address, byte data) {
            if (!ramEnabled || !ramPresent || ram == null) return;
            ushort ramAddress = (ushort)(address - 0xA000);
            ram[ramAddress] = data;
        }

        private ushort RamInitializationSize(byte cartSizeByte) {
            if (cartSizeByte <= 0x04) return 0x8000; // 32 KB RAM
            else return 0x2000; // 8 KB RAM
        }
    }
}
