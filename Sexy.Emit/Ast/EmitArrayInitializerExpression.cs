using System;
using System.Collections.Generic;
using System.Linq;

namespace Sexy.Emit.Ast
{
    public class EmitArrayInitializerExpression : EmitExpression
    {
        public IEmitType Type { get; }
        public EmitArrayInitializer Initializer { get; }
        public int Rank { get; }
        public IReadOnlyList<int> Lengths { get; }

        public EmitArrayInitializerExpression(IEmitType type, params IEmitArrayElement[] elements) : this(type, new EmitArrayInitializer(elements))
        {
        }

        public EmitArrayInitializerExpression(IEmitType type, EmitArrayInitializer initializer)
        {
            Type = type;
            Initializer = initializer;

            if (Initializer.Length == 0)
                throw new EmitVerifyException("An array initiaizler expression must have at least one element.");

            Rank = 0;
            var lengths = new Queue<int>();
            lengths.Enqueue(Initializer.Length);
            var current = Initializer[0];
            while (current != null)
            {
                Rank++;
                if (current is EmitArrayInitializer)
                {
                    lengths.Enqueue(current.Length);
                    current = current[0];
                }
                else
                {
                    current = null;
                }
            }
            Lengths = lengths.ToArray();
        }

        public override void Compile(EmitCompilerContext context, IEmitIl il)
        {
            var arrayType = GetType(context.TypeSystem);

            if (Lengths.Count == 1)
            {
                il.Emit(EmitOpCodes.Ldc_I4, Lengths[0]);
                il.Emit(EmitOpCodes.Newarr, Type);
                for (var i = 0; i < Initializer.Length; i++)
                {
                    var element = (EmitExpression)Initializer[i];
                    il.Emit(EmitOpCodes.Dup);
                    il.Emit(EmitOpCodes.Ldc_I4, i);
                    element.Compile(context, il);
                    il.Emit(EmitOpCodes.Stelem, Type);
                }
            }
            else
            {
                var constructor = arrayType.Members.OfType<IEmitConstructor>().Single(x => x.Parameters.Count() == Rank);
                var setter = arrayType.Members.OfType<IEmitMethod>().Single(x => x.Name == "Set");
                foreach (var length in Lengths)
                    il.Emit(EmitOpCodes.Ldc_I4, length);
                il.Emit(EmitOpCodes.Newobj, constructor);

                var indices = new int[Lengths.Count];
                Action<int, IEmitArrayElement> recurse = null;
                recurse = (level, element) =>
                {
                    if (level == Rank - 1)
                    {
                        var expression = (EmitExpression)element;
                        il.Emit(EmitOpCodes.Dup);
                        foreach (var index in indices)
                        {
                            il.Emit(EmitOpCodes.Ldc_I4, index);
                        }
                        expression.Compile(context, il);
                        il.Emit(EmitOpCodes.Call, setter);
                    }
                    else
                    {
                        for (var i = 0; i < element.Length; i++)
                        {
                            indices[level + 1] = i;
                            var current = element[i];
                            recurse(level + 1, current);
                        }
                    }
                };
                for (var i = 0; i < Initializer.Length; i++)
                {
                    var element = Initializer[i];
                    indices[0] = i;
                    recurse(0, element);
                }
            }
        }

        public override IEmitType GetType(IEmitTypeSystem typeSystem)
        {
            return Type.MakeArrayType(Rank);
        }
    }
}
