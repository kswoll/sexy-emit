using System.Collections.Generic;
using Sexy.Emit.Ast;

namespace Sexy.Emit
{
    public class EmitConstructorBuilder : EmitConstructor, IEmitMethodOrConstructorBuilder
    {
        public EmitBlockStatement Body { get; } = new EmitBlockStatement();
        public EmitIl Il { get; } = new EmitIl();

        private List<EmitParameter> parameters;

        public EmitConstructorBuilder(EmitType declaringType, EmitVisibility visibility, bool isStatic) : 
            base(declaringType, result => ((EmitConstructorBuilder)result).parameters = new List<EmitParameter>(), 
                visibility, isStatic)
        {
        }

        EmitType IEmitMethodOrConstructorBuilder.ReturnType => null;

        public EmitParameter DefineParameter(EmitType parameterType, string name = null)
        {
            var parameter = new EmitParameter(name, parameterType);
            parameters.Add(parameter);
            return parameter;
        }
        
        public void Compile()
        {
            Body.Compile(new EmitCompilerContext(this), Il);
        }
    }
}
