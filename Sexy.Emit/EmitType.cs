using System;
using System.Collections.Generic;
using System.Reflection;
using Sexy.Emit.Utils;

namespace Sexy.Emit
{
    public class EmitType : EmitMember
    {
        public EmitAssembly Assembly { get; }
        public string Namespace { get; }
        public IReadOnlyList<EmitMember> Members { get; }
        public EmitVisibility Visibility { get; }
        public EmitTypeKind Kind { get; }
        public bool IsAbstract { get; }
        public bool IsSealed { get; }
        public IReadOnlyList<EmitType> ImplementedInterfaces { get; }

        private readonly EmitTypeReference baseType;

        public EmitType(EmitAssembly assembly, string ns, string name, EmitTypeKind kind, EmitType declaringType, 
            Func<EmitType, IReadOnlyList<EmitMemberReference>> membersFactory, EmitVisibility visibility, 
            EmitTypeReference baseType, 
            Func<EmitType, IReadOnlyList<EmitTypeReference>> implementedInterfacesFactory,
            bool isAbstract, bool isSealed
        ) : 
            base(declaringType, name)
        {
            Assembly = assembly;
            Namespace = ns;
            Members = new ReadOnlyListWrapper<EmitMemberReference, EmitMember>(membersFactory(this));
            Visibility = visibility;
            Kind = kind;
            IsAbstract = isAbstract;
            IsSealed = isSealed;
            ImplementedInterfaces = new ReadOnlyListWrapper<EmitTypeReference, EmitType>(implementedInterfacesFactory(this));

            this.baseType = baseType;
        }

        public EmitType BaseType => baseType.Value;
        public bool IsValueType => Kind == EmitTypeKind.Struct;
        public bool IsInterface => Kind == EmitTypeKind.Interface;
        public bool IsEnum => Kind == EmitTypeKind.Enum;
        public virtual string FullName => (!string.IsNullOrEmpty(Namespace) ? Namespace + "." : "") + (DeclaringType == null ? Name : $"{DeclaringType.Name}+{Name}");

        public override TOutput Accept<TInput, TOutput>(IEmitSchemaVisitor<TInput, TOutput> visitor, TInput input = default(TInput))
        {
            return visitor.VisitType(this, input);
        }

        public override string ToString()
        {
            return $"{Namespace}.{Name}";
        }

        protected bool Equals(EmitType other)
        {
            return FullName == other.FullName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is EmitType) return ((EmitType)obj).FullName == FullName;
            if (obj.GetType() != GetType()) return false;
            return Equals((EmitType)obj);
        }

        public override int GetHashCode()
        {
            return FullName.GetHashCode();
        }

        public EmitGenericType MakeGenericType(params EmitType[] typeArguments)
        {
            Func<EmitType, IReadOnlyList<EmitMemberReference>> genericMembersFactory = genericType =>
            {
                var genericMembers = new List<EmitMemberReference>();
                foreach (var member in Members)
                {
                    
                }
                return genericMembers;
            };
            return new EmitGenericType(Assembly, Namespace, Name, DeclaringType, genericMembersFactory, BaseType, this, typeArguments);
        }

        public EmitArrayType MakeArrayType(int rank)
        {
            var arrayType = (EmitType)typeof(Array);
            Func<EmitType, IReadOnlyList<EmitMemberReference>> arrayMembersFactory = result =>
            {
                var arrayMembers = new List<EmitMemberReference>();
                foreach (var member in arrayType.Members)
                {
                    
                }
                return arrayMembers;
            };
            return new EmitArrayType(Assembly, Namespace, Name, EmitTypeKind.Array, DeclaringType, arrayMembersFactory, arrayType, this, rank);
        }

        public static implicit operator EmitType(Type type)
        {
            return type == null ? null : (EmitTypeReference)type;
        }

        public static implicit operator Type(EmitType type)
        {
            return ((Assembly)type.Assembly).GetType(type.FullName);
        }
    }
}
