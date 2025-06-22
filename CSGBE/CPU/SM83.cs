using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGBE.CPU {
    internal class SM83 {
        public byte A { get; set; } // Accumulator
        public byte F { get; set; } // Flags
        public byte B { get; set; } // General purpose register
        public byte C { get; set; } // General purpose register
        public byte D { get; set; } // General purpose register
        public byte E { get; set; } // General purpose register
        public byte H { get; set; } // High byte of HL register pair
        public byte L { get; set; } // Low byte of HL register pair
        public ushort PC { get; set; } // Program Counter
        public ushort SP { get; set; } // Stack Pointer
        public ushort AF { get { return (ushort)((A << 8) | F); } set { A = (byte)(value >> 8); F = (byte)(value & 0xFF); } } // Combined AF register
        public ushort BC { get { return (ushort)((B << 8) | C); } set { B = (byte)(value >> 8); C = (byte)(value & 0xFF); } } // Combined BC register
        public ushort DE { get { return (ushort)((D << 8) | E); } set { D = (byte)(value >> 8); E = (byte)(value & 0xFF); } } // Combined DE register
        public ushort HL { get { return (ushort)((H << 8) | L); } set { H = (byte)(value >> 8); L = (byte)(value & 0xFF); } } // Combined HL register

        public SM83() {
            Reset();
        }

        public void Reset() {
            A = 0;
            F = 0;
            B = 0;
            C = 0;
            D = 0;
            E = 0;
            H = 0;
            L = 0;
            PC = 0x0100; // Typical starting point for the program counter
            SP = 0xFFFE; // Typical starting point for the stack pointer
        }
    }
}
