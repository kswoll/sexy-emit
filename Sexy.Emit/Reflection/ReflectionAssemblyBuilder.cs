using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Sexy.Emit.Reflection
{
    public class ReflectionAssemblyBuilder : ReflectionAssembly, IEmitAssemblyBuilder
    {
        private readonly AssemblyBuilder assemblyBuilder;
        private readonly ModuleBuilder moduleBuilder;

        public static ReflectionAssemblyBuilder Create(string name)
        {
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(name), AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(name);
            return new ReflectionAssemblyBuilder(assemblyBuilder, moduleBuilder);
        }

        private ReflectionAssemblyBuilder(AssemblyBuilder assemblyBuilder, ModuleBuilder moduleBuilder) : base(assemblyBuilder)
        {
            this.assemblyBuilder = assemblyBuilder;
            this.moduleBuilder = moduleBuilder;
        }

        public IEnumerable<IEmitTypeBuilder> TypeBuilders
        {
            get { return moduleBuilder.GetTypes().Select(x => new ReflectionTypeBuilder((TypeBuilder)x)); }
        }

        IEmitTypeBuilder IEmitAssemblyBuilder.DefineType(string name, EmitTypeAttributes typeAttributes) => DefineType(name, typeAttributes);

        public ReflectionTypeBuilder DefineType(string name, EmitTypeAttributes typeAttributes = 0)
        {
            var typeBuilder = moduleBuilder.DefineType(name, typeAttributes.ToTypeAttributes(false));
            return new ReflectionTypeBuilder(typeBuilder);
        }
    }
}
