using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGBE.System.MEM {
    internal class StatefulMemory : Memory {
        public bool Enabled { get; set; } = false;

        public StatefulMemory(ushort size) : base(size) { }

        public override byte Read(ushort address) {
            if (!Enabled) return 0xFF;
            return base.Read(address);
        }
        public override void Write(ushort address, byte data) {
            if (!Enabled) return;
            base.Write(address, data);
        }
    }
}
