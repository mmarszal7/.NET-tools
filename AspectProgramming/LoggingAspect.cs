using PostSharp.Aspects;
using System;

namespace AspectProgramming
{
    [Serializable]
    public class LoggingAspect : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            Console.WriteLine($"Method {args.Method.Name} has started");
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            Console.WriteLine($"Method {args.Method.Name} has ended \n");
        }
    }
}
