using System;
using System.Collections.Generic;
using System.Reflection;

namespace Sexy.Emit
{
    public class EmitProperty : EmitMember
    {
        public EmitType PropertyType { get; }
        public IReadOnlyList<EmitParameter> Parameters { get; }
        public EmitMethod GetMethod { get; }
        public EmitMethod SetMethod { get; }

        public EmitProperty(
            EmitType declaringType, 
            string name, 
            EmitType propertyType, 
            Func<EmitProperty, IReadOnlyList<EmitParameter>> parameters, 
            EmitMethod getMethod, EmitMethod setMethod
        ) : 
            base(declaringType, name)
        {
            PropertyType = propertyType;
            Parameters = parameters(this);
            GetMethod = getMethod;
            SetMethod = setMethod;
        }

        public override TOutput Accept<TInput, TOutput>(IEmitSchemaVisitor<TInput, TOutput> visitor, TInput input = default(TInput))
        {
            return visitor.VisitProperty(this, input);
        }

        public static implicit operator EmitProperty(PropertyInfo property)
        {
            return (EmitPropertyReference)property;
        }
    }
}
