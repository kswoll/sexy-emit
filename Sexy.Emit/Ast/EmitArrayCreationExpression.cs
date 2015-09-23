using System.Collections.Generic;
using System.Linq;

namespace Sexy.Emit.Ast
{
    public class EmitArrayCreationExpression : EmitExpression
    {
        public EmitType Type { get; }
        public IReadOnlyList<EmitExpression> Lengths { get; }

        public EmitArrayCreationExpression(EmitType type, params EmitExpression[] lengths)
        {
            Type = type;
            Lengths = lengths;
        }

        public override void Compile(EmitCompilerContext context, EmitIl il)
        {
            if (Lengths.Count == 1)
            {
                Lengths[0].Compile(context, il);
                il.Emit(EmitOpCodes.Newarr, Type);
            }
            else
            {
                var arrayType = GetExpressionType();
                var constructor = arrayType.Members.OfType<EmitConstructor>().Single(x => x.Parameters.Count() == Lengths.Count);
                foreach (var length in Lengths)
                {
                    length.Compile(context, il);
                }
                il.Emit(EmitOpCodes.Newobj, constructor);
            }
        }

        public override EmitType GetExpressionType()
        {
            return Type.MakeArrayType(Lengths.Count);
        }
    }
}
