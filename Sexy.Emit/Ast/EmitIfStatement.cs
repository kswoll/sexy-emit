using System.Linq;

namespace Sexy.Emit.Ast
{
    public class EmitIfStatement : EmitStatement
    {
        public EmitExpression Condition { get; }
        public EmitStatement Statement { get; }
        public EmitStatement Else { get; }

        public EmitIfStatement(EmitExpression condition, EmitStatement statement, EmitStatement @else = null)
        {
            Condition = condition;
            Statement = statement;
            Else = @else;
        }

        public override void Compile(EmitCompilerContext context, EmitIl il)
        {
            Condition.Compile(context, il);
            var ifNotTrue = il.DefineLabel();
            il.Emit(EmitOpCodes.Brfalse, ifNotTrue);
            Statement.Compile(context, il);
            var statementReturned = il.Instructions.Last().OpCode == EmitOpCodes.Ret;

            if (Else != null)
            {
                EmitLabel end = null; 
                if (!statementReturned)
                {
                    end = il.DefineLabel();
                    il.Emit(EmitOpCodes.Br, end);
                }
                il.MarkLabel(ifNotTrue);
                Else.Compile(context, il);
                if (!statementReturned)
                {
                    il.MarkLabel(end);
                    il.Emit(EmitOpCodes.Nop);                    
                }
            }
            else 
            {
                il.MarkLabel(ifNotTrue);                
            }
        }
    }
}
