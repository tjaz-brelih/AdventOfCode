using var file = new StreamReader("input.txt");


List<int> list1 = [];
List<int> list2 = [];

while (file.ReadLine() is string line)
{
    var span = line.AsSpan();

    var start = span.IndexOf(' ');
    var end = span.LastIndexOf(' ');

    list1.Add(int.Parse(span[..start]));
    list2.Add(int.Parse(span[(end + 1)..]));
}


list1.Sort();
list2.Sort();

var resultOne = list1.Zip(list2).Sum(x => Math.Abs(x.First - x.Second));

Console.WriteLine(resultOne);



var counter = new Dictionary<int, int>();

foreach (var item in list1)
{
    if (counter.ContainsKey(item)) { continue; }

    counter[item] = list2.Count(x => x == item);
}

var resultTwo = list1.Sum(x => x * counter[x]);

Console.WriteLine(resultTwo);