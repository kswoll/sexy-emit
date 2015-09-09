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
            var block = method.Body;
            ilGenerator(block);
            method.Compile();

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

        [Test]
        public void ReturnAddTwoInts()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(5).Add(4)));
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(9, result);
        }

        [Test]
        public void ReturnAddTwoFloats()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(5.1f).Add(4.2f)));
            var result = (float)method.Invoke(null, null);
            Assert.IsTrue(Math.Abs(9.3f - result) < .0001f);
        }

        [Test]
        public void ReturnAddTwoDoubles()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(5.1d).Add(4.2d)));
            var result = (double)method.Invoke(null, null);
            Assert.IsTrue(Math.Abs(9.3d - result) < .0001d);
        }

        [Test]
        public void ReturnAddDoubleAndFloat()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(5.1d).Add(4.2f)));
            var result = (double)method.Invoke(null, null);
            Assert.IsTrue(Math.Abs(9.3d - result) < .0001d);
        }

        [Test]
        public void ReturnAddDoubleAndInt()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(5.1d).Add(4)));
            var result = (double)method.Invoke(null, null);
            Assert.IsTrue(Math.Abs(9.1d - result) < .0001d);
        }

        [Test]
        public void Subtract()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(7).Subtract(3)));
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(4, result);
        }

        [Test]
        public void Multiply()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(7).Multiply(3)));
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(21, result);
        }

        [Test]
        public void Divide()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(7).Divide(3)));
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void Modulus()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(7).Modulus(3)));
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void EqualsFalse()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(7).EqualTo(5)));
            var result = (bool)method.Invoke(null, null);
            Assert.IsFalse(result);
        }

        [Test]
        public void EqualsTrue()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(5).EqualTo(5)));
            var result = (bool)method.Invoke(null, null);
            Assert.IsTrue(result);
        }

        [Test]
        public void NotEqualsTrue()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(4).NotEqualTo(5)));
            var result = (bool)method.Invoke(null, null);
            Assert.IsTrue(result);            
        }

        [Test]
        public void Assign()
        {
            var method = CreateMethod(block =>
            {
                var variable = block.Declare(typeof(string));
                block.Express(variable.Assign("foo"));
                block.Return(variable);
            });
            var result = (string)method.Invoke(null, null);
            Assert.AreEqual("foo", result);
        }

        [Test]
        public void AddAssign()
        {
            var method = CreateMethod(block =>
            {
                var variable = block.Declare(typeof(int));
                block.Express(variable.Assign(5));
                block.Express(variable.AddAssign(4));
                block.Return(variable);
            });
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(9, result);            
        }

        [Test]
        public void SubtractAssign()
        {
            var method = CreateMethod(block =>
            {
                var variable = block.Declare(typeof(int));
                block.Express(variable.Assign(5));
                block.Express(variable.SubtractAssign(4));
                block.Return(variable);
            });
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(1, result);            
        }

        [Test]
        public void MultiplyAssign()
        {
            var method = CreateMethod(block =>
            {
                var variable = block.Declare(typeof(int));
                block.Express(variable.Assign(5));
                block.Express(variable.MultiplyAssign(4));
                block.Return(variable);
            });
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(20, result);            
        }

        [Test]
        public void DivideAssign()
        {
            var method = CreateMethod(block =>
            {
                var variable = block.Declare(typeof(int));
                block.Express(variable.Assign(12));
                block.Express(variable.DivideAssign(4));
                block.Return(variable);
            });
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(3, result);            
        }

        [Test]
        public void ModulusAssign()
        {
            var method = CreateMethod(block =>
            {
                var variable = block.Declare(typeof(int));
                block.Express(variable.Assign(11));
                block.Express(variable.ModulusAssign(4));
                block.Return(variable);
            });
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(3, result);            
        }

// For later, when other functions are complete
//        [Test]
//        public void BooleanAndShortCircuits()
//        {
//            var method = CreateMethod(block => block.Return(EmitAst.Literal(7).Modulus(3)));
//        }
    }
}