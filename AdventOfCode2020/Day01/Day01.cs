using System;
using System.IO;
using System.Collections.Generic;


using var file = new StreamReader("input.txt");

List<int> expenses = new();

while (file.ReadLine() is string line)
{
    expenses.Add(int.Parse(line));
}


Console.WriteLine(TwoSum(expenses));
Console.WriteLine(ThreeSum(expenses));




static int? TwoSum(IList<int> input)
{
    for (int i = 0; i < input.Count - 1; i++)
    {
        for (int j = i + 1; j < input.Count; j++)
        {
            var sum = input[i] + input[j];
            if (sum == 2020)
            {
                return input[i] * input[j];
            }
        }
    }

    return null;
}



static int? ThreeSum(IList<int> input)
{
    for (int i = 0; i < input.Count - 2; i++)
    {
        for (int j = i + 1; j < input.Count - 1; j++)
        {
            for (int k = j + 1; k < input.Count; k++)
            {
                var sum = input[i] + input[j]  + input[k];
                if (sum == 2020)
                {
                    return input[i] * input[j] * input[k];
                }
            }
        }
    }

    return null;
}
