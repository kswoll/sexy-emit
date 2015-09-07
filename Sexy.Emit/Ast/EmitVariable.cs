namespace Sexy.Emit.Ast
{
    public class EmitVariable 
    {
        public IEmitType Type { get; }

        public EmitVariable(IEmitType type)
        {
            Type = type;
        }

        public IEmitLocal GetData(EmitCompilerContext context)
        {
            return (IEmitLocal)context.Data[this];
        }

        public void SetData(EmitCompilerContext context, IEmitLocal local)
        {
            context.Data[this] = local;
        }

        public static implicit operator EmitVariableReferenceExpression(EmitVariable variable)
        {
            return new EmitVariableReferenceExpression(variable);
        }
    }
}
