using System.Collections.Generic;
using System.Linq;

namespace Sexy.Emit.Ast
{
    public class EmitArrayCreationExpression : EmitExpression
    {
        public IEmitType Type { get; }
        public IReadOnlyList<EmitExpression> Lengths { get; }

        public EmitArrayCreationExpression(IEmitType type, params EmitExpression[] lengths)
        {
            Type = type;
            Lengths = lengths;
        }

        public override void Compile(EmitCompilerContext context, IEmitIl il)
        {
            if (Lengths.Count == 1)
            {
                Lengths[0].Compile(context, il);
                il.Emit(EmitOpCodes.Newarr, Type);
            }
            else
            {
                var arrayType = GetType(context.TypeSystem);
                var constructor = arrayType.Members.OfType<IEmitConstructor>().Single(x => x.Parameters.Count() == Lengths.Count);
                foreach (var length in Lengths)
                {
                    length.Compile(context, il);
                }
                il.Emit(EmitOpCodes.Newobj, constructor);
            }
        }

        public override IEmitType GetType(IEmitTypeSystem typeSystem)
        {
            return Type.MakeArrayType(Lengths.Count);
        }
    }
}
