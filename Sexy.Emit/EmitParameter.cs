using System.Collections.Concurrent;
using System.Reflection;

namespace Sexy.Emit
{
    public class EmitParameter
    {
        public string Name { get; }
        public EmitType ParameterType { get; }

        public EmitParameter(string name, EmitType parameterType)
        {
            Name = name;
            ParameterType = parameterType;
        }

        private static readonly ConcurrentDictionary<ParameterInfo, EmitParameter> cache = new ConcurrentDictionary<ParameterInfo,EmitParameter>();

        public static implicit operator EmitParameter(ParameterInfo parameter)
        {
            return cache.GetOrAdd(parameter, _ => new EmitParameter(parameter.Name, parameter.ParameterType));
        }
    }
}
