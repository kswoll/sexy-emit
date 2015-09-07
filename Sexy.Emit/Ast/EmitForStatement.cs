namespace Sexy.Emit.Ast
{
    public class EmitForStatement : EmitStatement<object>
    {
        public EmitVariableDeclarationStatement Initializer { get; }
        public EmitExpression Predicate { get; }
        public IEmitStatement Incrementor { get; }
        public IEmitStatement Body { get; }

        public EmitForStatement(EmitVariableDeclarationStatement initializer, EmitExpression predicate, IEmitStatement incrementor, IEmitStatement body)
        {
            Initializer = initializer;
            Predicate = predicate;
            Incrementor = incrementor;
            Body = body;
        }

        public override void Compile(EmitCompilerContext context, IEmitIl il)
        {
            Initializer?.Compile(context, il);

            var start = il.DefineLabel();
            var end = il.DefineLabel();

            il.MarkLabel(start);

            if (Predicate != null)
                Predicate.Compile(context, il);
            else
                il.Emit(EmitOpCodes.Ldc_I4_1);

            il.Emit(EmitOpCodes.Brfalse, end);

            Body.Compile(context, il);
            Incrementor?.Compile(context, il);
            il.Emit(EmitOpCodes.Br, start);

            il.MarkLabel(end);
        }
    }
}
