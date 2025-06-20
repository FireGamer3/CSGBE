using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGBE.System {
    internal class Bus {
        private Cart cart;
        public Bus(Cart cart) {
            this.cart = cart;
        }

        public byte Read(ushort address) {
            if (address <= 0x3FFF) { // ROM Bank 0
                return cart.Read(address);
            } else if (address >= 0x4000 && address <= 0x7FFF) { // ROM Bank 1+
                return cart.Read(address);
            } else if (address >= 0x8000 && address <= 0x9FFF) { // VRAM
                // Handle reading from VRAM if implemented
                throw new NotImplementedException("Reading from VRAM is not implemented.");
            } else if (address >= 0xA000 && address <= 0xBFFF) { // External RAM
                return cart.RAMRead(address);
            } else if (address >= 0xC000 && address <= 0xDFFF) { // Internal RAM
                // Handle reading from internal RAM if implemented
                throw new NotImplementedException("Reading from internal RAM is not implemented.");
            } else if (address >= 0xE000 && address <= 0xFDFF) { // Echo RAM
                // Handle reading from echo RAM if implemented
                throw new NotImplementedException("Reading from echo RAM is not implemented.");
            } else if (address >= 0xFE00 && address <= 0xFE9F) { // OAM
                // Handle reading from OAM if implemented
                throw new NotImplementedException("Reading from OAM is not implemented.");
            } else if (address >= 0xFEA0 && address <= 0xFEFF) { // Unused Area
                return 0xFF; 
            } else if (address >= 0xFF00 && address <= 0xFF7F) { // I/O Ports
                // Handle reading from I/O ports if implemented
                throw new NotImplementedException("Reading from I/O ports is not implemented.");
            } else if (address >= 0xFF80 && address <= 0xFFFF) { // High RAM and Interrupt Enable Register
                // Handle reading from high RAM or interrupt enable register if implemented
                throw new NotImplementedException("Reading from high RAM or interrupt enable register is not implemented.");
            } else {
                throw new ArgumentOutOfRangeException(nameof(address), "Address is out of bounds of the memory map.");
            }
        }

        public void Write(ushort address, byte data) {
            if (address <= 0x3FFF) { // ROM Bank 0
                cart.Write(address, data);
            } else if (address >= 0x4000 && address <= 0x7FFF) { // ROM Bank 1+
                cart.Write(address, data);
            } else if (address >= 0x8000 && address <= 0x9FFF) { // VRAM
                // Handle writing to VRAM if implemented
                throw new NotImplementedException("Writing to VRAM is not implemented.");
            } else if (address >= 0xA000 && address <= 0xBFFF) { // External RAM
                cart.RAMWrite(address, data);
            } else if (address >= 0xC000 && address <= 0xDFFF) { // Internal RAM
                // Handle writing to internal RAM if implemented
                throw new NotImplementedException("Writing to internal RAM is not implemented.");
            } else if (address >= 0xE000 && address <= 0xFDFF) { // Echo RAM
                // Handle writing to echo RAM if implemented
                throw new NotImplementedException("Writing to echo RAM is not implemented.");
            } else if (address >= 0xFE00 && address <= 0xFE9F) { // OAM
                // Handle writing to OAM if implemented
                throw new NotImplementedException("Writing to OAM is not implemented.");
            } else if (address >= 0xFEA0 && address <= 0xFEFF) { // Unused Area
            } else if (address >= 0xFF00 && address <= 0xFF7F) { // I/O Ports
                // Handle writing to I/O ports if implemented
                throw new NotImplementedException("Writing to I/O ports is not implemented.");
            } else if (address >= 0xFF80 && address <= 0xFFFF) { // High RAM and Interrupt Enable Register
                // Handle writing to high RAM or interrupt enable register if implemented
                throw new NotImplementedException("Writing to high RAM or interrupt enable register is not implemented.");
            } else {
                throw new ArgumentOutOfRangeException(nameof(address), "Address is out of bounds of the memory map.");
            }
        }
    }
}
