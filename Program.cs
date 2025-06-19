using CSGBE.System;
using System.Diagnostics;

namespace CSGBE {
    internal class Program {
        static void Main(string[] args) {
            Clock clock = new Clock();

            //if(args.Length == 0) {
            //    Console.WriteLine("Usage: CSGBE <rom_file>");
            //    return;
            //}
            Cart cart = new Cart("./tetris.gb");
            if (!cart.Checksum()) {
                Console.WriteLine("ROM checksum failed.");
                return;
            }
            Console.WriteLine("ROM checksum passed.");
            Console.Title = cart.GetRomName();
            Console.WriteLine($"ROM Name: {cart.GetRomName()}");
            Console.WriteLine($"ROM Size: {cart.GetCartRomSize()} ({cart.GetFullRomSize()} bytes)");
            Console.WriteLine($"ROM Type: {cart.GetRomType()}");
            Console.WriteLine($"ROM Licensee: {cart.GetLicensee()}");


            while (true) {
                clock.WaitForNextTick();
            }
        }
    }
}
