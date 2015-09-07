namespace Sexy.Emit.Ast
{
    public class EmitExpressionStatement : EmitStatement
    {
        public EmitExpression Expression { get; }

        public EmitExpressionStatement(EmitExpression expression)
        {
            Expression = expression;
        }

        public override void Compile(EmitCompilerContext context, IEmitIl il)
        {
            Expression.Compile(context, il);
            il.Emit(EmitOpCodes.Pop);
        }
    }
}
