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
        private MethodInfo CreateMethod(Action<EmitBlockStatement> ilGenerator, Type baseType = null)
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
        public void LiteralString()
        {
            var method = CreateMethod(block => block.Return("foo"));
            var result = (string)method.Invoke(null, null);
            Assert.AreEqual("foo", result);
        }

        [Test]
        public void LiteralInt()
        {
            var method = CreateMethod(block => block.Return(5));
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(5, result);
        }

        [Test]
        public void LiteralLong()
        {
            var method = CreateMethod(block => block.Return(5L));
            var result = (long)method.Invoke(null, null);
            Assert.AreEqual(5, result);
        }

        [Test]
        public void LiteralNull()
        {
            var method = CreateMethod(block => block.ReturnNull());
            var result = method.Invoke(null, null);
            Assert.IsNull(result);
        }

        [Test]
        public void LiteralTrue()
        {
            var method = CreateMethod(block => block.Return(true));
            var result = (bool)method.Invoke(null, null);
            Assert.IsTrue(result);
        }

        [Test]
        public void LiteralFloat()
        {
            var method = CreateMethod(block => block.Return(1.2f));
            var result = (float)method.Invoke(null, null);
            Assert.AreEqual(1.2f, result);
        }

        [Test]
        public void LiteralDouble()
        {
            var method = CreateMethod(block => block.Return(1.2d));
            var result = (double)method.Invoke(null, null);
            Assert.AreEqual(1.2d, result);
        }

        [Test]
        public void AddTwoInts()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(5).Add(4)));
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(9, result);
        }

        [Test]
        public void AddTwoFloats()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(5.1f).Add(4.2f)));
            var result = (float)method.Invoke(null, null);
            Assert.IsTrue(Math.Abs(9.3f - result) < .0001f);
        }

        [Test]
        public void AddTwoDoubles()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(5.1d).Add(4.2d)));
            var result = (double)method.Invoke(null, null);
            Assert.IsTrue(Math.Abs(9.3d - result) < .0001d);
        }

        [Test]
        public void AddDoubleAndFloat()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(5.1d).Add(4.2f)));
            var result = (double)method.Invoke(null, null);
            Assert.IsTrue(Math.Abs(9.3d - result) < .0001d);
        }

        [Test]
        public void AddDoubleAndInt()
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
        public void BitwiseAnd()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(7).BitwiseAnd(4 | 1)));
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(5, result);
        }

        [Test]
        public void BitwiseOr()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(1 | 2).BitwiseOr(4 | 8)));
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(15, result);
        }

        [Test]
        public void ShiftLeft()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(1).ShiftLeft(2)));
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(4, result);            
        }

        [Test]
        public void ShiftRight()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(4).ShiftRight(2)));
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

        [Test]
        public void BitwiseOrAssign()
        {
            var method = CreateMethod(block =>
            {
                var variable = block.Declare(typeof(int));
                block.Express(variable.Assign(1 | 2));
                block.Express(variable.BitwiseOrAssign(4 | 8));
                block.Return(variable);
            });
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(15, result);                       
        }

        [Test]
        public void BitwiseAndAssign()
        {
            var method = CreateMethod(block =>
            {
                var variable = block.Declare(typeof(int));
                block.Express(variable.Assign(7));
                block.Express(variable.BitwiseAndAssign(4 | 1));
                block.Return(variable);
            });
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(5, result);                       
        }

        [Test]
        public void ShiftLeftAssign()
        {
            var method = CreateMethod(block =>
            {
                var variable = block.Declare(typeof(int));
                block.Express(variable.Assign(1));
                block.Express(variable.ShiftLeftAssign(2));
                block.Return(variable);
            });
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(4, result);                       
        }

        [Test]
        public void ShiftRightAssign()
        {
            var method = CreateMethod(block =>
            {
                var variable = block.Declare(typeof(int));
                block.Express(variable.Assign(4));
                block.Express(variable.ShiftRightAssign(2));
                block.Return(variable);
            });
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(1, result);                       
        }

        [Test]
        public void GreaterThan()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(5).GreaterThan(4)));
            var result = (bool)method.Invoke(null, null);
            Assert.IsTrue(result);
        }

        [Test]
        public void LessThan()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(5).LessThan(6)));
            var result = (bool)method.Invoke(null, null);
            Assert.IsTrue(result);
        }

        [Test]
        public void GreaterThanOrEqualWhenEqual()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(5).GreaterThanOrEqual(5)));
            var result = (bool)method.Invoke(null, null);
            Assert.IsTrue(result);            
        }

        [Test]
        public void GreaterThanOrEqualWhenGreater()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(5).GreaterThanOrEqual(4)));
            var result = (bool)method.Invoke(null, null);
            Assert.IsTrue(result);            
        }

        [Test]
        public void LessThanOrEqualWhenEqual()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(5).LessThanOrEqual(5)));
            var result = (bool)method.Invoke(null, null);
            Assert.IsTrue(result);            
        }

        [Test]
        public void LessThanOrEqualWhenLess()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(5).LessThanOrEqual(6)));
            var result = (bool)method.Invoke(null, null);
            Assert.IsTrue(result);            
        }


// For later, when other functions are complete
//        [Test]
//        public void BooleanAndShortCircuits()
//        {
//            var method = CreateMethod(block => block.Return(EmitAst.Literal(7).Modulus(3)));
//        }

        [Test]
        public void InstantiateClass()
        {
            var method = CreateMethod(block => block.Return(typeof(InstanceClass).GetConstructor(new Type[0]).Instantiation()));
            var result = method.Invoke(null, null);
            Assert.IsTrue(result is InstanceClass);
        }

        public class InstanceClass
        {
        }

        [Test]
        public void InstantiateClassWithConstructor()
        {
            var method = CreateMethod(block => block.Return(typeof(InstanceClassWithConstructor).GetConstructors()[0].Instantiation(1, "foo")));
            var result = (InstanceClassWithConstructor)method.Invoke(null, null);
            Assert.AreEqual(1, result.IntProperty);
            Assert.AreEqual("foo", result.StringProperty);
        }

        public class InstanceClassWithConstructor
        {
            public int IntProperty { get; }
            public string StringProperty { get; }

            public InstanceClassWithConstructor(int intProperty, string stringProperty)
            {
                IntProperty = intProperty;
                StringProperty = stringProperty;
            }
        }

        [Test]
        public void InstantiateStruct()
        {
            var method = CreateMethod(block => block.Return(typeof(Struct).Instantiation()));
            var result = method.Invoke(null, null);
            Assert.IsTrue(result is Struct);
        }

        public struct Struct
        {
        }

        [Test]
        public void InstantiateStructWithConstructor()
        {
            var method = CreateMethod(block => block.Return(typeof(StructWithConstructor).GetConstructors()[0].Instantiation("foo")));
            var result = (StructWithConstructor)method.Invoke(null, null);
            Assert.AreEqual("foo", result.StringProperty);
        }

        public struct StructWithConstructor
        {
            public string StringProperty { get; }

            public StructWithConstructor(string stringProperty)
            {
                StringProperty = stringProperty;
            }
        }

        [Test]
        public void InvokeStaticMethod()
        {
            var method = CreateMethod(block => block.Return(typeof(StaticMethodClass).GetMethod("Mirror").Invocation("foo")));
            var result = (string)method.Invoke(null, null);
            Assert.AreEqual("foo", result);
        }

        public static class StaticMethodClass
        {
            public static string Mirror(string s)
            {
                return s;
            }
        }
    }
}