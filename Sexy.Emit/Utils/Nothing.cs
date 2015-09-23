namespace Sexy.Emit.Utils
{
    public sealed class Nothing
    {
        public static Nothing Value { get; } = new Nothing();

        private Nothing()
        {
        }
    }
}
