using System;
using Sexy.Emit.Ast;

namespace Sexy.Emit.Reflection
{
    public static class ReflectionEmitAstExtensions
    {
        public static EmitVariableDeclarationStatement Declare(this EmitBlockStatement block, Type type)
        {
            return block.Declare(new ReflectionType(type));
        }
    }
}
