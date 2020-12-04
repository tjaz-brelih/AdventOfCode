using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Day04.Models;


var file = new StreamReader("input.txt");

Dictionary<string, string> data = new();

List<Passport?> passports = new();


while (file.ReadLine() is string line)
{
    if (line.Length == 0)
    {
        if (Passport.ContainsRequiredProperties(data))
        {
            passports.Add(Passport.FromDictionary(data));
        }

        data.Clear();
        continue;
    }


    foreach (var token in line.Split(' '))
    {
        var keyValue = token.Split(':');
        data[keyValue[0]] = keyValue[1];
    }
}

file.Close();


Console.WriteLine(passports.Count());

Console.WriteLine(passports.Where(p => p is not null).Count(p => p!.IsValid));
