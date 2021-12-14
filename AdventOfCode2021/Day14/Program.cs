using var file = new StreamReader("input.txt");

var template = file.ReadLine();
file.ReadLine();

Dictionary<(char, char), char> rules = new();

while (file.ReadLine() is string line)
{
    rules.Add((line[0], line[1]), line[^1]);
}



Dictionary<(char, char), ulong> pairs = new();
for (int i = 0; i < template!.Length - 1; i++)
{
    var key = (template[i], template[i + 1]);
    Increment(pairs, key);
}

for (int c = 0; c < 10; c++)
{
    Dictionary<(char, char), ulong> prev = new(pairs);

    foreach (var (key, increment) in prev)
    {
        var insert = rules[key];

        Increment(pairs, (key.Item1, insert), increment);
        Increment(pairs, (insert, key.Item2), increment);
        Decrement(pairs, key, increment);
    }
}

Dictionary<char, ulong> frequencies = new();
foreach (var (key, value) in pairs)
{
    Increment(frequencies, key.Item1, value);
    Increment(frequencies, key.Item2, value);
}

frequencies[template[0]]++;
frequencies[template[^1]]++;

var resultOne = (frequencies.Max(f => f.Value) / 2) - (frequencies.Min(f => f.Value) / 2);
Console.WriteLine(resultOne);



pairs.Clear();
for (int i = 0; i < template!.Length - 1; i++)
{
    var key = (template[i], template[i + 1]);
    Increment(pairs, key);
}

for (int c = 0; c < 40; c++)
{
    Dictionary<(char, char), ulong> prev = new(pairs);

    foreach (var (key, increment) in prev)
    {
        var insert = rules[key];

        Increment(pairs, (key.Item1, insert), increment);
        Increment(pairs, (insert, key.Item2), increment);
        Decrement(pairs, key, increment);
    }
}

frequencies.Clear();
foreach (var (key, value) in pairs)
{
    Increment(frequencies, key.Item1, value);
    Increment(frequencies, key.Item2, value);
}

frequencies[template[0]]++;
frequencies[template[^1]]++;

var resultTwo = (frequencies.Max(f => f.Value) / 2) - (frequencies.Min(f => f.Value) / 2);
Console.WriteLine(resultTwo);



static void Increment<TKey>(Dictionary<TKey, ulong> dict, TKey key, ulong increment = 1) where TKey : notnull
    => dict[key] = dict.ContainsKey(key) ? dict[key] + increment : increment;

static void Decrement<TKey>(Dictionary<TKey, ulong> dict, TKey key, ulong decrement = 1) where TKey : notnull
    => dict[key] -= decrement;
