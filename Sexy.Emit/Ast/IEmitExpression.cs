﻿namespace Sexy.Emit.Ast
{
    public interface IEmitExpression
    {
        void Compile(EmitCompilerContext context, IEmitIl il);
    }
}
