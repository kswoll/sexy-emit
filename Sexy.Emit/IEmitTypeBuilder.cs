namespace Sexy.Emit
{
    public interface IEmitTypeBuilder : IEmitType
    {
        IEmitFieldBuilder DefineField(string name, IEmitType type);
        IEmitMethodBuilder DefineMethod(string name, IEmitType returnType, EmitVisibility visibility = EmitVisibility.Public, 
            bool isAbstract = false, bool isSealed = false, bool isVirtual = false, bool isOverride = false, 
            bool isExtern = false, params IEmitType[] parameterTypes);
    }
}
