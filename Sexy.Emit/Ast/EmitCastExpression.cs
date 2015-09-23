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
            var operandType = Operand.GetType(context.TypeSystem);

            var typeInt = context.TypeSystem.GetType(typeof(int));
            var typeUint = context.TypeSystem.GetType(typeof(uint));
            var typeShort = context.TypeSystem.GetType(typeof(short));
            var typeUshort = context.TypeSystem.GetType(typeof(ushort));
            var typeByte = context.TypeSystem.GetType(typeof(byte));
            var typeSbyte = context.TypeSystem.GetType(typeof(sbyte));
            var typeLong = context.TypeSystem.GetType(typeof(long));
            var typeUlong = context.TypeSystem.GetType(typeof(ulong));
            var typeDouble = context.TypeSystem.GetType(typeof(double));
            var typeFloat = context.TypeSystem.GetType(typeof(float));
            var typeDecimal = context.TypeSystem.GetType(typeof(decimal));
            var typeBool = context.TypeSystem.GetType(typeof(bool));

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

        public override EmitType GetType(IEmitTypeSystem typeSystem)
        {
            return Type;
        }
    }
}
