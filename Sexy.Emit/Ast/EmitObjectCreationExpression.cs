using System;
using System.Collections.Generic;

namespace Sexy.Emit.Ast
{
    public class EmitObjectCreationExpression : EmitExpression
    {
        public EmitType Type { get; }
        public EmitConstructor Constructor { get; }
        public List<EmitExpression> Arguments { get; } = new List<EmitExpression>();

        public EmitObjectCreationExpression(EmitType valueType)
        {
            Type = valueType;
        }

        public EmitObjectCreationExpression(EmitConstructor constructor, params EmitExpression[] arguments)
        {
            if (constructor.IsStatic)
                throw new ArgumentException("Cannot create an instance using a static constructor", nameof(constructor));

            Type = constructor.DeclaringType;
            Constructor = constructor;
            Arguments.AddRange(arguments);
        }

        public override void Compile(EmitCompilerContext context, EmitIl il)
        {
            // Special handling for value types without a constructor
            if (Constructor == null)
            {
                var local = il.DeclareLocal(Type);
                il.Emit(EmitOpCodes.Ldloca, local);
                il.Emit(EmitOpCodes.Initobj, Type);
                il.Emit(EmitOpCodes.Ldloc, local);
                return;
            }

            foreach (var argument in Arguments)
            {
                argument.Compile(context, il);
            }

            il.Emit(EmitOpCodes.Newobj, Constructor);
        }

        public override EmitType GetExpressionType()
        {
            return Type;
        }
    }
}
