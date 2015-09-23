using System;
using System.Collections.Generic;
using System.Linq;
using Sexy.Emit.OpCodes;
using Sexy.Emit.Utils;

namespace Sexy.Emit
{
    public class EmitIl
    {
        private readonly List<EmitInstruction> instructions = new List<EmitInstruction>();
        private readonly List<EmitLocal> locals = new List<EmitLocal>();
        private readonly List<EmitLabel> labels = new List<EmitLabel>();
        private readonly List<EmitLabel> pendingLabels = new List<EmitLabel>();

        public IReadOnlyList<EmitInstruction> Instructions => instructions;
        public IReadOnlyList<EmitLocal> Locals => locals;
        public IReadOnlyList<EmitLabel> Labels => labels;

        private void AddInstruction(EmitInstruction instruction)
        {
            instructions.Add(instruction);
            if (pendingLabels.Any())
            {
                foreach (var label in pendingLabels)
                {
                    label.TargetInstruction = instruction;
                }
                pendingLabels.Clear();
            }
        }

        public EmitLocal DeclareLocal(EmitType type)
        {
            var local = new EmitLocal(type);
            locals.Add(local);
            return local;
        }

        public EmitLabel DefineLabel()
        {
            var label = new EmitLabel();
            labels.Add(label);
            return label;
        }

        public void MarkLabel(EmitLabel label)
        {
            pendingLabels.Add(label);
        }

        public EmitInstruction Emit(EmitOpCode opCode, Nothing nothing)
        {
            throw new NotImplementedException();
        }

        public EmitInstruction Emit(IEmitOpCodeVoid opCode)
        {
            var instruction = new EmitInstruction(opCode, null);
            AddInstruction(instruction);
            return instruction;
        }

        public EmitInstruction Emit(IEmitOpCodeType opCode, EmitType type)
        {
            var instruction = new EmitInstruction(opCode, type);
            AddInstruction(instruction);
            return instruction;
        }

        public EmitInstruction Emit(IEmitOpCodeMethod opCode, EmitMethod method)
        {
            var instruction = new EmitInstruction(opCode, method);
            AddInstruction(instruction);
            return instruction;
        }

        public EmitInstruction Emit(IEmitOpCodeConstructor opCode, EmitConstructor constructor)
        {
            var instruction = new EmitInstruction(opCode, constructor);
            AddInstruction(instruction);
            return instruction;
        }

        public EmitInstruction Emit(IEmitOpCodeField opCode, EmitField field)
        {
            var instruction = new EmitInstruction(opCode, field);
            AddInstruction(instruction);
            return instruction;
        }

        public EmitInstruction Emit(IEmitOpCodeInt opCode, int operand)
        {
            var instruction = new EmitInstruction(opCode, operand);
            AddInstruction(instruction);
            return instruction;
        }

        public EmitInstruction Emit(IEmitOpCodeShort opCode, short operand)
        {
            var instruction = new EmitInstruction(opCode, operand);
            AddInstruction(instruction);
            return instruction;
        }

        public EmitInstruction Emit(IEmitOpCodeByte opCode, byte operand)
        {
            var instruction = new EmitInstruction(opCode, operand);
            AddInstruction(instruction);
            return instruction;
        }

        public EmitInstruction Emit(IEmitOpCodeShort opCode, sbyte operand)
        {
            var instruction = new EmitInstruction(opCode, operand);
            AddInstruction(instruction);
            return instruction;
        }

        public EmitInstruction Emit(IEmitOpCodeLong opCode, long operand)
        {
            var instruction = new EmitInstruction(opCode, operand);
            AddInstruction(instruction);
            return instruction;
        }

        public EmitInstruction Emit(IEmitOpCodeDouble opCode, double operand)
        {
            var instruction = new EmitInstruction(opCode, operand);
            AddInstruction(instruction);
            return instruction;
        }

        public EmitInstruction Emit(IEmitOpCodeFloat opCode, float operand)
        {
            var instruction = new EmitInstruction(opCode, operand);
            AddInstruction(instruction);
            return instruction;
        }

        public EmitInstruction Emit(IEmitOpCodeString opCode, string operand)
        {
            var instruction = new EmitInstruction(opCode, operand);
            AddInstruction(instruction);
            return instruction;
        }

        public EmitInstruction Emit(IEmitOpCodeLocal opCode, EmitLocal local)
        {
            var instruction = new EmitInstruction(opCode, local);
            AddInstruction(instruction);
            return instruction;
        }

        public EmitInstruction Emit(IEmitOpCodeLabel opCode, EmitLabel label)
        {
            var instruction = new EmitInstruction(opCode, label);
            AddInstruction(instruction);
            return instruction;
        }

        public EmitInstruction Emit(IEmitOpCodeLabelArray opCode, EmitLabel[] labels)
        {
            var instruction = new EmitInstruction(opCode, labels);
            AddInstruction(instruction);
            return instruction;
        }
    }
}
