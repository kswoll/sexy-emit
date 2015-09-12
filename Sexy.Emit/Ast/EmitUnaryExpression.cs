namespace Sexy.Emit.Ast
{
    public class EmitUnaryExpression : EmitExpression
    {
        public EmitUnaryOperator Operator { get; }
        public EmitExpression Operand { get; }

        public EmitUnaryExpression(EmitUnaryOperator @operator, EmitExpression operand)
        {
            Operator = @operator;
            Operand = operand;
        }

        public override void Compile(EmitCompilerContext context, IEmitIl il)
        {
            switch (Operator)
            {
                case EmitUnaryOperator.BooleanNot:
                    Operand.Compile(context, il);
                    il.Emit(EmitOpCodes.Ldc_I4_0);
                    il.Emit(EmitOpCodes.Ceq);
                    break;
                case EmitUnaryOperator.BitwiseNot:
                    Operand.Compile(context, il);
                    il.Emit(EmitOpCodes.Not);
                    break;
                case EmitUnaryOperator.Minus:
                    Operand.Compile(context, il);
                    il.Emit(EmitOpCodes.Neg);
                    break;
                case EmitUnaryOperator.Plus:
                    Operand.Compile(context, il);
                    break;
                case EmitUnaryOperator.PostfixDecrement:
                {
                    Operand.Compile(context, il);

                    var local = il.DeclareLocal(Operand.GetType(context.TypeSystem));
                    il.Emit(EmitOpCodes.Stloc, local);

                    ((IEmitReferenceExpression)Operand).CompileAssignment(context, il, () =>
                    {
                        il.Emit(EmitOpCodes.Ldloc, local);
                        il.Emit(EmitOpCodes.Ldc_I4_1);
                        il.Emit(EmitOpCodes.Sub);
                    });

                    il.Emit(EmitOpCodes.Pop);
                    il.Emit(EmitOpCodes.Ldloc, local);

                    break;                    
                }
                case EmitUnaryOperator.PostfixIncrement:
                {
                    Operand.Compile(context, il);

                    var local = il.DeclareLocal(Operand.GetType(context.TypeSystem));
                    il.Emit(EmitOpCodes.Stloc, local);

                    ((IEmitReferenceExpression)Operand).CompileAssignment(context, il, () =>
                    {
                        il.Emit(EmitOpCodes.Ldloc, local);
                        il.Emit(EmitOpCodes.Ldc_I4_1);
                        il.Emit(EmitOpCodes.Add);
                    });

                    il.Emit(EmitOpCodes.Pop);
                    il.Emit(EmitOpCodes.Ldloc, local);

                    break;                    
                }
                case EmitUnaryOperator.PrefixDecrement:
                {
                    Operand.Compile(context, il);
                    il.Emit(EmitOpCodes.Ldc_I4_1);
                    il.Emit(EmitOpCodes.Sub);

                    var local = il.DeclareLocal(Operand.GetType(context.TypeSystem));
                    il.Emit(EmitOpCodes.Stloc, local);

                    ((IEmitReferenceExpression)Operand).CompileAssignment(context, il, () =>
                    {
                        il.Emit(EmitOpCodes.Ldloc, local);
                    });
                    break;                    
                }
                case EmitUnaryOperator.PrefixIncrement:
                {
                    Operand.Compile(context, il);
                    il.Emit(EmitOpCodes.Ldc_I4_1);
                    il.Emit(EmitOpCodes.Add);

                    var local = il.DeclareLocal(Operand.GetType(context.TypeSystem));
                    il.Emit(EmitOpCodes.Stloc, local);

                    ((IEmitReferenceExpression)Operand).CompileAssignment(context, il, () =>
                    {
                        il.Emit(EmitOpCodes.Ldloc, local);
                    });
                    break;                    
                }
            }
        }

        public override IEmitType GetType(IEmitTypeSystem typeSystem)
        {
            return Operand.GetType(typeSystem);
        }
    }
}
