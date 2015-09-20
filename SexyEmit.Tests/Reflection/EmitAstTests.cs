using System;
using System.Linq;
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
        private MethodInfo CreateMethod(Action<EmitBlockStatement> ilGenerator, Type baseType = null, Action<IEmitMethodBuilder> methodInstrumenter = null)
        {
            var assemblyBuilder = ReflectionAssemblyBuilder.Create("MethodAssembly");
            var typeBuilder = assemblyBuilder.DefineType("Method");
            var method = typeBuilder.DefineMethod("Foo", typeof(object), isStatic: true);
            methodInstrumenter?.Invoke(method);
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


        [Test]
        public void BooleanAndShortCircuitsTrue()
        {
            ShortCircuitsData.Reset();
            var returnTrue = typeof(ShortCircuitsData).GetMethod("ReturnTrue");
            var returnFalse = typeof(ShortCircuitsData).GetMethod("ReturnFalse");
            var method = CreateMethod(block => block.Return(returnFalse.Call().BooleanAnd(returnTrue.Call())));
            var result = (bool)method.Invoke(null, null);
            Assert.AreEqual(1, ShortCircuitsData.NumberOfCallsToReturnFalse);
            Assert.AreEqual(0, ShortCircuitsData.NumberOfCallsToReturnTrue);
            Assert.IsFalse(result);
        }

        [Test]
        public void BooleanAndShortCircuitsFalse()
        {
            ShortCircuitsData.Reset();
            var returnTrue = typeof(ShortCircuitsData).GetMethod("ReturnTrue");
            var returnFalse = typeof(ShortCircuitsData).GetMethod("ReturnFalse");
            var method = CreateMethod(block => block.Return(returnTrue.Call().BooleanAnd(returnFalse.Call())));
            var result = (bool)method.Invoke(null, null);
            Assert.AreEqual(1, ShortCircuitsData.NumberOfCallsToReturnFalse);
            Assert.AreEqual(1, ShortCircuitsData.NumberOfCallsToReturnTrue);
            Assert.IsFalse(result);
        }

        [Test]
        public void BooleanOrShortCircuitsTrue()
        {
            ShortCircuitsData.Reset();
            var returnTrue = typeof(ShortCircuitsData).GetMethod("ReturnTrue");
            var returnFalse = typeof(ShortCircuitsData).GetMethod("ReturnFalse");
            var method = CreateMethod(block => block.Return(returnTrue.Call().BooleanOr(returnFalse.Call())));
            var result = (bool)method.Invoke(null, null);
            Assert.AreEqual(1, ShortCircuitsData.NumberOfCallsToReturnTrue);
            Assert.AreEqual(0, ShortCircuitsData.NumberOfCallsToReturnFalse);
            Assert.IsTrue(result);
        }

        [Test]
        public void BooleanOrShortCircuitsFalse()
        {
            ShortCircuitsData.Reset();
            var returnTrue = typeof(ShortCircuitsData).GetMethod("ReturnTrue");
            var returnFalse = typeof(ShortCircuitsData).GetMethod("ReturnFalse");
            var method = CreateMethod(block => block.Return(returnFalse.Call().BooleanOr(returnTrue.Call())));
            var result = (bool)method.Invoke(null, null);
            Assert.AreEqual(1, ShortCircuitsData.NumberOfCallsToReturnTrue);
            Assert.AreEqual(1, ShortCircuitsData.NumberOfCallsToReturnFalse);
            Assert.IsTrue(result);
        }

        public static class ShortCircuitsData 
        {
            public static int NumberOfCallsToReturnTrue { get; set; }
            public static int NumberOfCallsToReturnFalse { get; set; }

            public static void Reset()
            {
                NumberOfCallsToReturnFalse = 0;
                NumberOfCallsToReturnTrue = 0;
            }

            public static bool ReturnTrue()
            {
                NumberOfCallsToReturnTrue++;
                return true;
            }

            public static bool ReturnFalse()
            {
                NumberOfCallsToReturnFalse++;
                return false;
            }
        }

        [Test]
        public void InstantiateClass()
        {
            var method = CreateMethod(block => block.Return(typeof(InstanceClass).GetConstructor(new Type[0]).New()));
            var result = method.Invoke(null, null);
            Assert.IsTrue(result is InstanceClass);
        }

        public class InstanceClass
        {
        }

        [Test]
        public void InstantiateClassWithConstructor()
        {
            var method = CreateMethod(block => block.Return(typeof(InstanceClassWithConstructor).GetConstructors()[0].New(1, "foo")));
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
            var method = CreateMethod(block => block.Return(typeof(Struct).New()));
            var result = method.Invoke(null, null);
            Assert.IsTrue(result is Struct);
        }

        public struct Struct
        {
        }

        [Test]
        public void InstantiateStructWithConstructor()
        {
            var method = CreateMethod(block => block.Return(typeof(StructWithConstructor).GetConstructors()[0].New("foo")));
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
            var method = CreateMethod(block => block.Return(typeof(StaticMethodClass).GetMethod("Mirror").Call("foo")));
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

        [Test]
        public void InvokeInstanceMethod()
        {
            var x = 5;
            var y = +x;

            var type = typeof(InstanceMethodClass);
            var method = CreateMethod(block => block.Return(type.New().Call(type.GetMethod("Mirror"), "foo")));
            var result = (string)method.Invoke(null, null);
            Assert.AreEqual("foo", result);            
        }

        public class InstanceMethodClass
        {
            public string Mirror(string s)
            {
                return s;
            }
        }

        [Test]
        public void BooleanNot()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(false).BooleanNot()));
            var result = (bool)method.Invoke(null, null);
            Assert.IsTrue(result);
        }

        [Test]
        public void BitwiseNot()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(34754935).BitwiseNot()));
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(result, ~34754935);
        }

        [Test]
        public void Minus()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(123).Minus()));
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(result, -123);
        }

        [Test]
        public void Plus()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(123).Plus()));
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(result, 123);
        }

        [Test]
        public void PreIncrement()
        {
            var tupleMethod = typeof(Tuple).GetMethods().Where(x => x.GetGenericArguments().Length == 2).Single().MakeGenericMethod(typeof(int), typeof(int));
            var method = CreateMethod(block =>
            {
                var variable = block.Declare(typeof(int));
                block.Express(variable.Assign(1));
                block.Return(tupleMethod.Call(variable.PreIncrement(), variable));
            });
            var result = (Tuple<int, int>)method.Invoke(null, null);
            Assert.AreEqual(2, result.Item1);
            Assert.AreEqual(2, result.Item2);
        }

        [Test]
        public void PreDecrement()
        {
            var tupleMethod = typeof(Tuple).GetMethods().Where(x => x.GetGenericArguments().Length == 2).Single().MakeGenericMethod(typeof(int), typeof(int));
            var method = CreateMethod(block =>
            {
                var variable = block.Declare(typeof(int));
                block.Express(variable.Assign(2));
                block.Return(tupleMethod.Call(variable.PreDecrement(), variable));
            });
            var result = (Tuple<int, int>)method.Invoke(null, null);
            Assert.AreEqual(1, result.Item1);
            Assert.AreEqual(1, result.Item2);
        }

        [Test]
        public void PostIncrement()
        {
            var tupleMethod = typeof(Tuple).GetMethods().Where(x => x.GetGenericArguments().Length == 2).Single().MakeGenericMethod(typeof(int), typeof(int));
            var method = CreateMethod(block =>
            {
                var variable = block.Declare(typeof(int));
                block.Express(variable.Assign(1));
                block.Return(tupleMethod.Call(variable.PostIncrement(), variable));
            });
            var result = (Tuple<int, int>)method.Invoke(null, null);
            Assert.AreEqual(1, result.Item1);
            Assert.AreEqual(2, result.Item2);
        }

        [Test]
        public void PostDecrement()
        {
            var tupleMethod = typeof(Tuple).GetMethods().Where(x => x.GetGenericArguments().Length == 2).Single().MakeGenericMethod(typeof(int), typeof(int));
            var method = CreateMethod(block =>
            {
                var variable = block.Declare(typeof(int));
                block.Express(variable.Assign(2));
                block.Return(tupleMethod.Call(variable.PostDecrement(), variable));
            });
            var result = (Tuple<int, int>)method.Invoke(null, null);
            Assert.AreEqual(2, result.Item1);
            Assert.AreEqual(1, result.Item2);
        }

        [Test]
        public void CastIntToByte()
        {
            var expected = 12543;
            var method = CreateMethod(block => block.Return(EmitAst.Literal(12543).Cast(typeof(byte))));
            var result = (byte)method.Invoke(null, null);
            Assert.AreEqual((byte)expected, result);
        }

        [Test]
        public void CastIntToShort()
        {
            var expected = 1254323423;
            var method = CreateMethod(block => block.Return(EmitAst.Literal(1254323423).Cast(typeof(short))));
            var result = (short)method.Invoke(null, null);
            Assert.AreEqual((short)expected, result);
        }

        [Test]
        public void CastIntToInt()
        {
            var expected = 1254323423;
            var method = CreateMethod(block => block.Return(EmitAst.Literal(1254323423).Cast(typeof(int))));
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CastIntToFloat()
        {
            var expected = 123F;
            var method = CreateMethod(block => block.Return(EmitAst.Literal(123).Cast(typeof(float))));
            var result = (float)method.Invoke(null, null);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CastIntToDouble()
        {
            var expected = 1233223523532D;
            var method = CreateMethod(block => block.Return(EmitAst.Literal(1233223523532).Cast(typeof(double))));
            var result = (double)method.Invoke(null, null);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CastIntToLong()
        {
            var expected = 234233L;
            var method = CreateMethod(block => block.Return(EmitAst.Literal(234233).Cast(typeof(long))));
            var result = (long)method.Invoke(null, null);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CastLongToInt()
        {
            var expected = 2342332342342334L;
            var method = CreateMethod(block => block.Return(EmitAst.Literal(2342332342342334L).Cast(typeof(int))));
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual((int)expected, result);
        }

        [Test]
        public void CastDoubleToInt()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(4.5D).Cast(typeof(int))));
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(4, result);
        }

        [Test]
        public void CastFloatToInt()
        {
            var method = CreateMethod(block => block.Return(EmitAst.Literal(4.5F).Cast(typeof(int))));
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(4, result);
        }

        [Test]
        public void ForLoop()
        {
            var method = CreateMethod(block =>
            {
                var otherVariable = block.Declare(typeof(int));
                block.Express(otherVariable.Assign(EmitAst.Literal(1)));
                var variable = EmitAst.Declare(new ReflectionType(typeof(int)));
                block.Statements.Add(EmitAst.For(variable, EmitAst.LessThan(variable, EmitAst.Literal(5)), variable.AddAssign(1).Express(), otherVariable.AddAssign(1).Express()));
                block.Return(otherVariable);
            });
            var result = (int)method.Invoke(null, null);
            Assert.AreEqual(6, result);
        }

        [Test]
        public void IfTrue()
        {
            var method = CreateMethod(block => block.If(true, EmitAst.Return("foo"), EmitAst.Return("bar")));
            var result = (string)method.Invoke(null, null);
            Assert.AreEqual("foo", result);
        }

        [Test]
        public void IfFalse()
        {
            var method = CreateMethod(block => block.If(false, EmitAst.Return("foo"), EmitAst.Return("bar")));
            var result = (string)method.Invoke(null, null);
            Assert.AreEqual("bar", result);
        }

        [Test]
        public void IfOnlyTrue()
        {
            var method = CreateMethod(block =>
            {
                block.If(true, EmitAst.Return("foo"));
                block.Return("bar");
            });
            var result = (string)method.Invoke(null, null);
            Assert.AreEqual("foo", result);            
        }

        [Test]
        public void IfOnlyFalse()
        {
            var method = CreateMethod(block =>
            {
                block.If(false, EmitAst.Return("foo"));
                block.Return("bar");
            });
            var result = (string)method.Invoke(null, null);
            Assert.AreEqual("bar", result);            
        }

        [Test]
        public void IfNoReturnTrue()
        {
            var method = CreateMethod(block =>
            {
                var variable = block.Declare(typeof(string));
                block.If(true, variable.Assign("foo").Express(), variable.Assign("bar").Express());
                block.Return(variable);
            });
            var result = (string)method.Invoke(null, null);
            Assert.AreEqual("foo", result);            
        }

        [Test]
        public void IfNoReturnFalse()
        {
            var method = CreateMethod(block =>
            {
                var variable = block.Declare(typeof(string));
                block.If(false, variable.Assign("foo").Express(), variable.Assign("bar").Express());
                block.Return(variable);
            });
            var result = (string)method.Invoke(null, null);
            Assert.AreEqual("bar", result);            
        }

        [Test]
        public void NewSingleDimensionArray()
        {
            var method = CreateMethod(block => block.Return(typeof(string).NewArray(5)));
            var result = (string[])method.Invoke(null, null);
            Assert.AreEqual(5, result.Length);
        }

        [Test]
        public void NewTwoDimensionalArray()
        {
            var method = CreateMethod(block => block.Return(typeof(string).NewArray(5, 4)));
            var result = (string[,])method.Invoke(null, null);
            Assert.AreEqual(4, result.GetUpperBound(0));
            Assert.AreEqual(3, result.GetUpperBound(1));
        }

        [Test]
        public void SingleDimensionArrayInitializer()
        {
            var method = CreateMethod(block => block.Return(typeof(int).NewArrayFrom(5, 4)));
            var result = (int[])method.Invoke(null, null);
            Assert.AreEqual(5, result[0]);
            Assert.AreEqual(4, result[1]);
        }

        [Test]
        public void TwoDimensionArrayInitializer()
        {
            var source = new[,] {{1, 2}, {3, 4}};
            Assert.AreEqual(1, source[0, 0]);
            Assert.AreEqual(2, source[0, 1]);
            Assert.AreEqual(3, source[1, 0]);
            Assert.AreEqual(4, source[1, 1]);

            var method = CreateMethod(block => block.Return(typeof(int).NewArrayFrom(source)));
            var result = (int[,])method.Invoke(null, null);
            Assert.AreEqual(1, result[0, 0]);
            Assert.AreEqual(2, result[0, 1]);
            Assert.AreEqual(3, result[1, 0]);
            Assert.AreEqual(4, result[1, 1]);
        }

        [Test]
        public void ThreeDimensionArrayInitializer()
        {
            var source = new[,,] {{{1, 2}, {3, 4}},{{5, 6}, {7, 8}}};
            Assert.AreEqual(1, source[0, 0, 0]);
            Assert.AreEqual(2, source[0, 0, 1]);
            Assert.AreEqual(3, source[0, 1, 0]);
            Assert.AreEqual(4, source[0, 1, 1]);
            Assert.AreEqual(5, source[1, 0, 0]);
            Assert.AreEqual(6, source[1, 0, 1]);
            Assert.AreEqual(7, source[1, 1, 0]);
            Assert.AreEqual(8, source[1, 1, 1]);

            var method = CreateMethod(block => block.Return(typeof(int).NewArrayFrom(source)));
            var result = (int[,,])method.Invoke(null, null);
            Assert.AreEqual(1, result[0, 0, 0]);
            Assert.AreEqual(2, result[0, 0, 1]);
            Assert.AreEqual(3, result[0, 1, 0]);
            Assert.AreEqual(4, result[0, 1, 1]);
            Assert.AreEqual(5, result[1, 0, 0]);
            Assert.AreEqual(6, result[1, 0, 1]);
            Assert.AreEqual(7, result[1, 1, 0]);
            Assert.AreEqual(8, result[1, 1, 1]);
        }

/*
        [Test]
        public void ReturnParameter()
        {
            IEmitParameter parameter = null;
            var method = CreateMethod(block => block.Return(parameter))
        }
*/

        public void Foo()
        {
            var x = new object[1, 2];
            x[0, 0] = 5;
        }

/*
        [Test]
        public void Foreach()
        {
            var method = CreateMethod(block =>
            {
                block.Foreach();
                var variable = block.Declare(typeof(string));
                block.If(false, variable.Assign("foo").Express(), variable.Assign("bar").Express());
                block.Return(variable);
            });
            var result = (string)method.Invoke(null, null);
            Assert.AreEqual("bar", result);                        
        }
*/
    }
}