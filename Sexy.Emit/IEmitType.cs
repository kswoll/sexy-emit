using System;
using System.Collections.Generic;
using System.Text;

namespace Sexy.Emit
{
    public interface IEmitType
    {
        string Namespace { get; }
        string Name { get; }
        string FullName { get; }
        IEmitType DeclaringType { get; }
        IEnumerable<IEmitMember> Members { get; }
        bool IsValueType { get; }
        bool IsInterface { get; }
        IEmitType BaseType { get; }
    }
}
