namespace Sexy.Emit.Ast
{
    public interface IEmitStatement
    {
        void Compile(EmitCompilerContext context, EmitIl il);
    }
}
