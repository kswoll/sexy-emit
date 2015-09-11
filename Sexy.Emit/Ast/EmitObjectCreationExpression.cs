using System;
using System.Collections.Generic;

namespace Sexy.Emit.Ast
{
    public class EmitObjectCreationExpression : EmitExpression
    {
        public IEmitType Type { get; }
        public IEmitConstructor Constructor { get; }
        public List<EmitExpression> Arguments { get; } = new List<EmitExpression>();

        public EmitObjectCreationExpression(IEmitType valueType)
        {
            Type = valueType;
        }

        public EmitObjectCreationExpression(IEmitConstructor constructor, params EmitExpression[] arguments)
        {
            if (constructor.IsStatic)
                throw new ArgumentException("Cannot create an instance using a static constructor", nameof(constructor));

            Type = constructor.DeclaringType;
            Constructor = constructor;
            Arguments.AddRange(arguments);
        }

        public override void Compile(EmitCompilerContext context, IEmitIl il)
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

        public override IEmitType GetType(IEmitTypeSystem typeSystem)
        {
            return Type;
        }
    }
}
