using System;
using System.Collections.Generic;
using System.Linq;

namespace Sexy.Emit.Ast
{
    public class EmitArrayInitializer : IEmitArrayElement
    {
        public IReadOnlyList<IEmitArrayElement> Elements { get; }

        public EmitArrayInitializer(params IEmitArrayElement[] elements)
        {
            Elements = elements;
        }

        public int Length => Elements.Count;
        public IEmitArrayElement this[int index] => Elements[index];

        public static implicit operator EmitArrayInitializer(Array array)
        {
            var indices = new int[array.Rank];
            Func<int, List<IEmitArrayElement>> recurse = null;
            recurse = level =>
            {
                var items = new List<IEmitArrayElement>();
                for (var i = array.GetLowerBound(level); i <= array.GetUpperBound(level); i++)
                {
                    indices[level] = i;
                    if (level == array.Rank - 1)
                    {
                        var value = array.GetValue(indices);

                        var converter = typeof(EmitExpression)
                            .GetMethods()
                            .Single(x => 
                                x.Name == "op_Implicit" && 
                                x.ReturnType == typeof(EmitExpression) && 
                                x.GetParameters().Single().ParameterType == value.GetType());
                        var expression = (EmitExpression)converter.Invoke(null, new[] { value });

                        items.Add(expression);
                    }
                    else
                    {
                        var childItems = recurse(level + 1);
                        items.Add(new EmitArrayInitializer(childItems.ToArray()));
                    }
                }
                return items;
            };
            return new EmitArrayInitializer(recurse(0).ToArray());
        }
/*

        public static implicit operator EmitArrayInitializer(EmitExpression[] elements)
        {
            return new EmitArrayInitializer(elements);
        }

        public static implicit operator EmitArrayInitializer(EmitArrayInitializer[] elements)
        {
            return new EmitArrayInitializer(elements.ToArray());
        }

        public static implicit operator EmitArrayInitializer(int[] elements)
        {
            return new EmitArrayInitializer(elements.Select(x => (EmitExpression)x).ToArray());
        }

        public static implicit operator EmitArrayInitializer(bool[] elements)
        {
            return new EmitArrayInitializer(elements.Select(x => (EmitExpression)x).ToArray());
        }

        public static implicit operator EmitArrayInitializer(byte[] elements)
        {
            return new EmitArrayInitializer(elements.Select(x => (EmitExpression)x).ToArray());
        }

        public static implicit operator EmitArrayInitializer(sbyte[] elements)
        {
            return new EmitArrayInitializer(elements.Select(x => (EmitExpression)x).ToArray());
        }

        public static implicit operator EmitArrayInitializer(short[] elements)
        {
            return new EmitArrayInitializer(elements.Select(x => (EmitExpression)x).ToArray());
        }

        public static implicit operator EmitArrayInitializer(ushort[] elements)
        {
            return new EmitArrayInitializer(elements.Select(x => (EmitExpression)x).ToArray());
        }

        public static implicit operator EmitArrayInitializer(uint[] elements)
        {
            return new EmitArrayInitializer(elements.Select(x => (EmitExpression)x).ToArray());
        }

        public static implicit operator EmitArrayInitializer(long[] elements)
        {
            return new EmitArrayInitializer(elements.Select(x => (EmitExpression)x).ToArray());
        }

        public static implicit operator EmitArrayInitializer(ulong[] elements)
        {
            return new EmitArrayInitializer(elements.Select(x => (EmitExpression)x).ToArray());
        }

        public static implicit operator EmitArrayInitializer(float[] elements)
        {
            return new EmitArrayInitializer(elements.Select(x => (EmitExpression)x).ToArray());
        }

        public static implicit operator EmitArrayInitializer(double[] elements)
        {
            return new EmitArrayInitializer(elements.Select(x => (EmitExpression)x).ToArray());
        }

        public static implicit operator EmitArrayInitializer(decimal[] elements)
        {
            return new EmitArrayInitializer(elements.Select(x => (EmitExpression)x).ToArray());
        }
*/
    }
}
