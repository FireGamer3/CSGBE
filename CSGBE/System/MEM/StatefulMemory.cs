using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGBE.System.MEM {
    internal class StatefulMemory : Memory {
        public bool Enabled { get; set; } = false;

        public StatefulMemory(uint size) : base(size) { }

        public override byte Read(uint address) {
            if (!Enabled) return 0xFF;
            return base.Read(address);
        }
        public override void Write(uint address, byte data) {
            if (!Enabled) return;
            base.Write(address, data);
        }
    }
}
