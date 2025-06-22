using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGBE.System.MEM {
    internal class Memory {
        private byte[] memory;
        public Memory(uint size) {
            memory = new byte[size];
        }

        public uint Size {
            get { return (uint)memory.Length; }
        }

        public virtual byte Read(uint address) {
            return memory[address];
        }
        public virtual void Write(uint address, byte data) {
            memory[address] = data;
        }
    }
}
