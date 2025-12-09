using System.Numerics;

namespace Common;

public static class Helpers
{
    public static List<string> ReadInputFile(string filePath = "input.txt")
    {
        using var file = new StreamReader(filePath);

        List<string> lines = [];

        while (file.ReadLine() is string line)
        {
            lines.Add(line);
        }

        return lines;
    }


    public static T LeastCommonMultiple<T>(IEnumerable<T> a) where T : INumber<T>
        => LcmGcdInternal(a, (a, b) => LeastCommonMultiple(a, b));

    public static T GreatestCommonDivisor<T>(IEnumerable<T> a) where T : INumber<T>
        => LcmGcdInternal(a, (a, b) => GreatestCommonDivisor(a, b));


    private static T LcmGcdInternal<T>(IEnumerable<T> a, Func<T, T, T> f) where T : INumber<T>
    {
        var stack = new Stack<T>(a);

        if (stack.Count == 0) throw new ArgumentException("Empty collection", nameof(a));
        while (stack.Count > 1) stack.Push(f(stack.Pop(), stack.Pop()));

        return stack.Pop();
    }


    public static T LeastCommonMultiple<T>(T a, T b) where T : INumber<T> => a * b / GreatestCommonDivisor(a, b);

    public static T GreatestCommonDivisor<T>(T a, T b) where T : INumber<T>
    {
        while (b != T.Zero)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}