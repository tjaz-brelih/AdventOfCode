using System;
using System.IO;
using System.Collections.Generic;



using var file = new StreamReader("input.txt");

List<string> lines = new();

while (file.ReadLine() is string line)
{
    lines.Add(line.Replace(" ", ""));
}





static long EvaluateExpression(ReadOnlySpan<char> input)
{
    var startIndex = input.IndexOf('(');
    var endIndex = input.IndexOf(')');

    var expressionValue = 0L;

    if (startIndex >= 0 && endIndex >= 0)
    {
        expressionValue = EvaluateExpression(input[(startIndex + 1)..endIndex]);
    }
    else
    {
        var currentIndex = 0;
        var value = 0L;

        do
        {
            var operationIndex = input[currentIndex..].IndexOfAny('+', '*');
            if (operationIndex == -1)
            {
                value = long.Parse(input);
                break;
            }

            currentIndex += operationIndex + 1;

            var rightOperand = long.Parse(input[currentIndex..input.IndexOfAny('+', '*')]);
            var operation = input[input.IndexOfAny('+', '*')];

            value = operation switch
            {
                '+' => value + rightOperand,
                '*' => value * rightOperand,
                _ => throw new InvalidOperationException()
            };

        } while (true);
    }


    return expressionValue;
}