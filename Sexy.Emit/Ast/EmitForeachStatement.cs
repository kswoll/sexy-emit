using System.Collections.Generic;
using System.Linq;

namespace Sexy.Emit.Ast
{
    public class EmitForeachStatement : EmitStatement
    {
        public EmitVariable Item { get; }
        public EmitExpression Collection { get; }
        public EmitStatement Statement { get; }

        public EmitForeachStatement(EmitVariable item, EmitExpression collection, EmitStatement statement)
        {
            Item = item;
            Collection = collection;
            Statement = statement;
        }

        public override void Compile(EmitCompilerContext context, EmitIl il)
        {
            var item = il.DeclareLocal(Item.Type);
            Item.SetData(context, item);

            var genericEnumerableType = context.TypeSystem.GetType(typeof(IEnumerable<>));
            var enumerableType = genericEnumerableType.MakeGenericType(Item.Type);
            var getEnumeratorMethod = enumerableType.Members.OfType<EmitMethod>().Single(x => x.Name == nameof(IEnumerable<object>.GetEnumerator));

            var genericEnumeratorType = context.TypeSystem.GetType(typeof(IEnumerator<>));
            var enumeratorType = genericEnumeratorType.MakeGenericType(Item.Type);
            var moveNextMethod = enumerableType.Members.OfType<EmitMethod>().Single(x => x.Name == nameof(IEnumerator<object>.MoveNext));
            var getCurrentMethod = enumerableType.Members.OfType<EmitProperty>().Single(x => x.Name == nameof(IEnumerator<object>.Current)).GetMethod;

            var enumerator = il.DeclareLocal(enumeratorType);

            Collection.Compile(context, il);
            il.Emit(EmitOpCodes.Callvirt, getEnumeratorMethod);
            il.Emit(EmitOpCodes.Stloc, enumerator);

            var topOfLoop = il.DefineLabel();
            var end = il.DefineLabel();

            il.MarkLabel(topOfLoop);
            il.Emit(EmitOpCodes.Ldloc, enumerator);
            il.Emit(EmitOpCodes.Callvirt, moveNextMethod);
            il.Emit(EmitOpCodes.Brfalse, end);

            il.Emit(EmitOpCodes.Ldloc, enumerator);
            il.Emit(EmitOpCodes.Callvirt, getCurrentMethod);
            il.Emit(EmitOpCodes.Stloc, item);

            Statement.Compile(context, il);
            il.Emit(EmitOpCodes.Br, topOfLoop);

            il.MarkLabel(end);
            il.Emit(EmitOpCodes.Nop);
        }
    }
}
