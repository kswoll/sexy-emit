namespace Sexy.Emit.Ast
{
    public class EmitVariable 
    {
        public EmitType Type { get; }

        public EmitVariable(EmitType type)
        {
            Type = type;
        }

        public EmitLocal GetData(EmitCompilerContext context)
        {
            return (EmitLocal)context.Data[this];
        }

        public void SetData(EmitCompilerContext context, EmitLocal local)
        {
            context.Data[this] = local;
        }

        public static implicit operator EmitVariableReferenceExpression(EmitVariable variable)
        {
            return new EmitVariableReferenceExpression(variable);
        }
    }
}
