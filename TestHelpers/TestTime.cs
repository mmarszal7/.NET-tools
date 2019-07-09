using System;
using System.Collections;
using System.Diagnostics;

/// <summary>
/// Code from BFsharp https://github.com/mac-michael/bfsharp/tree/master/BFsharp.Tests/TestHelpers
/// </summary>

namespace Tests
{
    public class TestTime : IDisposable
    {
        private Stopwatch _s;

        public static TestTime Start()
        {
            return new TestTime { _s = Stopwatch.StartNew() };
        }

        public static void Start(double iterations, Action action)
        {
            Start((int)iterations, action);
        }

        public static void Start(int iterations, Action action)
        {
            Debug.WriteLine("Iteration: 1");
            var w = Stopwatch.StartNew();

            action();
            w.Stop();
            long firstIterationTime = w.ElapsedMilliseconds;
            Console.WriteLine("First iteration: {0}ms", firstIterationTime);

            if (iterations == 1) return;

            Debug.WriteLine("Iterations: 2." + iterations);
            w.Start();
            for (int i = 1; i < iterations; i++)
                action();

            w.Stop();
            Console.WriteLine("Total time (without first iteration): {0}ms", w.ElapsedMilliseconds - firstIterationTime);
            Console.WriteLine("Total time: {0}ms", w.ElapsedMilliseconds);
            Console.WriteLine("Average time (without first iteration): {0}ms",
                              (w.ElapsedMilliseconds - firstIterationTime) / (decimal)(iterations - 1));
            Console.WriteLine("Average time: {0}ms", w.ElapsedMilliseconds / (decimal)iterations);

            Console.WriteLine("Average rate (without first iteration): {0}/sec.", 1000 / (w.ElapsedMilliseconds / (decimal)iterations));
            Console.WriteLine("Average rate: {0}/sec.", 1000 / (w.ElapsedMilliseconds - firstIterationTime) / (decimal)(iterations - 1));
        }

        public static void Measure(double miliseconds, Action action)
        {
            Measure((int)miliseconds, action, false);
        }

        public static void Measure(Action action)
        {
            Measure(1000, action);
        }

        public static void Measure(bool excludeFirstIteration, Action action)
        {
            Measure(1000, action, excludeFirstIteration);
        }

        public static void Measure(int miliseconds, Action action, bool excludeFirstIteration)
        {
            Debug.WriteLine("Iteration: 1");
            var w = Stopwatch.StartNew();

            action();
            w.Stop();
            long firstIterationTime = w.ElapsedMilliseconds;
            Console.WriteLine("First iteration: {0}ms", firstIterationTime);

            ArrayList l = new ArrayList();
            l.Add(1);

            Debug.WriteLine("Iterations: 2.");
            if (excludeFirstIteration)
            {
                w.Reset();
                firstIterationTime = 0;
            }
            w.Start();
            int iterations = 0;
            for (; w.ElapsedMilliseconds < miliseconds; iterations++)
                action();

            w.Stop();
            Console.WriteLine("Total time (without first iteration): {0}ms", w.ElapsedMilliseconds - firstIterationTime);
            Console.WriteLine("Total iterations: {0}", iterations);

            Console.WriteLine("Average time (without first iteration):\n {0}ms",
                              (w.ElapsedMilliseconds - firstIterationTime) / (decimal)(iterations - 1));
            Console.WriteLine("Average time:\n {0}ms", w.ElapsedMilliseconds / (decimal)iterations);

            Console.WriteLine("Average rate (without first iteration):\n {0}/sec.", 1000 / (w.ElapsedMilliseconds / (decimal)iterations));
            Console.WriteLine("Average rate:\n {0}/sec.", 1000 / ((w.ElapsedMilliseconds - firstIterationTime) / (decimal)(iterations - 1)));
        }

        public void Dispose()
        {
            _s.Stop();
            Console.WriteLine(_s.ElapsedMilliseconds);
        }
    }
}