using System;

namespace Sexy.Emit.Ast
{
    public abstract class EmitExpression : IEmitExpression, IEmitArrayElement
    {
        public abstract void Compile(EmitCompilerContext context, IEmitIl il);
        public abstract IEmitType GetType(IEmitTypeSystem typeSystem);

        public static implicit operator EmitExpression(string value)
        {
            return new EmitLiteralExpression(value);
        }

        public static implicit operator EmitExpression(int value)
        {
            return new EmitLiteralExpression(value);
        }

        public static implicit operator EmitExpression(long value)
        {
            return new EmitLiteralExpression(value);
        }

        public static implicit operator EmitExpression(float value)
        {
            return new EmitLiteralExpression(value);
        }

        public static implicit operator EmitExpression(double value)
        {
            return new EmitLiteralExpression(value);
        }

        public static implicit operator EmitExpression(bool value)
        {
            return new EmitLiteralExpression(value);
        }

        int IEmitArrayElement.Length => 0;

        IEmitArrayElement IEmitArrayElement.this[int index]
        {
            get { throw new IndexOutOfRangeException(); }
        }
    }
}
