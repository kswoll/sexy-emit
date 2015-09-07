namespace Sexy.Emit.Ast
{
    public interface IEmitStatement
    {
        void Compile(EmitCompilerContext context, IEmitIl il);
    }
}
