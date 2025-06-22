using CSGBE.System.MEM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGBE.System {
    internal class Bus {
        private Cart cart;
        private Memory vram = new Memory(0x2000);
        private Memory wram = new Memory(0x2000);
        private Memory oam = new Memory(0x9F);
        public Bus(Cart cart) {
            this.cart = cart;
        }

        public byte Read(ushort address) {
            if (address <= 0x7FFF) { // Cartridge ROM
                return cart.Read(address);
            } else if (address >= 0x8000 && address <= 0x9FFF) { // VRAM
                ushort indexAddress = (ushort)(address - 0x8000);
                return vram.Read(indexAddress);
            } else if (address >= 0xA000 && address <= 0xBFFF) { // External RAM
                return cart.RAMRead(address);
            } else if (address >= 0xC000 && address <= 0xDFFF) { // WRAM
                ushort indexAddress = (ushort)(address - 0xC000);
                return wram.Read(indexAddress);
            } else if (address >= 0xE000 && address <= 0xFDFF) { // Echo RAM
                ushort indexAddress = (ushort)(address - 0xE000);
                return wram.Read(indexAddress);
            } else if (address >= 0xFE00 && address <= 0xFE9F) { // OAM
                ushort indexAddress = (ushort)(address - 0xFE00);
                return oam.Read(indexAddress);
            } else if (address >= 0xFEA0 && address <= 0xFEFF) { // Unused Area
                return 0xFF;
            } else if (address >= 0xFF00 && address <= 0xFF7F) { // I/O Ports
                throw new NotImplementedException("Reading from I/O ports is not implemented.");
            } else if (address >= 0xFF80 && address <= 0xFFFF) { // High RAM and Interrupt Enable Register
                throw new NotImplementedException("Reading from high RAM or interrupt enable register is not implemented.");
            } else {
                throw new ArgumentOutOfRangeException(nameof(address), "Address is out of bounds of the memory map.");
            }
        }

        public void Write(ushort address, byte data) {
            if (address <= 0x7FFF) { // Cartridge ROM
                cart.Write(address, data);
            } else if (address >= 0x8000 && address <= 0x9FFF) { // VRAM
                ushort indexAddress = (ushort)(address - 0x8000);
                vram.Write(indexAddress, data);
            } else if (address >= 0xA000 && address <= 0xBFFF) { // External RAM
                cart.RAMWrite(address, data);
            } else if (address >= 0xC000 && address <= 0xDFFF) { // Internal RAM
                ushort indexAddress = (ushort)(address - 0xC000);
                wram.Write(indexAddress, data);
            } else if (address >= 0xE000 && address <= 0xFDFF) { // Echo RAM
                // Technically this area is said to not be usable, but lets just mirror WRAM writes here for simplicity
                ushort indexAddress = (ushort)(address - 0xE000);
                wram.Write(indexAddress, data);
            } else if (address >= 0xFE00 && address <= 0xFE9F) { // OAM
                ushort indexAddress = (ushort)(address - 0xFE00);
                oam.Write(indexAddress, data);
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
