using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Sexy.Emit.Reflection
{
    public class ReflectionProvider : IEmitProvider
    {
        public IEnumerable<EmitAssembly> Assemblies => assemblies;

        private readonly List<EmitAssemblyBuilder> assemblies = new List<EmitAssemblyBuilder>();

        public EmitAssemblyBuilder DefineAssembly(string name)
        {
            var assembly = new EmitAssemblyBuilder(name);
            assemblies.Add(assembly);
            return assembly;
        }

        public Assembly Compile(EmitAssemblyBuilder assembly)
        {
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(assembly.Name), AssemblyBuilderAccess.RunAndSave);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(assembly.Name, "module.dll", true);

            var compiler = new SchemaCompiler(moduleBuilder);
            var types = new List<Type>();
            foreach (EmitTypeBuilder type in assembly.Types)
            {
                var typeBuilder = (TypeBuilder)type.Accept(compiler);
                var createdType = typeBuilder.CreateType();
                types.Add(createdType);
            }

            return types.First().Assembly;
        }

        private class SchemaCompiler : IEmitSchemaVisitor<TypeBuilder, MemberInfo>
        {
            private readonly ModuleBuilder moduleBuilder;

            public SchemaCompiler(ModuleBuilder moduleBuilder)
            {
                this.moduleBuilder = moduleBuilder;
            }

            public MemberInfo VisitType(EmitType type, TypeBuilder parent)
            {
                var typeAttributes = ReflectionTypeAttributes.ToTypeAttributes(type.Kind, type.Visibility, type.DeclaringType != null, type.IsAbstract, type.IsSealed);
                var interfaces = type.ImplementedInterfaces.Select(x => (Type)x).ToArray();

                TypeBuilder typeBuilder;
                if (parent == null)
                {
                    typeBuilder = moduleBuilder.DefineType(type.Name, typeAttributes, type.BaseType, interfaces);
                }
                else
                {
                    typeBuilder = parent.DefineNestedType(type.Name, typeAttributes, type.BaseType, interfaces);
                }

                foreach (var member in type.Members)
                {
                    member.Accept(this, typeBuilder);
                }

                return typeBuilder;
            }

            public MemberInfo VisitField(EmitField field, TypeBuilder parent)
            {
                var fieldBuilder = parent.DefineField(field.Name, field.FieldType,
                    ReflectionFieldAttributes.ToFieldAttributes(field.Visibility, field.IsStatic, field.IsReadOnly, false));
                return fieldBuilder;
            }

            public MemberInfo VisitConstructor(EmitConstructor constructor, TypeBuilder parent)
            {
                var constructorBuilder = parent.DefineConstructor(
                    ReflectionMethodAttributes.ToMethodAttributes(constructor.Visibility, false, false, false, false, constructor.IsStatic),
                    constructor.IsStatic ? CallingConventions.Standard : CallingConventions.HasThis,
                    constructor.Parameters.Select(x => (Type)x.ParameterType).ToArray());

                return constructorBuilder;
            }

            public MemberInfo VisitMethod(EmitMethod method, TypeBuilder parent)
            {
                var methodBuilder = parent.DefineMethod(
                    method.Name,
                    ReflectionMethodAttributes.ToMethodAttributes(method.Visibility, method.IsAbstract, method.IsSealed, method.IsVirtual, false, method.IsStatic),
                    method.ReturnType,
                    method.Parameters.Select(x => (Type)x.ParameterType).ToArray());

                return methodBuilder;
            }

            public MemberInfo VisitProperty(EmitProperty property, TypeBuilder parent)
            {
                var propertyBuilder = parent.DefineProperty(
                    property.Name,
                    PropertyAttributes.None,
                    property.PropertyType,
                    property.Parameters.Select(x => (Type)x.ParameterType).ToArray());

                return propertyBuilder;
            }
        }
    }
}