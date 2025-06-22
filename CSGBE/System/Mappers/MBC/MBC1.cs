using CSGBE.System.Extra;

namespace CSGBE.System.Mappers.MBC {
    internal class MBC1 : IMapper {
        protected byte cartSizeByte; // Size code of the cartridge
        protected byte romBank = 0; // Current ROM bank
        protected byte highRomBank = 0; // High bits for ROM bank
        protected bool bankingMode = false;

        public MBC1(byte cartSizeByte) {
            this.cartSizeByte = cartSizeByte;
        }

        public byte Read(byte[] rom, ushort address) {
            if (bankingMode) {
                return ReadBankingMode(rom, address);
            } else {
                return ReadNonBankingMode(rom, address);
            }
        }

        public virtual void Write(byte[] rom, ushort address, byte data) {
            if (address >= 0x2000 && address <= 0x3FFF) { // ROM Bank Number
                romBank = (byte)(data & CartUtils.BankBitMask(cartSizeByte));
            } else if (address >= 0x4000 && address <= 0x5FFF) { // ROM Bank Number
                if (cartSizeByte >= 0x05) {
                    highRomBank = (byte)(data & 0x03);
                }
            } else if (address >= 0x6000 && address <= 0x7FFF) { // Mode Select
                bankingMode = (data & 0x01) == 0x01; // false = Fixed ROM Bank, true = 
            }
        }

        public byte RAMRead(ushort address) {
            return 0xFF;
        }

        public void RAMWrite(ushort address, byte data) {}

        private byte ReadBankingMode(byte[] rom, ushort address) {
            if(address <= 0x3FFF) { //Low Rom Bank
                uint finalAddress = (uint)(highRomBank << 19) + address;
                return rom[finalAddress];
            } else {
                uint finalAddress = (uint)(highRomBank << 19) + (uint)(romBank << 14) + address;
                return rom[finalAddress];
            }
        }

        private byte ReadNonBankingMode(byte[] rom, ushort address) {
            if (address <= 0x3FFF) { //Low Rom Bank
                return rom[address];
            } else {
                uint finalAddress = (uint)(highRomBank << 19) + (uint)(romBank << 14) + address;
                return rom[finalAddress];
            }
        }
    }
}
