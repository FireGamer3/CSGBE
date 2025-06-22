using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGBE.System.MEM {
    internal class Memory {
        private byte[] memory;
        public Memory(ushort size) {
            memory = new byte[size];
        }
        public virtual byte Read(ushort address) {
            return memory[address];
        }
        public virtual void Write(ushort address, byte data) {
            memory[address] = data;
        }
    }
}
