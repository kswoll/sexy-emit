﻿using System;

namespace Sexy.Emit.Ast
{
    public class EmitVariableReferenceExpression : EmitExpression, IEmitReferenceExpression
    {
        public EmitVariable Variable { get; }

        public EmitVariableReferenceExpression(EmitVariable variable)
        {
            Variable = variable;
        }

        public override void Compile(EmitCompilerContext context, IEmitIl il)
        {
            var local = Variable.GetData(context);
            il.Emit(EmitOpCodes.Ldloc, local);
        }

        public void CompileAssignment(EmitCompilerContext context, IEmitIl il, Action compileValue)
        {
            var local = Variable.GetData(context);
            compileValue();
            il.Emit(EmitOpCodes.Stloc, local);
        }

        public override IEmitType GetType(IEmitTypeSystem typeSystem)
        {
            return Variable.Type;
        }
    }
}