using System;

namespace AspectProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            // Attribute aspect
            TestLoggingAspect();

            // Interceptor aspect
            var testValue2 = TestCachingInterceptor();
            Console.WriteLine(testValue2);
            var testValueFromCache = TestCachingInterceptor();
            Console.WriteLine(testValueFromCache + "\n");

            // Custom using/disposable aspect
            using (var profiler = new DisposableProfiler())
            {
                TestDisposableProfiler();
            }

            Console.ReadKey();
        }

        [LoggingAspect]
        static void TestLoggingAspect()
        {
            Console.WriteLine("Demo data 1");
        }

        [CacheAttribute]
        static string TestCachingInterceptor()
        {
            Console.WriteLine("Test method invoked.");
            return "Result: Demo data 2";
        }

        static void TestDisposableProfiler()
        {
            var squareSum = 0;
            for (int i = 0; i < 1000000; i++)
            {
                squareSum += i * i;
            }
        }
    }
}
