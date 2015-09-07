using System.Collections.Generic;

namespace Sexy.Emit.Ast
{
    public class EmitCompilerContext
    {
        public IEmitMethodBuilder Method { get; }
        public IEmitTypeSystem TypeSystem { get; }
        public Dictionary<object, object> Data { get; }

        public EmitCompilerContext(IEmitMethodBuilder method, IEmitTypeSystem typeSystem)
        {
            Method = method;
            TypeSystem = typeSystem;
            Data = new Dictionary<object, object>();
        }
    }
}
