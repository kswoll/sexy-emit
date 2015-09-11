using System;
using System.Collections.Generic;
using System.Linq;
using Sexy.Emit.OpCodes;

namespace Sexy.Emit.Ast
{
    public class EmitMethodInvocationExpression : EmitExpression
    {
        public EmitExpression Target { get; }
        public IEmitMethod Method { get; }
        public List<EmitExpression> Arguments { get; } = new List<EmitExpression>();

        public EmitMethodInvocationExpression(IEmitMethod method, params EmitExpression[] arguments)
        {
            if (!method.IsStatic)
                throw new Exception("Non-static method requires a target.");

            Method = method;
            Arguments.AddRange(arguments);
        }

        public EmitMethodInvocationExpression(EmitExpression target, IEmitMethod method, params EmitExpression[] arguments)
        {
            if (method.IsStatic && target != null)
                throw new Exception("Static method cannot have a target");

            Target = target;
            Method = method;
            Arguments.AddRange(arguments);
        }

        public override void Compile(EmitCompilerContext context, IEmitIl il)
        {
            if (Arguments.Count != Method.Parameters.Count())
                throw new Exception($"Incorrect number of arguments passed to method {Method}");

            Target?.Compile(context, il);
            foreach (var argument in Arguments)
                argument.Compile(context, il);

            var opCode = Method.IsVirtual || Method.DeclaringType.IsInterface ? (IEmitOpCodeMethod)EmitOpCodes.Callvirt : EmitOpCodes.Call;

            il.Emit(opCode, Method);
        }

        public override IEmitType GetType(IEmitTypeSystem typeSystem)
        {
            return Method.ReturnType;
        }
    }
}
