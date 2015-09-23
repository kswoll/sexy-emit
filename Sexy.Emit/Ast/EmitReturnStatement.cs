namespace Sexy.Emit.Ast
{
    public class EmitReturnStatement : EmitStatement
    {
        public EmitExpression Expression { get; }

        public EmitReturnStatement(EmitExpression expression)
        {
            Expression = expression;
        }

        public override void Compile(EmitCompilerContext context, EmitIl il)
        {
            Expression.Compile(context, il);

            var expressionType = Expression.GetExpressionType();
            EmitType voidType = typeof(void);
            if (expressionType.IsValueType && !context.Method.ReturnType.IsValueType && !Equals(expressionType, voidType))
            {
                il.Emit(EmitOpCodes.Box, Expression.GetExpressionType());
            }
//            else if (!Expression.GetType(context.TypeSystem).IsValueType && !context.Method.ReturnType.IsValueType)
//            {
//                
//            }

            il.Emit(EmitOpCodes.Ret);
        }
    }
}
