using System;
using System.Reflection;
using Sexy.Emit.Ast;

namespace Sexy.Emit.Reflection
{
    public static class ReflectionEmitAstExtensions
    {
        public static EmitVariableDeclarationStatement Declare(this EmitBlockStatement block, Type type)
        {
            return block.Declare(new ReflectionType(type));
        }

        public static EmitMethodInvocationExpression Call(this MethodInfo method, params EmitExpression[] arguments)
        {
            return new EmitMethodInvocationExpression(new ReflectionMethod(method), arguments);
        }

        public static EmitMethodInvocationExpression Call(this EmitExpression target, MethodInfo method, params EmitExpression[] arguments)
        {
            return target.Call(new ReflectionMethod(method), arguments);
        }

        public static EmitObjectCreationExpression New(this ConstructorInfo constructor, params EmitExpression[] arguments)
        {
            return new EmitObjectCreationExpression(new ReflectionConstructor(constructor), arguments);
        }

        public static EmitObjectCreationExpression New(this Type type)
        {
            if (!type.IsValueType)
            {
                var constructor = type.GetConstructor(new Type[0]);
                return constructor.New();
            }
            return new EmitObjectCreationExpression(new ReflectionType(type));
        }

        public static EmitArrayCreationExpression NewArray(this Type elementType, params EmitExpression[] length)
        {
            return new EmitArrayCreationExpression(new ReflectionType(elementType), length);
        }

        public static EmitArrayInitializerExpression NewArrayFrom(this Type elementType, params IEmitArrayElement[] elements) 
        {
            return new EmitArrayInitializerExpression(new ReflectionType(elementType), elements);
        }

        public static EmitArrayInitializerExpression NewArrayFrom(this Type elementType, params EmitExpression[] elements) 
        {
            return new EmitArrayInitializerExpression(new ReflectionType(elementType), elements);
        }

        public static EmitArrayInitializerExpression NewArrayFrom(this Type elementType, Array elements) 
        {
            return new EmitArrayInitializerExpression(new ReflectionType(elementType), (EmitArrayInitializer)elements);
        }

        public static EmitMethodInvocationExpression Invoke(this EmitExpression target, MethodInfo method, params EmitExpression[] arguments)
        {
            return new EmitMethodInvocationExpression(target, new ReflectionMethod(method), arguments);
        }

        public static EmitCastExpression Cast(this EmitExpression operand, Type type)
        {
            return new EmitCastExpression(operand, new ReflectionType(type));
        }
    }
}
