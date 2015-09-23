using System.Collections.Generic;

namespace Sexy.Emit
{
    public class EmitAssemblyBuilder : EmitAssembly
    {
        private List<EmitTypeReference> typeBuilders = new List<EmitTypeReference>();

        public EmitAssemblyBuilder(string name) : base(name, result => ((EmitAssemblyBuilder)result).typeBuilders = new List<EmitTypeReference>())
        {
        }

        public EmitTypeBuilder DefineType(string ns, string name, EmitTypeKind kind = EmitTypeKind.Class, 
            EmitVisibility visibility = EmitVisibility.Public, EmitTypeReference baseType = null, 
            IReadOnlyList<EmitTypeReference> implementedInterfaces = null, bool isAbstract = false,
            bool isSealed = false)
        {
            baseType = baseType ?? typeof(object);
            var typeBuilder = new EmitTypeBuilder(this, ns, name, kind, null, visibility, baseType, result => implementedInterfaces ?? new List<EmitTypeReference>(), isAbstract, isSealed);
            typeBuilders.Add(new EmitTypeReference(() => typeBuilder));
            return typeBuilder;
        }
    }
}
