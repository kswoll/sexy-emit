using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Sexy.Emit.Reflection
{
    public class ReflectionAssemblyBuilder : ReflectionAssembly, IEmitAssemblyBuilder
    {
        public AssemblyBuilder AssemblyBuilder { get; }
        public ModuleBuilder ModuleBuilder { get; }

        public static ReflectionAssemblyBuilder Create(string name)
        {
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(name), AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(name);
            return new ReflectionAssemblyBuilder(assemblyBuilder, moduleBuilder);
        }

        private ReflectionAssemblyBuilder(AssemblyBuilder assemblyBuilder, ModuleBuilder moduleBuilder) : base(assemblyBuilder)
        {
            AssemblyBuilder = assemblyBuilder;
            ModuleBuilder = moduleBuilder;
        }

        public IEnumerable<IEmitTypeBuilder> TypeBuilders
        {
            get { return ModuleBuilder.GetTypes().Select(x => new ReflectionTypeBuilder((TypeBuilder)x)); }
        }

        IEmitTypeBuilder IEmitAssemblyBuilder.DefineType(string name, EmitTypeKind kind, EmitVisibility visibility, bool isAbstract, bool isSealed) => DefineType(name, kind, visibility, isAbstract, isSealed);

        public ReflectionTypeBuilder DefineType(string name, EmitTypeKind kind = EmitTypeKind.Class, EmitVisibility visibility = EmitVisibility.Public, bool isAbstract = false, bool isSealed = false)
        {
            var typeBuilder = ModuleBuilder.DefineType(name, ReflectionTypeAttributes.ToTypeAttributes(kind, visibility, false, isAbstract, isSealed));
            return new ReflectionTypeBuilder(typeBuilder);
        }
    }
}
