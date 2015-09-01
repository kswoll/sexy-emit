using System;
using NUnit.Framework;
using Sexy.Emit;
using Sexy.Emit.Reflection;

namespace SexyEmit.Tests.Reflection
{
    [TestFixture]
    public class IlTests
    {
        [Test]
        public void DeclareVariable()
        {
            var assemblyBuilder = ReflectionAssemblyBuilder.Create("TestAssembly");
            var typeBuilder = assemblyBuilder.DefineType("TestType");
            var method = typeBuilder.DefineMethod("Foo", typeof(string));
            var variable = method.Il.DeclareLocal(typeof(string));
            method.Il.Emit(EmitOpCode.Ldstr, "bar");
            method.Il.Emit(EmitOpCode.Stloc, variable);
            method.Il.Emit(EmitOpCode.Ldloc, variable);
            method.Il.Emit(EmitOpCode.Ret);

            var type = typeBuilder.CreateType();
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Foo");

            var result = (string)methodInfo.Invoke(instance, null);
            Assert.AreEqual("bar", result);
        }

        [Test]
        public void GotoLabel()
        {
            var assemblyBuilder = ReflectionAssemblyBuilder.Create("TestAssembly");
            var typeBuilder = assemblyBuilder.DefineType("TestType");
            var method = typeBuilder.DefineMethod("Foo", typeof(int));
            var variable = method.Il.DeclareLocal(typeof(int));
            var label = method.Il.DefineLabel();
            method.Il.Emit(EmitOpCode.Ldc_I4_0);
            method.Il.Emit(EmitOpCode.Stloc, variable);
            method.Il.MarkLabel(label);
            method.Il.Emit(EmitOpCode.Ldloc, variable);
            method.Il.Emit(EmitOpCode.Ldc_I4_1);
            method.Il.Emit(EmitOpCode.Add);
            method.Il.Emit(EmitOpCode.Dup);
            method.Il.Emit(EmitOpCode.Stloc, variable);
            method.Il.Emit(EmitOpCode.Ldc_I4_5);
            method.Il.Emit(EmitOpCode.Blt, label);
            method.Il.Emit(EmitOpCode.Ldloc, variable);
            method.Il.Emit(EmitOpCode.Ret);

            var type = typeBuilder.CreateType();
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Foo");

            var result = (int)methodInfo.Invoke(instance, null);
            Assert.AreEqual(5, result);            
        }
    }
}