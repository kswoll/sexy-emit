using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Sexy.Emit.Reflection
{
    public class ReflectionTypeBuilder : ReflectionType, IEmitTypeBuilder
    {
        private readonly TypeBuilder type;

        public ReflectionTypeBuilder(TypeBuilder type) : base(type)
        {
            this.type = type;
        }

        public IEmitFieldBuilder DefineField(string name, IEmitType type)
        {
            return new ReflectionFieldBuilder(this.type.DefineField(name, ((ReflectionType)type).Type, FieldAttributes.Public));
        }

        public IEmitMethodBuilder DefineMethod(string name, IEmitType returnType, EmitVisibility visibility = EmitVisibility.Public, 
            bool isAbstract = false, bool isSealed = false, bool isVirtual = false, bool isOverride = false, bool isExtern = false, 
            params IEmitType[] parameterTypes)
        {
            return new ReflectionMethodBuilder(type.DefineMethod(name, ReflectionMethodAttributes.ToMethodAttributes(visibility, isAbstract, isSealed, isVirtual, isExtern), 
                ((ReflectionType)returnType).Type, parameterTypes.Select(x => ((ReflectionType)x).Type).ToArray()));
        }

        public Type CreateType()
        {
            return type.CreateType();
        }
    }
}
