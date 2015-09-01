using System.Reflection.Emit;

namespace Sexy.Emit.Reflection
{
    public class ReflectionLabel : IEmitLabel
    {
        public Label Label { get; }

        public ReflectionLabel(Label label)
        {
            Label = label;
        }
    }
}
