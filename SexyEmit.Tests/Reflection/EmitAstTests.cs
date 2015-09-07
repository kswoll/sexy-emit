using System;
using System.Reflection;
using NUnit.Framework;
using Sexy.Emit;
using Sexy.Emit.Ast;
using Sexy.Emit.Reflection;

namespace SexyEmit.Tests.Reflection
{
    [TestFixture]
    public class EmitAstTests
    {
        private MethodInfo CreateMethod(Action<EmitBlockStatement> ilGenerator)
        {
            var assemblyBuilder = ReflectionAssemblyBuilder.Create("MethodAssembly");
            var typeBuilder = assemblyBuilder.DefineType("Method");
            var method = typeBuilder.DefineMethod("Foo", typeof(object), isStatic: true);
            var block = EmitAst.Block();
            ilGenerator(block);
            block.Compile(new EmitCompilerContext(method, new ReflectionTypeSystem()), method.Il);

            var type = typeBuilder.CreateType();
            assemblyBuilder.AssemblyBuilder.Save("assembly.dll");
            var methodInfo = type.GetMethod("Foo");

            return methodInfo;
        }

        [Test]
        public void ReturnLiteralString()
        {
            var method = CreateMethod(block => block.Return("foo"));
            var result = (string)method.Invoke(null, null);
            Assert.AreEqual("foo", result);
        }

        [Test]
        public void ReturnLiteralInt()
        {
            var method = CreateMethod(block => block.Return(5));
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(5, result);
        }

        [Test]
        public void ReturnLiteralLong()
        {
            var method = CreateMethod(block => block.Return(5L));
            var result = (long)method.Invoke(null, null);
            Assert.AreEqual(5, result);
        }

        [Test]
        public void ReturnLiteralNull()
        {
            var method = CreateMethod(block => block.ReturnNull());
            var result = method.Invoke(null, null);
            Assert.IsNull(result);
        }

        [Test]
        public void ReturnLiteralTrue()
        {
            var method = CreateMethod(block => block.Return(true));
            var result = (bool)method.Invoke(null, null);
            Assert.IsTrue(result);
        }

        [Test]
        public void ReturnLiteralFloat()
        {
            var method = CreateMethod(block => block.Return(1.2f));
            var result = (float)method.Invoke(null, null);
            Assert.AreEqual(1.2f, result);
        }

        [Test]
        public void ReturnLiteralDouble()
        {
            var method = CreateMethod(block => block.Return(1.2d));
            var result = (double)method.Invoke(null, null);
            Assert.AreEqual(1.2d, result);
        }
    }
}