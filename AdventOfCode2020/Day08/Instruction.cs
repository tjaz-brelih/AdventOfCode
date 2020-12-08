using System;


namespace Day08
{
    public abstract class Instruction
    {
        public string Name { get; }
        public int Argument { get; }

        public int ExecutedCount { get; private set; }



        protected Instruction(string name, int argument)
        {
            this.Name = name;
            this.Argument = argument;
        }



        public static Instruction FromString(string instruction)
        {
            var tokens = instruction.Split(' ');

            var name = tokens[0];
            var arg = int.Parse(tokens[1]);

            return name switch
            {
                NopInstruction.Operation => new NopInstruction(arg),
                AccInstruction.Operation => new AccInstruction(arg),
                JmpInstruction.Operation => new JmpInstruction(arg),
                _ => throw new ArgumentOutOfRangeException(nameof(instruction))
            };
        }



        public void Reset()
        {
            this.ExecutedCount = default;
        }


        public void Execute(VirtualMachine vm)
        {
            this.ExecuteInstruction(vm);
            this.ExecutedCount++;
        }

        protected abstract void ExecuteInstruction(VirtualMachine vm);
    }


    public class NopInstruction : Instruction
    {
        public const string Operation = "nop";



        public NopInstruction(int argument)
            : base(Operation, argument) { }



        protected override void ExecuteInstruction(VirtualMachine vm)
        {

        }
    }


    public class AccInstruction : Instruction
    {
        public const string Operation = "acc";



        public AccInstruction(int argument)
            : base(Operation, argument) { }



        protected override void ExecuteInstruction(VirtualMachine vm)
        {
            vm.Accumulator += this.Argument;
        }
    }


    public class JmpInstruction : Instruction
    {
        public const string Operation = "jmp";



        public JmpInstruction(int argument)
            : base(Operation, argument) { }



        protected override void ExecuteInstruction(VirtualMachine vm)
        {
            vm.InstructionPointer += this.Argument - 1;
        }
    }
}
