namespace Day12
{
    internal class Cave
    {
        public string Name { get; }
        public bool IsSmall { get; }

        public List<Cave> ConnectedCaves { get; } = new();



        public Cave(string name)
        {
            this.Name = name;
            this.IsSmall = char.IsLower(this.Name[0]);
        }
    }
}
