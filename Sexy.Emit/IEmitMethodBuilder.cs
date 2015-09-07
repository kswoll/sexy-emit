using Sexy.Emit.Ast;

namespace Sexy.Emit
{
    public interface IEmitMethodBuilder : IEmitMethod
    {
        IEmitIl Il { get; }
        EmitBlockStatement Body { get; }
        void Compile();
    }
}
