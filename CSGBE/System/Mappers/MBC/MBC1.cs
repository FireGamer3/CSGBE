using CSGBE.System.Extra;

namespace CSGBE.System.Mappers.MBC {
    internal class MBC1 : IMapper {
        byte cartSizeByte; // Size code of the cartridge
        byte romBank = 0; // Current ROM bank
        byte highBank = 0; // High bank for MBC1M

        public MBC1(byte cartSizeByte) {
            this.cartSizeByte = cartSizeByte;
        }

        public byte Read(byte[] rom, ushort address) {
            throw new NotImplementedException();
        }

        public virtual void Write(byte[] rom, ushort address, byte data) {
            if (address >= 0x2000 && address <= 0x3FFF) { // ROM Bank Number
                romBank = (byte)(data & CartUtils.BankBitMask(cartSizeByte));
            } else if (address >= 0x4000 && address <= 0x5FFF) { // RAM Bank Number (if present)

            } else if (address >= 0x6000 && address <= 0x7FFF) { // Mode Select

            }
        }

        public byte RAMRead(ushort address) {
            return 0xFF;
        }

        public void RAMWrite(ushort address, byte data) {}
    }
}
