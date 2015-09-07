using System;

namespace Sexy.Emit.Ast
{
    public interface IEmitReferenceExpression : IEmitExpression
    {
        void CompileAssignment(EmitCompilerContext context, IEmitIl il, Action compileValue);
    }
}
