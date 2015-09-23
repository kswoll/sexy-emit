using System.Collections.Generic;
using System.Linq;

namespace Sexy.Emit.Ast
{
    public class EmitVariableDeclarationStatement : EmitStatement<EmitLocal>
    {
        public List<EmitVariable> Variables { get; }

        public EmitVariableDeclarationStatement(params EmitVariable[] variables)
        {
            Variables = new List<EmitVariable>(variables);
        }

        public override void Compile(EmitCompilerContext context, EmitIl il)
        {
            foreach (var variable in Variables)
            {
                var local = il.DeclareLocal(variable.Type);
                variable.SetData(context, local);
            }
        }

        public static implicit operator EmitVariableReferenceExpression(EmitVariableDeclarationStatement declaration)
        {
            return new EmitVariableReferenceExpression(declaration.Variables.Single());
        }
    }
}
