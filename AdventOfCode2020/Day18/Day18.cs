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
    var firstIndex = input.IndexOf('(');
    var lastIndex = input.IndexOf(')');

    var expressionValue = 0L;

    if (firstIndex >= 0 && lastIndex >= 0)
    {
        expressionValue = EvaluateExpression(input[(firstIndex + 1)..lastIndex]);

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