namespace Day11;

public class Monkey
{
    public List<ulong> Items { get; set; } = new();

    public Func<ulong, ulong> Operation { get; set; }

    public ulong Test { get; set; }

    public int TrueThrow { get; set; }
    public int FalseThrow { get; set; }

    public ulong CountInspected { get; set; } = 0;


    public Monkey Clone()
    {
        return new Monkey()
        {
            Items = this.Items.ToList(),
            Operation = this.Operation,
            Test = this.Test,
            TrueThrow = this.TrueThrow,
            FalseThrow = this.FalseThrow
        };
    }

    public int ThrowToMonkey(ulong val) => val % this.Test == 0 ? this.TrueThrow: this.FalseThrow;
}