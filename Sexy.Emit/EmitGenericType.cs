using System;
using System.Collections.Generic;
using System.Linq;

namespace Sexy.Emit
{
    public class EmitGenericType : EmitType
    {
        public EmitType GenericTypeDefinition { get; }
        public IReadOnlyList<EmitType> TypeArguments { get; }

        public EmitGenericType(EmitAssembly assembly, string ns, string name, EmitType declaringType, Func<EmitType, IReadOnlyList<EmitMemberReference>> membersFactory, EmitTypeReference baseType, EmitType genericTypeDefinition, IReadOnlyList<EmitType> typeArguments) : base(assembly, ns, name, genericTypeDefinition.Kind, declaringType, membersFactory, genericTypeDefinition.Visibility, baseType, DeriveImplementedInterfaces(genericTypeDefinition), genericTypeDefinition.IsAbstract, genericTypeDefinition.IsSealed)
        {
            GenericTypeDefinition = genericTypeDefinition;
            TypeArguments = typeArguments;
        }

        private static Func<EmitType, IReadOnlyList<EmitTypeReference>> DeriveImplementedInterfaces(EmitType genericTypeDefinition)
        {
            return result => new List<EmitTypeReference>(genericTypeDefinition.ImplementedInterfaces.Select(x => new EmitTypeReference(() => x)));
        }
    }
}
