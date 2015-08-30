using System;

namespace Sexy.Emit
{
    [Flags]
    public enum EmitTypeAttributes
    {
        None = 0,
        Class = 0,
        Interface = 1,
        Abstract = 2,
        Public = 0,
        Protected = 4,
        Private = 8,
        Internal = 16
    }
}
