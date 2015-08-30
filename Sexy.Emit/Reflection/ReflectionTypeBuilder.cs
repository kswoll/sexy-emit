using System;
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

        public Type CreateType()
        {
            return type.CreateType();
        }
    }
}
