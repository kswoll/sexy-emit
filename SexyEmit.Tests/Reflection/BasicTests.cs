using System;
using System.Reflection;
using NUnit.Framework;
using Sexy.Emit;
using Sexy.Emit.Reflection;

namespace SexyEmit.Tests.Reflection
{
    [TestFixture]
    public class BasicTests
    {
        [Test]
        public void SetAndGetValueFromField()
        {
            var assemblyBuilder = ReflectionAssemblyBuilder.Create("SetAndGetValueFromFieldAssembly");
            var typeBuilder = assemblyBuilder.DefineType("SetAndGetValueFromField");
            typeBuilder.DefineField("foo", typeof(string));
            var type = typeBuilder.CreateType();
            var instance = Activator.CreateInstance(type);
            var field = type.GetField("foo");

            Assert.IsNull(field.GetValue(instance));
            field.SetValue(instance, "bar");
            Assert.AreEqual("bar", field.GetValue(instance));
        }

        [Test]
        public void MethodReturnsString()
        {
            var assemblyBuilder = ReflectionAssemblyBuilder.Create("MethodReturnsStringAssembly");
            var typeBuilder = assemblyBuilder.DefineType("MethodReturnsString");
            var method = typeBuilder.DefineMethod("Foo", typeof(string));
            method.Il.Emit(EmitOpCode.Ldstr, "bar");
            method.Il.Emit(EmitOpCode.Ret);

            var type = typeBuilder.CreateType();
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Foo");

            var result = (string)methodInfo.Invoke(instance, null);
            Assert.AreEqual("bar", result);
        }

        [Test]
        public void PrivateMethodIsPrivate()
        {
            var assemblyBuilder = ReflectionAssemblyBuilder.Create("PrivateMethodIsPrivateAssembly");
            var typeBuilder = assemblyBuilder.DefineType("PrivateMethodIsPrivate");
            var method = typeBuilder.DefineMethod("Foo", typeof(string), EmitVisibility.Private);
            method.Il.Emit(EmitOpCode.Ldstr, "bar");
            method.Il.Emit(EmitOpCode.Ret);

            var type = typeBuilder.CreateType();
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Foo", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.IsNotNull(methodInfo);
        }
    }
}