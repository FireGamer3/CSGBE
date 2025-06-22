namespace CSGBE.System.Mappers {
    internal interface IMapper {
        public byte Read(byte[] rom, ushort address);
        public void Write(byte[] rom, ushort address, byte data);

        public byte RAMRead(ushort address);

        public void RAMWrite(ushort address, byte data);
    }
}
