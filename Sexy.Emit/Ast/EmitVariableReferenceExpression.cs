using System;

namespace Sexy.Emit.Ast
{
    public class EmitVariableReferenceExpression : EmitExpression, IEmitReferenceExpression
    {
        public EmitVariable Variable { get; }

        public EmitVariableReferenceExpression(EmitVariable variable)
        {
            Variable = variable;
        }

        public override void Compile(EmitCompilerContext context, EmitIl il)
        {
            var local = Variable.GetData(context);
            il.Emit(EmitOpCodes.Ldloc, local);
        }

        public void CompileAssignment(EmitCompilerContext context, EmitIl il, Action compileValue)
        {
            var local = Variable.GetData(context);
            compileValue();
            il.Emit(EmitOpCodes.Dup);       // We want to leave the assigned value on the stack, since assignment is an expression
            il.Emit(EmitOpCodes.Stloc, local);
        }

        public override EmitType GetExpressionType()
        {
            return Variable.Type;
        }
    }
}
