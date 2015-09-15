using System;
using System.Linq;
using System.Reflection.Emit;
using Sexy.Emit.Ast;
using Sexy.Emit.OpCodes;
using Sexy.Emit.Utils;

namespace Sexy.Emit.Reflection
{
    public class ReflectionIl : IEmitIl
    {
        private readonly ILGenerator il;

        public ReflectionIl(ILGenerator il)
        {
            this.il = il;
        }

        public IEmitLocal DeclareLocal(IEmitType type)
        {
            return new ReflectionLocal(il.DeclareLocal(((ReflectionType)type).Type));
        }

        public IEmitLabel DefineLabel()
        {
            return new ReflectionLabel(il.DefineLabel());
        }

        public void MarkLabel(IEmitLabel label)
        {
            il.MarkLabel(((ReflectionLabel)label).Label);
        }

        public void Emit(EmitOpCode instruction, Impossible impossible)
        {
            throw new NotImplementedException();
        }

        public void Emit(IEmitOpCodeVoid instruction)
        {
            il.Emit(instruction.ToOpCode());
        }

        public void Emit(IEmitOpCodeType instruction, IEmitType type)
        {
            il.Emit(instruction.ToOpCode(), ((ReflectionType)type).Type);
        }

        public void Emit(IEmitOpCodeMethod instruction, IEmitMethod method)
        {
            il.Emit(instruction.ToOpCode(), ((ReflectionMethod)method).Method);
        }

        public void Emit(IEmitOpCodeConstructor instruction, IEmitConstructor constructor)
        {
            il.Emit(instruction.ToOpCode(), ((ReflectionConstructor)constructor).Constructor);
        }

        public void Emit(IEmitOpCodeField instruction, IEmitField field)
        {
            il.Emit(instruction.ToOpCode(), ((ReflectionField)field).Field);
        }

        public void Emit(IEmitOpCodeInt instruction, int operand)
        {
            il.Emit(instruction.ToOpCode(), operand);
        }

        public void Emit(IEmitOpCodeShort instruction, short operand)
        {
            il.Emit(instruction.ToOpCode(), operand);
        }

        public void Emit(IEmitOpCodeByte instruction, byte operand)
        {
            il.Emit(instruction.ToOpCode(), operand);
        }

        public void Emit(IEmitOpCodeShort instruction, sbyte operand)
        {
            il.Emit(instruction.ToOpCode(), operand);
        }

        public void Emit(IEmitOpCodeLong instruction, long operand)
        {
            il.Emit(instruction.ToOpCode(), operand);
        }

        public void Emit(IEmitOpCodeDouble instruction, double operand)
        {
            il.Emit(instruction.ToOpCode(), operand);
        }

        public void Emit(IEmitOpCodeFloat instruction, float operand)
        {
            il.Emit(instruction.ToOpCode(), operand);
        }

        public void Emit(IEmitOpCodeString instruction, string operand)
        {
            il.Emit(instruction.ToOpCode(), operand);
        }

        public void Emit(IEmitOpCodeLocal instruction, IEmitLocal local)
        {
            il.Emit(instruction.ToOpCode(), ((ReflectionLocal)local).LocalBuilder);
        }

        public void Emit(IEmitOpCodeLabel instruction, IEmitLabel label)
        {
            il.Emit(instruction.ToOpCode(), ((ReflectionLabel)label).Label);
        }

        public void Emit(IEmitOpCodeLabelArray instruction, IEmitLabel[] labels)
        {
            il.Emit(instruction.ToOpCode(), labels.Select(x => ((ReflectionLabel)x).Label).ToArray());
        }
    }
}
