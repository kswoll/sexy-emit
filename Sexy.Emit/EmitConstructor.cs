using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sexy.Emit
{
    public class EmitConstructor : EmitMember
    {
        public IReadOnlyList<EmitParameter> Parameters { get; }
        public EmitVisibility Visibility { get; }
        public bool IsStatic { get; }

        public EmitConstructor(EmitType declaringType, Func<EmitConstructor, IReadOnlyList<EmitParameter>> parameters, EmitVisibility visibility, bool isStatic) : base(declaringType, isStatic ? ".cctor" : ".ctor")
        {
            Parameters = parameters(this);
            Visibility = visibility;
            IsStatic = isStatic;
        }

        public override TOutput Accept<TInput, TOutput>(IEmitSchemaVisitor<TInput, TOutput> visitor, TInput input)
        {
            return visitor.VisitConstructor(this, input);
        }

        public static implicit operator EmitConstructor(ConstructorInfo constructor)
        {
            return (EmitConstructorReference)constructor;
        }

        public static implicit operator ConstructorInfo(EmitConstructor constructor)
        {
            return ((Type)constructor.DeclaringType).GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, constructor.Parameters.Select(x => (Type)x.ParameterType).ToArray(), null);
        }
    }
}
