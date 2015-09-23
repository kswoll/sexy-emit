using System;

namespace Sexy.Emit.Ast
{
    public interface IEmitReferenceExpression : IEmitExpression
    {
        void CompileAssignment(EmitCompilerContext context, EmitIl il, Action compileValue);
    }
}
