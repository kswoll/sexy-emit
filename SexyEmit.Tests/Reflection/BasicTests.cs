using System;
using System.Reflection;
using NUnit.Framework;
using Sexy.Emit;
using Sexy.Emit.OpCodes;
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
        public void StaticFieldIsStatic()
        {
            var assemblyBuilder = ReflectionAssemblyBuilder.Create("TestAssembly");
            var typeBuilder = assemblyBuilder.DefineType("TestType");
            typeBuilder.DefineField("foo", typeof(string), isStatic: true);
            var type = typeBuilder.CreateType();
            var field = type.GetField("foo");
            Assert.IsTrue(field.IsStatic);
        }

        [Test]
        public void MethodReturnsString()
        {
            var assemblyBuilder = ReflectionAssemblyBuilder.Create("MethodReturnsStringAssembly");
            var typeBuilder = assemblyBuilder.DefineType("MethodReturnsString");
            var method = typeBuilder.DefineMethod("Foo", typeof(string));
            method.Il.Emit(EmitOpCodes.Ldstr, "bar");
            method.Il.Emit(EmitOpCodes.Ret);

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
            var method = typeBuilder.DefineMethod("Foo", typeof(void), EmitVisibility.Private);
            method.Il.Emit(EmitOpCodes.Ret);

            var type = typeBuilder.CreateType();
            var methodInfo = type.GetMethod("Foo", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.IsTrue(methodInfo.IsPrivate);
        }

        [Test]
        public void ProtectedMethodIsProtected()
        {
            var assemblyBuilder = ReflectionAssemblyBuilder.Create("ProtectedMethodIsProtectedAssembly");
            var typeBuilder = assemblyBuilder.DefineType("ProtectedMethodIsProtected");
            var method = typeBuilder.DefineMethod("Foo", typeof(void), EmitVisibility.Protected);
            method.Il.Emit(EmitOpCodes.Ret);

            var type = typeBuilder.CreateType();
            var methodInfo = type.GetMethod("Foo", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.IsTrue((methodInfo.Attributes & MethodAttributes.Family) == MethodAttributes.Family);
        }

        [Test]
        public void InternalMethodIsInternal()
        {
            var assemblyBuilder = ReflectionAssemblyBuilder.Create("InternalMethodIsInternalAssembly");
            var typeBuilder = assemblyBuilder.DefineType("InternalMethodIsInternal");
            var method = typeBuilder.DefineMethod("Foo", typeof(void), EmitVisibility.Internal);
            method.Il.Emit(EmitOpCodes.Ret);

            var type = typeBuilder.CreateType();
            var methodInfo = type.GetMethod("Foo", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.IsTrue((methodInfo.Attributes & MethodAttributes.Assembly) == MethodAttributes.Assembly);
        }

        [Test]
        public void StaticMethodIsStatic()
        {
            var assemblyBuilder = ReflectionAssemblyBuilder.Create("StaticMethodIsStaticAssembly");
            var typeBuilder = assemblyBuilder.DefineType("StaticMethodIsStatic");
            var method = typeBuilder.DefineMethod("Foo", typeof(void), EmitVisibility.Public, isStatic: true);
            method.Il.Emit(EmitOpCodes.Ret);

            var type = typeBuilder.CreateType();
            var methodInfo = type.GetMethod("Foo");

            Assert.IsTrue(methodInfo.IsStatic);
        }
    }
}