namespace CSGBE.System.MBC {
    internal class NoneMapper : IMapper {
        byte[]? ram;
        bool ramEnabled;
        public NoneMapper(bool ramEnabled) {
            this.ramEnabled = ramEnabled;

            if (ramEnabled) {
                ram = new byte[8192]; // 8 KB of RAM
            }
        }

        public byte Read(byte[] rom, ushort address) {
            return rom[address];
        }

        public void Write(byte[] rom, ushort address, byte data) { }
        
        public byte RAMRead(ushort address) {
            if(!ramEnabled || ram == null) return 0xFF;
            ushort ramAddress = (ushort)(address - 0xA000);
            return ram[ramAddress];
        }

        public void RAMWrite(ushort address, byte data) {
            if (!ramEnabled || ram == null) return;
            ushort ramAddress = (ushort)(address - 0xA000);
            ram[ramAddress] = data;
        }
    }
}
