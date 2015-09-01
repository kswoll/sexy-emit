using System.Reflection;

namespace Sexy.Emit.Reflection
{
    public class ReflectionParameter : IEmitParameter
    {
        private ParameterInfo parameter;

        public ReflectionParameter(ParameterInfo parameter)
        {
            this.parameter = parameter;
        }

        public string Name => parameter.Name;
        public IEmitType ParameterType => new ReflectionType(parameter.ParameterType);
    }
}
