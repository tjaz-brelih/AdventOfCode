using System.Linq;
using System.Collections.Generic;


namespace Day07
{
    public class Bag
    {
        public string Name { get; }
        public int Count { get; }

        public List<Bag> ContainedBags { get; } = new();



        public Bag(string name)
        {
            this.Name = name;
            this.Count = 1;
        }

        public Bag(string name, int count)
        {
            this.Name = name;
            this.Count = count;
        }



        public void Contains(string bagName, ICollection<Bag> bags)
        {
            if (this.Contains(bagName))
            {
                bags.Add(this);
            }

            foreach (var bag in this.ContainedBags)
            {
                bag.Contains(bagName, bags);
            }
        }


        public bool Contains(string bagName) => this.ContainedBags.Any(b => b.Name == bagName || b.Contains(bagName));
    }
}
