using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sexy.Emit.Reflection
{
    public class ReflectionType : IEmitType
    {
        public Type Type { get; }

        public ReflectionType(Type type)
        {
            Type = type;
        }

        public string Namespace => Type.Namespace;
        public string Name => Type.Name;
        public IEmitType DeclaringType => new ReflectionType(Type.DeclaringType);

        public IEnumerable<IEmitMember> Members
        {
            get
            {
                return Type
                    .GetMembers(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                    .Select(x => ReflectionMember.Create(x));
            }
        }
    }
}
