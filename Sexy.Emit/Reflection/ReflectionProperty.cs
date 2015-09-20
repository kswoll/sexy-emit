using System.Reflection;

namespace Sexy.Emit.Reflection
{
    public class ReflectionProperty : ReflectionMember, IEmitProperty
    {
        public PropertyInfo Property { get; }

        public ReflectionProperty(PropertyInfo property)
        {
            Property = property;
        }

        public string Name => Property.Name;
        public IEmitType PropertyType => new ReflectionType(Property.PropertyType);
        public IEmitMethod GetMethod => new ReflectionMethod(Property.GetMethod);
        public IEmitMethod SetMethod => new ReflectionMethod(Property.SetMethod);
    }
}