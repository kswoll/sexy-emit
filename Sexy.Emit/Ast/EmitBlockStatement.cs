using System.Collections.Generic;

namespace Sexy.Emit.Ast
{
    public class EmitBlockStatement : EmitStatement<object>
    {
        public List<IEmitStatement> Statements { get; }

        public EmitBlockStatement(params IEmitStatement[] statements)
        {
            Statements = new List<IEmitStatement>(statements);
        }

        public override void Compile(EmitCompilerContext context, IEmitIl il)
        {
            foreach (var statement in Statements)
            {
                statement.Compile(context, il);
            }
        }
    }
}
