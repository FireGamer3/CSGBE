using CSGBE.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CSGBE.CPU {
    internal class SM83 {
        private Bus bus;
        private int MCycleTicksLeft = 4;
        public Register A { get; set; } = new Register(); // Accumulator
        public Register F { get; set; } = new Register(); // Flags
        public Register B { get; set; } = new Register(); // General purpose register
        public Register C { get; set; } = new Register(); // General purpose register
        public Register D { get; set; } = new Register(); // General purpose register
        public Register E { get; set; } = new Register(); // General purpose register
        public Register H { get; set; } = new Register(); // High byte of HL register pair
        public Register L { get; set; } = new Register(); // Low byte of HL register pair
        public ushort PC { get; set; } // Program Counter
        public ushort SP { get; set; } // Stack Pointer
        public ushort AF { get { return (ushort)((A.Value << 8) | F.Value); } set { A.Value = (byte)(value >> 8); F.Value = (byte)(value & 0xFF); } } // Combined AF register
        public ushort BC { get { return (ushort)((B.Value << 8) | C.Value); } set { B.Value = (byte)(value >> 8); C.Value = (byte)(value & 0xFF); } } // Combined BC register
        public ushort DE { get { return (ushort)((D.Value << 8) | E.Value); } set { D.Value = (byte)(value >> 8); E.Value = (byte)(value & 0xFF); } } // Combined DE register
        public ushort HL { get { return (ushort)((H.Value << 8) | L.Value); } set { H.Value = (byte)(value >> 8); L.Value = (byte)(value & 0xFF); } } // Combined HL register

        public SM83(Bus bus) {
            this.bus = bus;
            Reset();
        }

        public void Clock() {
            if (MCycleTicksLeft > 0) {
                MCycleTicksLeft--;
            } else {
                byte opcode = bus.Read(PC);
                byte extraBytes = CPUUtils.EXTRA_BYTES[opcode];
                byte[] operands = new byte[extraBytes];
                for (int i = 0; i < extraBytes; i++) {
                    operands[i] = bus.Read((ushort)(PC + 1 + i));
                }
                PC += (ushort)(1 + extraBytes);
                MCycleTicksLeft += 4 + (extraBytes * 4);
                Execute(opcode, operands);
            }
        }

        private void Execute(byte opcode, byte[] operands) {
            byte highNibble = (byte)(opcode >> 4);
            switch (highNibble) {
                case 0x0:
                    ExecuteHighByteZeroInstruction(opcode, operands);
                    break;
                case 0x1:
                    ExecuteHighByteOneInstruction(opcode, operands);
                    break;
                case 0x2:
                    ExecuteHighByteTwoInstruction(opcode, operands);
                    break;
                case 0x3:
                    ExecuteHighByteThreeInstruction(opcode, operands);
                    break;
                case 0x4:
                    ExecuteHighByteFourInstruction(opcode, operands);
                    break;
                case 0x5:
                    ExecuteHighByteFiveInstruction(opcode, operands);
                    break;
                case 0x6:
                    ExecuteHighByteSixInstruction(opcode, operands);
                    break;
                case 0x7:
                    ExecuteHighByteSevenInstruction(opcode, operands);
                    break;
                case 0x8:
                    ExecuteHighByteEightInstruction(opcode, operands);
                    break;
                case 0x9:
                    ExecuteHighByteNineInstruction(opcode, operands);
                    break;
                case 0xA:
                    ExecuteHighByteAInstruction(opcode, operands);
                    break;
                case 0xB:
                    ExecuteHighByteBInstruction(opcode, operands);
                    break;
                case 0xC:
                    ExecuteHighByteCInstruction(opcode, operands);
                    break;
                case 0xD:
                    ExecuteHighByteDInstruction(opcode, operands);
                    break;
                case 0xE:
                    ExecuteHighByteEInstruction(opcode, operands);
                    break;
                case 0xF:
                    ExecuteHighByteFInstruction(opcode, operands);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid opcode: {opcode:X2}");
            }
        }

        public void Reset() {
            A.Value = 0;
            F.Value = 0;
            B.Value = 0;
            C.Value = 0;
            D.Value = 0;
            E.Value = 0;
            H.Value = 0;
            L.Value = 0;
            PC = 0x0100; // Typical starting point for the program counter
            SP = 0xFFFE; // Typical starting point for the stack pointer
        }

        private void ExecuteHighByteZeroInstruction(byte opcode, byte[] operands) {
            switch(opcode) {
                case 0x00: // NOP
                    break;
                default:
                    throw new InvalidOperationException($"opcode: {opcode:X2} Not Implemented yet");
            }
        }

        private void ExecuteHighByteOneInstruction(byte opcode, byte[] operands) {
            throw new InvalidOperationException($"opcode: {opcode:X2} Not Implemented yet");

        }
        private void ExecuteHighByteTwoInstruction(byte opcode, byte[] operands) {
            throw new InvalidOperationException($"opcode: {opcode:X2} Not Implemented yet");
        }
        private void ExecuteHighByteThreeInstruction(byte opcode, byte[] operands) {
            throw new InvalidOperationException($"opcode: {opcode:X2} Not Implemented yet");
        }
        private void ExecuteHighByteFourInstruction(byte opcode, byte[] operands) {
            LoadRegister8ToRegister8(opcode);
        }
        private void ExecuteHighByteFiveInstruction(byte opcode, byte[] operands) {
            LoadRegister8ToRegister8(opcode);
        }
        private void ExecuteHighByteSixInstruction(byte opcode, byte[] operands) {
            LoadRegister8ToRegister8(opcode);
        }
        private void ExecuteHighByteSevenInstruction(byte opcode, byte[] operands) {
            LoadRegister8ToRegister8(opcode);
        }
        private void ExecuteHighByteEightInstruction(byte opcode, byte[] operands) {
            throw new InvalidOperationException($"opcode: {opcode:X2} Not Implemented yet");
        }
        private void ExecuteHighByteNineInstruction(byte opcode, byte[] operands) {
            throw new InvalidOperationException($"opcode: {opcode:X2} Not Implemented yet");
        }
        private void ExecuteHighByteAInstruction(byte opcode, byte[] operands) {
            throw new InvalidOperationException($"opcode: {opcode:X2} Not Implemented yet");
        }
        private void ExecuteHighByteBInstruction(byte opcode, byte[] operands) {
            throw new InvalidOperationException($"opcode: {opcode:X2} Not Implemented yet");
        }
        private void ExecuteHighByteCInstruction(byte opcode, byte[] operands) {
            throw new InvalidOperationException($"opcode: {opcode:X2} Not Implemented yet");
        }
        private void ExecuteHighByteDInstruction(byte opcode, byte[] operands) {
            throw new InvalidOperationException($"opcode: {opcode:X2} Not Implemented yet");
        }
        private void ExecuteHighByteEInstruction(byte opcode, byte[] operands) {
            throw new InvalidOperationException($"opcode: {opcode:X2} Not Implemented yet");
        }
        private void ExecuteHighByteFInstruction(byte opcode, byte[] operands) {
            throw new InvalidOperationException($"opcode: {opcode:X2} Not Implemented yet");
        }

        private void LoadRegister8ToRegister8(byte opcode) {
            byte dest = (byte)((opcode & 0x38) >> 3);
            byte src = (byte)(opcode & 0x07);
            if (dest == 0x06 && src == 0x06) {
                // HALT instruction
                // For now, just throw an exception to indicate it's not implemented
                throw new NotImplementedException("HALT instruction is not implemented yet.");
            }
            LoadR8toR8(dest, src);
        }

        private void LoadR8toR8(byte destR8, byte srcR8) {
            byte value = GetR8Value(srcR8);
            switch (destR8) {
                case 0x0: B.Value = value; break;
                case 0x1: C.Value = value; break;
                case 0x2: D.Value = value; break;
                case 0x3: E.Value = value; break;
                case 0x4: H.Value = value; break;
                case 0x5: L.Value = value; break;
                case 0x6:
                    MCycleTicksLeft += 4; // Additional 4 cycles for memory access
                    bus.Write(HL, value); // (HL)
                    break;
                case 0x7: A.Value = value; break;
                default: throw new InvalidOperationException($"Invalid r8 value: {destR8:X2}");
            }
        }

        private byte GetR8Value(byte r8) {
            switch(r8) {
                case 0x0: return B.Value;
                case 0x1: return C.Value;
                case 0x2: return D.Value;
                case 0x3: return E.Value;
                case 0x4: return H.Value;
                case 0x5: return L.Value;
                case 0x6:
                    MCycleTicksLeft += 4; // Additional 4 cycles for memory access
                    return bus.Read(HL); // (HL)
                case 0x7: return A.Value;
                default: throw new InvalidOperationException($"Invalid r8 value: {r8:X2}");
            }
        }
    }
}
