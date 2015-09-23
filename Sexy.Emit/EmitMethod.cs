using System;
using System.Collections.Generic;
using System.Reflection;

namespace Sexy.Emit
{
    public class EmitMethod : EmitMember
    {
        public EmitType ReturnType { get; }
        public IReadOnlyList<EmitParameter> Parameters { get; }
        public EmitVisibility Visibility { get; }
        public bool IsStatic { get; }
        public bool IsSealed { get; }
        public bool IsVirtual { get; }
        public bool IsAbstract { get; }

        public EmitMethod(EmitType declaringType, string name, EmitType returnType, Func<EmitMethod, IReadOnlyList<EmitParameter>> parameters, EmitVisibility visibility, bool isStatic, bool isSealed, bool isVirtual, bool isAbstract) : base(declaringType, name)
        {
            ReturnType = returnType;
            Parameters = parameters(this);
            Visibility = visibility;
            IsStatic = isStatic;
            IsSealed = isSealed;
            IsVirtual = isVirtual;
            IsAbstract = isAbstract;
        }

        public override TOutput Accept<TInput, TOutput>(IEmitSchemaVisitor<TInput, TOutput> visitor, TInput input)
        {
            return visitor.VisitMethod(this, input);
        }

        public static implicit operator EmitMethod(MethodInfo method)
        {
            return (EmitMethodReference)method;
        }
    }
}
