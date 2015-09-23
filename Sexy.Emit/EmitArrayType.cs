using System;
using System.Collections.Generic;

namespace Sexy.Emit
{
    public class EmitArrayType : EmitType
    {
        public EmitType ElementType { get; }
        public int Rank { get; }

        public EmitArrayType(EmitAssembly assembly, string ns, string name, EmitTypeKind kind, EmitType declaringType, 
            Func<EmitType, IReadOnlyList<EmitMemberReference>> membersFactory, EmitTypeReference baseType, 
            EmitType elementType, int rank
        ) : 
            base(assembly, ns, name, kind, declaringType, membersFactory, elementType.Visibility, baseType, _ => new List<EmitTypeReference>(), true, true)
        {
            ElementType = elementType;
            Rank = rank;
        }
    }
}
