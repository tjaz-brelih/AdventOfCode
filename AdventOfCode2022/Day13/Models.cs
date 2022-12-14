namespace Day13;


public class Item
{
    public int? Value { get; set; }
    public List<Item>? Items { get; set; }

    public bool IsInt => this.Value is not null;
    public bool IsList => !this.IsInt;


    public Item(int value)
    {
        this.Value = value;
    }

    public Item(List<Item> items)
    {
        this.Items = items;
    }
}