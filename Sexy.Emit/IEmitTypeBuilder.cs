namespace Sexy.Emit
{
    public interface IEmitTypeBuilder : IEmitType
    {
        IEmitFieldBuilder DefineField(string name, IEmitType type, EmitVisibility visibility = EmitVisibility.Public, 
            bool isStatic = false, bool isReadonly = false, bool isVolatile = false);

        IEmitMethodBuilder DefineMethod(string name, IEmitType returnType, EmitVisibility visibility = EmitVisibility.Public, 
            bool isAbstract = false, bool isSealed = false, bool isVirtual = false, bool isOverride = false, 
            bool isExtern = false, bool isStatic = false, params IEmitType[] parameterTypes);
    }
}
