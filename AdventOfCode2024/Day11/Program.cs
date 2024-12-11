using Common;

using Map = System.Collections.Generic.Dictionary<long, long>;


var lines = Helpers.ReadInputFile()[0];

var stonesOriginal = lines.Split(' ').Select(long.Parse).ToDictionary(x => x, _ => 1L);

int[] blinks = [25, 75];
Map stonesNew = [];

foreach (var b in blinks)
{
    Map stones = new(stonesOriginal);

    for (int i = 0; i < b; i++)
    {
        stonesNew.Clear();

        foreach (var (stone, count) in stones)
        {
            int digits = (int) (Math.Log10(stone) + 1);

            if (stone == 0) { InsertOrUpdate(stonesNew, 1, count); }
            else if (digits % 2 == 0)
            {
                var divider = (int) Math.Pow(10, digits / 2);
                var left = stone / divider;
                var right = stone - (left * divider);

                InsertOrUpdate(stonesNew, left, count);
                InsertOrUpdate(stonesNew, right, count);
            }
            else { InsertOrUpdate(stonesNew, stone * 2024, count); }
        }

        stones = new(stonesNew);
    }

    var result = stones.Aggregate(0L, (acc, x) => acc + x.Value);
    Console.WriteLine(result);
}


static void InsertOrUpdate(Map map, long el, long count) => map[el] = map.TryGetValue(el, out var val) ? val + count : count;