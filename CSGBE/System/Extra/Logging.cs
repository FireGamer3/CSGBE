using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGBE.System.Extra {
    internal static class Logging {
        public static void ExceptionHandle(Exception ex) {
#if DEBUG
            Console.WriteLine(ex);
#else
            Console.WriteLine(ex.Message);
#endif
            Environment.Exit(1);
        }

        public static void DebugLog(string message) {
#if DEBUG
            Console.WriteLine($"[DEBUG] {message}");
#endif
        }

        public static void InfoLog(string message) {
            Console.WriteLine($"[INFO] {message}");
        }
    }
}