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
            var provider = new ReflectionProvider();
            var assemblyBuilder = new EmitAssemblyBuilder("TestAssembly");
            var typeBuilder = assemblyBuilder.DefineType("", "TestClass");
            typeBuilder.DefineField("foo", typeof(string));
            var type = provider.Compile(assemblyBuilder).GetType("TestClass");
            var instance = Activator.CreateInstance(type);
            var field = type.GetField("foo");

            Assert.IsNull(field.GetValue(instance));
            field.SetValue(instance, "bar");
            Assert.AreEqual("bar", field.GetValue(instance));
        }

        [Test]
        public void StaticFieldIsStatic()
        {
            var provider = new ReflectionProvider();
            var assemblyBuilder = new EmitAssemblyBuilder("TestAssembly");
            var typeBuilder = assemblyBuilder.DefineType("", "TestClass");
            typeBuilder.DefineField("foo", typeof(string), isStatic: true);
            var type = provider.Compile(assemblyBuilder).GetType("TestClass");

            var field = type.GetField("foo");
            Assert.IsTrue(field.IsStatic);
        }

        [Test]
        public void MethodReturnsString()
        {
            var provider = new ReflectionProvider();
            var assemblyBuilder = new EmitAssemblyBuilder("TestAssembly");
            var typeBuilder = assemblyBuilder.DefineType("", "TestClass");
            var method = typeBuilder.DefineMethod("Foo", typeof(string));
            method.Il.Emit(EmitOpCodes.Ldstr, "bar");
            method.Il.Emit(EmitOpCodes.Ret);
            var type = provider.Compile(assemblyBuilder).GetType("TestClass");

            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Foo");

            var result = (string)methodInfo.Invoke(instance, null);
            Assert.AreEqual("bar", result);
        }

        [Test]
        public void PrivateMethodIsPrivate()
        {
            var provider = new ReflectionProvider();
            var assemblyBuilder = new EmitAssemblyBuilder("TestAssembly");
            var typeBuilder = assemblyBuilder.DefineType("", "TestClass");
            var method = typeBuilder.DefineMethod("Foo", typeof(void), EmitVisibility.Private);
            method.Il.Emit(EmitOpCodes.Ret);
            var type = provider.Compile(assemblyBuilder).GetType("TestClass");

            var methodInfo = type.GetMethod("Foo", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.IsTrue(methodInfo.IsPrivate);
        }

        [Test]
        public void ProtectedMethodIsProtected()
        {
            var provider = new ReflectionProvider();
            var assemblyBuilder = new EmitAssemblyBuilder("TestAssembly");
            var typeBuilder = assemblyBuilder.DefineType("", "TestClass");
            var method = typeBuilder.DefineMethod("Foo", typeof(void), EmitVisibility.Protected);
            method.Il.Emit(EmitOpCodes.Ret);
            var type = provider.Compile(assemblyBuilder).GetType("TestClass");

            var methodInfo = type.GetMethod("Foo", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.IsTrue((methodInfo.Attributes & MethodAttributes.Family) == MethodAttributes.Family);
        }

        [Test]
        public void InternalMethodIsInternal()
        {
            var provider = new ReflectionProvider();
            var assemblyBuilder = new EmitAssemblyBuilder("TestAssembly");
            var typeBuilder = assemblyBuilder.DefineType("", "TestClass");
            var method = typeBuilder.DefineMethod("Foo", typeof(void), EmitVisibility.Internal);
            method.Il.Emit(EmitOpCodes.Ret);
            var type = provider.Compile(assemblyBuilder).GetType("TestClass");

            var methodInfo = type.GetMethod("Foo", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.IsTrue((methodInfo.Attributes & MethodAttributes.Assembly) == MethodAttributes.Assembly);
        }

        [Test]
        public void StaticMethodIsStatic()
        {
            var provider = new ReflectionProvider();
            var assemblyBuilder = new EmitAssemblyBuilder("TestAssembly");
            var typeBuilder = assemblyBuilder.DefineType("", "TestClass");
            var method = typeBuilder.DefineMethod("Foo", typeof(void), EmitVisibility.Public, isStatic: true);
            method.Il.Emit(EmitOpCodes.Ret);
            var type = provider.Compile(assemblyBuilder).GetType("TestClass");

            var methodInfo = type.GetMethod("Foo");

            Assert.IsTrue(methodInfo.IsStatic);
        }

        [Test]
        public void ClassExtendsBaseType()
        {
            var provider = new ReflectionProvider();
            var assemblyBuilder = new EmitAssemblyBuilder("TestAssembly");
            var typeBuilder = assemblyBuilder.DefineType("", "TestClass", baseType: typeof(CustomBaseType));
            var method = typeBuilder.DefineMethod("Foo", typeof(void), EmitVisibility.Public, isStatic: true);
            method.Il.Emit(EmitOpCodes.Ret);
            var type = provider.Compile(assemblyBuilder).GetType("TestClass");

            Assert.IsTrue(typeof(CustomBaseType).IsAssignableFrom(type));
        }

        public class CustomBaseType
        {
        }
    }
}