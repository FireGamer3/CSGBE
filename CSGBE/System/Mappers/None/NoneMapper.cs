namespace CSGBE.System.Mappers.None {
    internal class NoneMapper : IMapper {
        public byte Read(byte[] rom, ushort address) {
            return rom[address];
        }

        public void Write(byte[] rom, ushort address, byte data) { }

        public byte RAMRead(ushort address) {
            return 0xFF;
        }

        public void RAMWrite(ushort address, byte data) {}
    }
}
