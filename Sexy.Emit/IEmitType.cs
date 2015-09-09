using System;
using System.Collections.Generic;
using System.Text;

namespace Sexy.Emit
{
    public interface IEmitType
    {
        string Namespace { get; }
        string Name { get; }
        IEmitType DeclaringType { get; }
        IEnumerable<IEmitMember> Members { get; }
        bool IsValueType { get; }
        IEmitType BaseType { get; }
    }
}
