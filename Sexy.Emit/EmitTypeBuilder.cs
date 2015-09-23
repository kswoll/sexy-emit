using System;
using System.Collections.Generic;

namespace Sexy.Emit
{
    public class EmitTypeBuilder : EmitType
    {
        private List<EmitMemberReference> memberBuilders;

        public EmitTypeBuilder(EmitAssembly assembly, string ns, string name, EmitTypeKind kind, EmitType declaringType, 
            EmitVisibility visibility, EmitTypeReference baseType, Func<EmitType, IReadOnlyList<EmitTypeReference>> implementedInterfacesFactory,
            bool isAbstract, bool isSealed
        ) : 
            base(assembly, ns, name, kind, declaringType, 
                result => ((EmitTypeBuilder)result).memberBuilders = new List<EmitMemberReference>(), 
                visibility, baseType, implementedInterfacesFactory, isAbstract, isSealed)
        {
        }

        public EmitFieldBuilder DefineField(string name, EmitType type, EmitVisibility visibility = EmitVisibility.Public,
            bool isStatic = false, bool isReadonly = false, bool isVolatile = false)
        {
            var field = new EmitFieldBuilder(this, name, type, visibility, isStatic, isReadonly);
            memberBuilders.Add(new EmitFieldReference(() => field));
            return field;
        }

        public EmitMethodBuilder DefineMethod(string name, EmitType returnType, EmitVisibility visibility = EmitVisibility.Public,
            bool isAbstract = false, bool isSealed = false, bool isVirtual = false, bool isOverride = false,
            bool isExtern = false, bool isStatic = false)
        {
            var method = new EmitMethodBuilder(this, name, returnType, visibility, isStatic, isSealed, isVirtual, isAbstract);
            memberBuilders.Add(new EmitMethodReference(() => method));
            return method;
        }

        public void Compile()
        {
            
        }
    }
}
