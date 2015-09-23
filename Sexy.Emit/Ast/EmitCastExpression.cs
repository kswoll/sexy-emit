namespace Sexy.Emit.Ast
{
    public class EmitCastExpression : EmitExpression
    {
        public EmitExpression Operand { get; }
        public EmitType Type { get; }

        public EmitCastExpression(EmitExpression operand, EmitType type)
        {
            Operand = operand;
            Type = type;
        }

        public override void Compile(EmitCompilerContext context, EmitIl il)
        {
            Operand.Compile(context, il);
            var operandType = Operand.GetExpressionType();

            EmitType typeInt = typeof(int);
            EmitType typeUint = typeof(uint);
            EmitType typeShort = typeof(short);
            EmitType typeUshort = typeof(ushort);
            EmitType typeByte = typeof(byte);
            EmitType typeSbyte = typeof(sbyte);
            EmitType typeLong = typeof(long);
            EmitType typeUlong = typeof(ulong);
            EmitType typeDouble = typeof(double);
            EmitType typeFloat = typeof(float);
            EmitType typeDecimal = typeof(decimal);
            EmitType typeBool = typeof(bool);

            var typeIs32Bit = Equals(Type, typeByte) || Equals(Type, typeShort) || Equals(Type, typeInt) || Equals(Type, typeSbyte) || Equals(Type, typeUshort) || Equals(Type, typeUint);
            var operandIs32Bit = Equals(operandType, typeByte) || Equals(operandType, typeShort) || Equals(operandType, typeInt) || Equals(operandType, typeSbyte) || Equals(operandType, typeUshort) || Equals(operandType, typeUint);
            var typeIsFloatingPoint = Equals(Type, typeFloat) || Equals(Type, typeDouble);
            var operandIsFloatingPoint = Equals(operandType, typeFloat) || Equals(operandType, typeDouble);

            if (operandIs32Bit || operandIsFloatingPoint || Equals(operandType, typeLong))
            {
                if (Equals(Type, operandType))
                {
                    return;
                }
                if (Equals(Type, typeFloat))
                {
                    il.Emit(EmitOpCodes.Conv_R4);
                    return;
                }
                if (Equals(Type, typeDouble))
                {
                    il.Emit(EmitOpCodes.Conv_R8);
                    return;
                }
                if (Equals(Type, typeByte))
                {
                    il.Emit(EmitOpCodes.Conv_U1);
                    return;
                }
                if (Equals(Type, typeSbyte))
                {
                    il.Emit(EmitOpCodes.Conv_I1);
                    return;
                }
                if (Equals(Type, typeShort))
                {
                    il.Emit(EmitOpCodes.Conv_I2);
                    return;
                }
                if (Equals(Type, typeUshort))
                {
                    il.Emit(EmitOpCodes.Conv_U2);
                    return;
                }
                if (Equals(Type, typeInt))
                {
                    il.Emit(EmitOpCodes.Conv_I4);
                    return;
                }
                if (Equals(Type, typeUint))
                {
                    il.Emit(EmitOpCodes.Conv_U4);
                    return;
                }
                if (Equals(Type, typeLong))
                {
                    il.Emit(EmitOpCodes.Conv_I8);
                    return;
                }
                if (Equals(Type, typeUlong))
                {
                    il.Emit(EmitOpCodes.Conv_U8);
                    return;
                }
            }

            // All else fails, then:
            il.Emit(EmitOpCodes.Castclass, Type);
        }

        public override EmitType GetExpressionType()
        {
            return Type;
        }
    }
}
