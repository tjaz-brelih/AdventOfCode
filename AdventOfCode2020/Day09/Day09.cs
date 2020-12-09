using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


using var file = new StreamReader("input.txt");

List<long> numbers = new();

while (file.ReadLine() is string line)
{
    numbers.Add(long.Parse(line));
}



var preambleLength = 25;
var resultIndex = 0;

for (int i = preambleLength; i < numbers.Count; i++)
{
    if (!IsValid(numbers, i, preambleLength))
    {
        resultIndex = i;
        break;
    }
}

var resultOne = numbers[resultIndex];
Console.WriteLine(resultOne);



var resultTwo = 0L;

for (int i = 0; i < numbers.Count - 1; i++)
{
    var currentSum = 0L;
    var index = i;

    while (currentSum < resultOne)
    {
        currentSum += numbers[index++];

        if (index >= numbers.Count)
        {
            break;
        }
    }

    if (currentSum == resultOne)
    {
        var numbersRange = numbers
            .Skip(i)
            .Take(index - i);

        resultTwo = numbersRange.Min() +  numbersRange.Max();

        break;
    }
}

Console.WriteLine(resultTwo);



static bool IsValid(IList<long> numbers, int index, int preambleLength)
{
    var currentNumber = numbers[index];

    for (int i = 0; i < preambleLength - 1; i++)
    {
        for (int j = i + 1; j < preambleLength; j++)
        {
            if (currentNumber == numbers[index - preambleLength + i] + numbers[index - preambleLength + j])
            {
                return true;
            }
        }
    }

    return false;
}