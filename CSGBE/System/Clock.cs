using System.Diagnostics;

namespace CSGBE.System {
    internal class Clock {
        const double targetFrequency = 4_194_304; // Hz
        const double targetPeriodNs = 1_000_000_000.0 / targetFrequency; // nanoseconds
        Stopwatch stopwatch;
        long lastTick;
        double nsPerTick = 1_000_000_000.0 / Stopwatch.Frequency;

        public Clock() {
            stopwatch = Stopwatch.StartNew();
            lastTick = stopwatch.ElapsedTicks;
        }

        public void WaitForNextTick() {
            long currentTick = stopwatch.ElapsedTicks;
            double elapsedNs = (currentTick - lastTick) * nsPerTick;
            while (elapsedNs < targetPeriodNs) {
                currentTick = stopwatch.ElapsedTicks;
                elapsedNs = (currentTick - lastTick) * nsPerTick;
            }
            lastTick = currentTick;
        }
    }
}
