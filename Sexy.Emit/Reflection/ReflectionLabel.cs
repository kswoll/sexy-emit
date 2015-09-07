using System.Reflection.Emit;

namespace Sexy.Emit.Reflection
{
    public class ReflectionLabel : EmitLabel
    {
        public Label Label { get; }

        public ReflectionLabel(Label label)
        {
            Label = label;
        }
    }
}
