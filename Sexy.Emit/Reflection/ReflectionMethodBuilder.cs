using System.Linq;
using System.Reflection.Emit;
using Sexy.Emit.Ast;

namespace Sexy.Emit.Reflection
{
    public class ReflectionMethodBuilder : ReflectionMethod, IEmitMethodBuilder
    {
        public EmitBlockStatement Body { get; } = new EmitBlockStatement();
        public MethodBuilder MethodBuilder { get; }

        private readonly ReflectionIl il;

        public ReflectionMethodBuilder(MethodBuilder methodBuilder) : base(methodBuilder)
        {
            MethodBuilder = methodBuilder;
            il = new ReflectionIl(methodBuilder.GetILGenerator());
        }

        public IEmitIl Il => il;

        public void Compile()
        {
            Body.Compile(new EmitCompilerContext(this, new ReflectionTypeSystem()), Il);
        }
    }
}
