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

            private void GenerateIl(ILGenerator il, EmitIl emitIl)
            {
                var locals = emitIl.Locals.ToDictionary(x => x, x => il.DeclareLocal(x.Type));
                var labels = emitIl.Labels.ToDictionary(x => x, x => il.DefineLabel());

                foreach (var instruction in emitIl.Instructions)
                {
                    foreach (var label in instruction.Labels)
                        il.MarkLabel(labels[label]);

                    var opCode = instruction.OpCode.ToOpCode();
                    if (instruction.Operand == null)
                        il.Emit(opCode);
                    else if (instruction.Operand is EmitType)
                        il.Emit(opCode, (EmitType)instruction.Operand);
                    else if (instruction.Operand is EmitMethod)
                        il.Emit(opCode, (EmitMethod)instruction.Operand);
                    else if (instruction.Operand is EmitConstructor)
                        il.Emit(opCode, (EmitConstructor)instruction.Operand);
                    else if (instruction.Operand is EmitField)
                        il.Emit(opCode, (EmitField)instruction.Operand);
                    else if (instruction.Operand is int)
                        il.Emit(opCode, (int)instruction.Operand);
                    else if (instruction.Operand is short)
                        il.Emit(opCode, (short)instruction.Operand);
                    else if (instruction.Operand is byte)
                        il.Emit(opCode, (byte)instruction.Operand);
                    else if (instruction.Operand is sbyte)
                        il.Emit(opCode, (sbyte)instruction.Operand);
                    else if (instruction.Operand is long)
                        il.Emit(opCode, (long)instruction.Operand);
                    else if (instruction.Operand is double)
                        il.Emit(opCode, (double)instruction.Operand);
                    else if (instruction.Operand is float)
                        il.Emit(opCode, (float)instruction.Operand);
                    else if (instruction.Operand is string)
                        il.Emit(opCode, (string)instruction.Operand);
                    else if (instruction.Operand is EmitLocal)
                        il.Emit(opCode, locals[(EmitLocal)instruction.Operand]);
                    else if (instruction.Operand is EmitLabel)
                        il.Emit(opCode, labels[(EmitLabel)instruction.Operand]);
                    else if (instruction.Operand is EmitLabel[])
                        il.Emit(opCode, ((EmitLabel[])instruction.Operand).Select(x => labels[x]).ToArray());
                    else
                        throw new Exception("Unexpected operand type: " + instruction.Operand.GetType().FullName);
                }                
            }

            public MemberInfo VisitMethod(EmitMethod method, TypeBuilder parent)
            {
                var methodBuilder = parent.DefineMethod(
                    method.Name,
                    ReflectionMethodAttributes.ToMethodAttributes(method.Visibility, method.IsAbstract, method.IsSealed, method.IsVirtual, false, method.IsStatic),
                    method.ReturnType,
                    method.Parameters.Select(x => (Type)x.ParameterType).ToArray());

                var emitMethodBuilder = (EmitMethodBuilder)method;
                emitMethodBuilder.Compile();

                var il = methodBuilder.GetILGenerator();
                GenerateIl(il, emitMethodBuilder.Il);

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