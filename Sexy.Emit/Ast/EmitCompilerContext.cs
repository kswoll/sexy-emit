using System.Collections.Generic;

namespace Sexy.Emit.Ast
{
    public class EmitCompilerContext
    {
        public IEmitMethodOrConstructorBuilder Method { get; }
        public Dictionary<object, object> Data { get; }

        public EmitCompilerContext(IEmitMethodOrConstructorBuilder method)
        {
            Method = method;
            Data = new Dictionary<object, object>();
        }
    }
}
