using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspectProgramming
{
    [Serializable]
    public class CacheAttribute : MethodInterceptionAspect
    {
        private static readonly Dictionary<string, object> cache = new Dictionary<string, object>();

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            string key = BuildKey(args);

            lock (cache)
            {
                if (cache.TryGetValue(key, out object value))
                {
                    args.ReturnValue = value;
                }
                else
                {
                    args.Proceed();
                    cache[key] = args.ReturnValue;
                }
            }
        }

        private string BuildKey(MethodInterceptionArgs args)
        {
            StringBuilder b = new StringBuilder();
            b.AppendFormat("{0}, {1}", args.Instance, args.Method.Name);

            foreach (var parameter in args.Method.GetParameters())
                b.AppendFormat(" {0}", args.Arguments[parameter.Position]);

            return b.ToString();
        }
    }
}
