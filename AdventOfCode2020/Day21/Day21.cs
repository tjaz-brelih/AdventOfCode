using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;



using var file = new StreamReader("input.txt");

List<string> lines = new();

while (file.ReadLine() is string line)
{
    lines.Add(line);
}



Dictionary<ISet<string>, ISet<string>> foods = new();

foreach (var food in lines)
{
    var firstIndex = food.IndexOf('(');
    var lastIndex = food.LastIndexOf(')');

    var ingredients = food[..(firstIndex - 1)].Split(' ');
    var allergens = food[(firstIndex + 10)..lastIndex].Split(' ');

    foods[ingredients.ToHashSet()] = allergens.ToHashSet();
}

Dictionary<string, ISet<string>> ingredientsAllergens = new();

foreach (var food in foods)
{
    foreach (var ingredient in food.Key)
    {
        if (!ingredientsAllergens.ContainsKey(ingredient))
        {
            ingredientsAllergens[ingredient] = food.Value.ToHashSet();
        }
        else
        {
            ingredientsAllergens[ingredient].IntersectWith(food.Value);
        }
    }
}

Console.WriteLine(ingredientsAllergens.Count(kv => !kv.Value.Any()));