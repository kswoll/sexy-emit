using System.Collections.Generic;
using Sexy.Emit.Ast;

namespace Sexy.Emit
{
    public class EmitMethodBuilder : EmitMethod, IEmitMethodOrConstructorBuilder
    {
        public EmitBlockStatement Body { get; } = new EmitBlockStatement();
        public EmitIl Il { get; } = new EmitIl();

        private List<EmitParameter> parameters;

        public EmitMethodBuilder(EmitType declaringType, string name, EmitType returnType, EmitVisibility visibility, bool isStatic, bool isSealed, bool isVirtual, bool isAbstract) : 
            base(declaringType, name, returnType, 
                result => ((EmitMethodBuilder)result).parameters = new List<EmitParameter>(),
                visibility, isStatic, isSealed, isVirtual, isAbstract)
        {
        }

        public EmitParameter DefineParameter(EmitType parameterType, string name = null)
        {
            var parameter = new EmitParameter(name, parameterType);
            parameters.Add(parameter);
            return parameter;
        }

        public void Compile(IEmitTypeSystem typeSystem)
        {
            Body.Compile(new EmitCompilerContext(this, typeSystem), Il);
        }
    }
}
