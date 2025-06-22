using CSGBE.System.MEM;

namespace CSGBE.System.Mappers.MBC {
    internal class MBC1Ram : MBC1, IMapper {
        StatefulMemory ram;

        public MBC1Ram(byte cartSizeByte) : base(cartSizeByte) {
            ram = new StatefulMemory(RamInitializationSize(cartSizeByte));
        }

        public override void Write(byte[] rom, ushort address, byte data) {
            if (address <= 0x1FFF) { // RAM Enable
                ram.Enabled = (data & 0x0F) == 0x0A; // Enable RAM if data is 0x0A, otherwise disable it
            } else base.Write(rom, address, data);
        }

        public new byte RAMRead(ushort address) {
            if (address < 0xA000 || address > 0xBFFF) return 0xFF;
            return ram.Read((ushort)(address - 0xA000));
        }

        public new void RAMWrite(ushort address, byte data) {
            if (address < 0xA000 || address > 0xBFFF) return;
            ram.Write((ushort)(address - 0xA000), data);
        }

        protected ushort RamInitializationSize(byte cartSizeByte) {
            if (cartSizeByte <= 0x04) return 0x8000; // 32 KB RAM
            else return 0x2000; // 8 KB RAM
        }
    }
}
