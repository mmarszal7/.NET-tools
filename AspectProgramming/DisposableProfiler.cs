using System;
using System.Diagnostics;

namespace AspectProgramming
{
    public class DisposableProfiler : IDisposable
    {
        private readonly Stopwatch stopwatch;

        public DisposableProfiler()
        {
            stopwatch = Stopwatch.StartNew();
        }

        public static DisposableProfiler Start()
        {
            return new DisposableProfiler();
        }

        public void Dispose()
        {
            stopwatch.Stop();
            Console.WriteLine($"Method executed in {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
