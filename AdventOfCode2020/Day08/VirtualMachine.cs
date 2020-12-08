using System.Collections.Generic;


namespace Day08
{
    public class VirtualMachine
    {
        public int InstructionPointer { get; set; }
        public long Accumulator { get; set; }

        public IList<Instruction> Instructions { get; }



        public VirtualMachine(IList<Instruction> instructions)
        {
            this.Instructions = instructions;
        }



        public Instruction NextInstruction => this.Instructions[this.InstructionPointer];

        public bool Finished => this.InstructionPointer == this.Instructions.Count;

        public void ExecuteNext()
        {
            var instruction = this.Instructions[this.InstructionPointer];

            instruction.Execute(this);

            this.InstructionPointer++;
        }


        public void Reset()
        {
            this.Accumulator = default;
            this.InstructionPointer = default;

            foreach (var instruction in this.Instructions)
            {
                instruction.Reset();
            }
        }
    }
}
