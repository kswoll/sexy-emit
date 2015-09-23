namespace Sexy.Emit.Ast
{
    public abstract class EmitStatement : IEmitStatement
    {
        public abstract void Compile(EmitCompilerContext context, EmitIl il);
        
    }

    public abstract class EmitStatement<TData> : EmitStatement
    {
        public TData GetData(EmitCompilerContext context)
        {
            return (TData)context.Data[this];
        }

        public void SetData(EmitCompilerContext context, TData data)
        {
            context.Data[this] = data;
        }
    }
}
