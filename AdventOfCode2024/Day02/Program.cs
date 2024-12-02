using var file = new StreamReader("input.txt");


List<List<long>> reports = [];

while (file.ReadLine() is string line)
{
    var levels = line.Split(' ');

    reports.Add(levels.Select(long.Parse).ToList());
}



var resultOne = reports.Count(report =>
{
    var dif = report.Zip(report.Skip(1)).Select(x => x.First - x.Second).ToList();
    return (dif.All(x => x < 0) || dif.All(x => x > 0)) && dif.All(x => Math.Abs(x) >= 1 && Math.Abs(x) <= 3);
});

Console.WriteLine(resultOne);



var resultTwo = reports.Count(report =>
{
    var dif = report.Zip(report.Skip(1)).Select(x => x.First - x.Second).ToList();

    var isSafe = (dif.All(x => x < 0) || dif.All(x => x > 0)) && dif.Select(Math.Abs).All(x => x >= 1 && x <= 3);
    if (isSafe) { return true; }

    for (var i = 0; i < report.Count; i++)
    {
        var report2 = report.Where((x, index) => index != i).ToList();
        dif = report2.Zip(report2.Skip(1)).Select(x => x.First - x.Second).ToList();

        isSafe = (dif.All(x => x < 0) || dif.All(x => x > 0)) && dif.Select(Math.Abs).All(x => x >= 1 && x <= 3);
        if (isSafe) { return true; }
    }

    return false;
});

Console.WriteLine(resultTwo);