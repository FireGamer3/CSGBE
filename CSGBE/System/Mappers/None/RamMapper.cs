using CSGBE.System.MEM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGBE.System.Mappers.None {
    internal class RamMapper : NoneMapper, IMapper {
        Memory ram = new Memory(0x2000);

        public new byte RAMRead(ushort address) {
            ushort ramAddress = (ushort)(address - 0xA000);
            return ram.Read(ramAddress);
        }

        public new void RAMWrite(ushort address, byte data) {
            ushort ramAddress = (ushort)(address - 0xA000);
            ram.Write(ramAddress, data);
        }
    }
}
