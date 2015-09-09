using System;
using Sexy.Emit.OpCodes;
using Sexy.Emit.Reflection;

namespace Sexy.Emit.Ast
{
    public class EmitBinaryExpression : EmitExpression
    {
        public EmitExpression Left { get; }
        public EmitBinaryOperator Operator { get; }
        public EmitExpression Right { get; }

        public EmitBinaryExpression(EmitExpression left, EmitBinaryOperator @operator, EmitExpression right)
        {
            Left = left;
            Operator = @operator;
            Right = right;
        }

        public override void Compile(EmitCompilerContext context, IEmitIl il)
        {
            switch (Operator)
            {
                case EmitBinaryOperator.Assign:
                    ((IEmitReferenceExpression)Left).CompileAssignment(context, il, () => Right.Compile(context, il));
                    break;
                case EmitBinaryOperator.BooleanAnd:
                    Left.Compile(context, il);
                    il.Emit(EmitOpCodes.Dup);

                    var andEnd = il.DefineLabel();
                    il.Emit(EmitOpCodes.Brfalse, andEnd);
                    il.Emit(EmitOpCodes.Pop);

                    Right.Compile(context, il);
                    il.MarkLabel(andEnd);

                    break;                    
                case EmitBinaryOperator.BooleanOr:
                    Left.Compile(context, il);
                    il.Emit(EmitOpCodes.Dup);

                    var orEnd = il.DefineLabel();
                    il.Emit(EmitOpCodes.Brtrue, orEnd);
                    il.Emit(EmitOpCodes.Pop);

                    Right.Compile(context, il);
                    il.MarkLabel(orEnd);

                    break;
                case EmitBinaryOperator.NotEquals:
                    Left.Compile(context, il);
                    Right.Compile(context, il);

                    il.Emit(EmitOpCodes.Ceq);
                    il.Emit(EmitOpCodes.Ldc_I4_0);
                    il.Emit(EmitOpCodes.Ceq);
                    break;
                default:
                    var instruction = GetOpCode();
                    var isInverted = IsOperatorInverse();
                    var isAssignment = IsOperatorAssignment();

                    if (isInverted)
                    {
                        Right.Compile(context, il);                        
                        Left.Compile(context, il);
                    }
                    else
                    {
                        Left.Compile(context, il);
                        Right.Compile(context, il);
                    }

                    il.Emit(instruction);

                    if (isAssignment)
                    {
                        var local = il.DeclareLocal(typeof(bool));
                        il.Emit(EmitOpCodes.Stloc, local);
                        ((IEmitReferenceExpression)Left).CompileAssignment(context, il, () => il.Emit(EmitOpCodes.Ldloc, local));
                    }

                    break;
            }
        }

        private bool IsOperatorAssignment()
        {
            switch (Operator)
            {
                case EmitBinaryOperator.AddAssign:
                case EmitBinaryOperator.BitwiseAndAssign:
                case EmitBinaryOperator.BitwiseOrAssign:
                case EmitBinaryOperator.DivideAssign:
                case EmitBinaryOperator.ModulusAssign:
                case EmitBinaryOperator.MultiplyAssign:
                case EmitBinaryOperator.ShiftLeftAssign:
                case EmitBinaryOperator.ShiftRightAssign:
                case EmitBinaryOperator.SubtractAssign:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsOperatorInverse()
        {
            switch (Operator)
            {
                case EmitBinaryOperator.GreaterThanOrEqual:
                case EmitBinaryOperator.LessThanOrEqual:
                    return true;
                default:
                    return false;
            }
        }

        public bool IsOperatorComparison()
        {
            switch (Operator)
            {
                case EmitBinaryOperator.BooleanAnd:
                case EmitBinaryOperator.BooleanOr:
                case EmitBinaryOperator.Equals:
                case EmitBinaryOperator.GreaterThan:
                case EmitBinaryOperator.GreaterThanOrEqual:
                case EmitBinaryOperator.LessThan:
                case EmitBinaryOperator.LessThanOrEqual:
                case EmitBinaryOperator.NotEquals:
                    return true;
                default:
                    return false;
            }
        }

        private IEmitOpCodeVoid GetOpCode()
        {
            switch (Operator)
            {
                case EmitBinaryOperator.Add:
                case EmitBinaryOperator.AddAssign:
                    return EmitOpCodes.Add;
                case EmitBinaryOperator.BitwiseAnd:
                case EmitBinaryOperator.BitwiseAndAssign:
                    return EmitOpCodes.And;
                case EmitBinaryOperator.BitwiseOr:
                case EmitBinaryOperator.BitwiseOrAssign:
                    return EmitOpCodes.Or;
                case EmitBinaryOperator.Divide:
                case EmitBinaryOperator.DivideAssign:
                    return EmitOpCodes.Div;
                case EmitBinaryOperator.Equals:
                    return EmitOpCodes.Ceq;
                case EmitBinaryOperator.GreaterThan:
                case EmitBinaryOperator.LessThanOrEqual:
                    return EmitOpCodes.Cgt;
                case EmitBinaryOperator.LessThan:
                case EmitBinaryOperator.GreaterThanOrEqual:
                    return EmitOpCodes.Clt;
                case EmitBinaryOperator.Modulus:
                case EmitBinaryOperator.ModulusAssign:
                    return EmitOpCodes.Rem;
                case EmitBinaryOperator.Multiply:
                case EmitBinaryOperator.MultiplyAssign:
                    return EmitOpCodes.Mul;
                case EmitBinaryOperator.Subtract:
                case EmitBinaryOperator.SubtractAssign:
                    return EmitOpCodes.Sub;
                case EmitBinaryOperator.ShiftLeft:
                case EmitBinaryOperator.ShiftLeftAssign:
                    return EmitOpCodes.Shl;
                case EmitBinaryOperator.ShiftRight:
                case EmitBinaryOperator.ShiftRightAssign:
                    return EmitOpCodes.Shr;
                default:
                    throw new Exception();
            }            
        }

        public override IEmitType GetType(IEmitTypeSystem typeSystem)
        {
            if (IsOperatorComparison())
                return typeSystem.GetType(typeof(bool));
            else
                return Left.GetType(typeSystem);  // Eventually apply widest-type semantics
        }
    }
}
