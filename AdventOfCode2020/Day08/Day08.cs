using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Day08;



using var file = new StreamReader("input.txt");

List<Instruction> instructions = new();

while (file.ReadLine() is string line)
{
    instructions.Add(Instruction.FromString(line));
}



VirtualMachine vm = new(instructions);

Instruction nextInstruction;
while ((nextInstruction = vm.NextInstruction).ExecutedCount < 1)
{
    vm.ExecuteNext();
}

Console.WriteLine(vm.Accumulator);




var nopAndJmpInstructions = instructions.Where(i => (i is NopInstruction && i.Argument != 0) || i is JmpInstruction).ToList();

foreach (var instruction in nopAndJmpInstructions)
{
    Instruction newInstruction = instruction switch
    {
        NopInstruction => new JmpInstruction(instruction.Argument),
        JmpInstruction => new NopInstruction(instruction.Argument),
        _ => throw new Exception()
    };

    var index = vm.Instructions.IndexOf(instruction);
    vm.Instructions[index] = newInstruction;

    
    vm.Reset();

    while (!vm.Finished && (nextInstruction = vm.NextInstruction).ExecutedCount < 1)
    {
        vm.ExecuteNext();
    }

    if (vm.Finished)
    {
        break;
    }


    vm.Instructions[index] = instruction;
}

Console.WriteLine(vm.Accumulator);
