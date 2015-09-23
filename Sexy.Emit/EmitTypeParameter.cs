namespace Sexy.Emit
{
    public class EmitTypeParameter
    {
        public bool HasClassConstraint { get; }
        public bool HasNewConstraint { get; }
        public bool HasStructConstraint { get; }
        public EmitType ClassConstraint { get; }
    }
}
