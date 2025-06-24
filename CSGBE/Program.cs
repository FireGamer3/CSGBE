using CSGBE.System;
using CSGBE.System.Extra;
using System.Diagnostics;

namespace CSGBE {
    internal class Program {
        static void Main(string[] args) {
            Clock clock = new Clock();

            //if(args.Length == 0) {
            //    Logging.InfoLog("Usage: CSGBE <rom_file>");
            //    return;
            //}
            //cpu_instrs.gb, tetris.gb
            Cart cart;
            try {
                cart = new Cart("./tetris.gb");
            } catch (Exception ex) {
                Logging.ExceptionHandle(ex);
                return;
            }
            if (!cart.Checksum()) {
                Logging.InfoLog("ROM checksum failed.");
                return;
            }
            Logging.DebugLog("ROM checksum passed.");
            Console.Title = cart.GetRomName();
            Logging.DebugLog($"ROM Name: {cart.GetRomName()}");
            Logging.DebugLog($"ROM Size: {cart.GetCartRomSize()} ({cart.GetFullRomSize()} bytes)");
            Logging.DebugLog($"RAM Size: {cart.GetCartRamSize()}");
            Logging.DebugLog($"ROM Type: {cart.GetRomType()}");
            Logging.DebugLog($"ROM Licensee: {cart.GetLicensee()}");

            Bus bus = new Bus(cart);
            try {
                while (true) {
                    bus.Clock();
                    clock.WaitForNextTick();
                }
            } catch (Exception ex) {
                Logging.ExceptionHandle(ex);
                return;
            }
        }
    }
}
