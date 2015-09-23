using System;

namespace Sexy.Emit.Ast
{
    public class EmitLiteralExpression : EmitExpression
    {
        public object Value { get; }

        public EmitLiteralExpression(int value)
        {
            Value = value;
        }

        public EmitLiteralExpression(long value)
        {
            Value = value;
        }

        public EmitLiteralExpression(float value)
        {
            Value = value;
        }

        public EmitLiteralExpression(double value)
        {
            Value = value;
        }

        public EmitLiteralExpression(bool value)
        {
            Value = value;
        }

        public EmitLiteralExpression(string value)
        {
            Value = value;
        }

        public EmitLiteralExpression()
        {
            Value = null;
        }

        public override void Compile(EmitCompilerContext context, EmitIl il)
        {
            if (Value == null)
                il.Emit(EmitOpCodes.Ldnull);
            else if (Value is int)
                il.Emit(EmitOpCodes.Ldc_I4, (int)Value);
            else if (Value is long)
                il.Emit(EmitOpCodes.Ldc_I8, (long)Value);
            else if (Value is float)
                il.Emit(EmitOpCodes.Ldc_R4, (float)Value);
            else if (Value is double)
                il.Emit(EmitOpCodes.Ldc_R8, (double)Value);
            else if (Value is bool)
                il.Emit(EmitOpCodes.Ldc_I4, (bool)Value ? 1 : 0);
            else if (Value is string)
                il.Emit(EmitOpCodes.Ldstr, (string)Value);
            else
                throw new Exception();
        }

        public override EmitType GetExpressionType()
        {
            if (Value == null)
                return typeof(void);
            else if (Value is int)
                return typeof(int);
            else if (Value is long)
                return typeof(long);
            else if (Value is float)
                return typeof(float);
            else if (Value is double)
                return typeof(double);
            else if (Value is bool)
                return typeof(bool);
            else if (Value is string)
                return typeof(string);
            else
                throw new Exception();
        }
    }
}
