using System;
using NUnit.Framework;
using Sexy.Emit.Reflection;

namespace SexyEmit.Tests.Reflection
{
    [TestFixture]
    public class BasicTests
    {
        [Test]
        public void SetAndGetValueFromField()
        {
            var assemblyBuilder = ReflectionAssemblyBuilder.Create("MethodReturnsStringAssembly");
            var typeBuilder = assemblyBuilder.DefineType("MethodReturnsString");
            typeBuilder.DefineField("foo", typeof(string));
            var type = typeBuilder.CreateType();
            var instance = Activator.CreateInstance(type);
            var field = type.GetField("foo");

            Assert.IsNull(field.GetValue(instance));
            field.SetValue(instance, "bar");
            Assert.AreEqual("bar", field.GetValue(instance));
        }
    }
}