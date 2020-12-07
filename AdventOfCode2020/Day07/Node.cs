using System.Linq;
using System.Collections.Generic;


namespace Day07
{
    public class Node
    {
        private readonly List<Relation> _relations = new();


        public string Name { get; }

        public IEnumerable<Relation> Relations => this._relations.AsEnumerable();



        public Node(string name)
        {
            this.Name = name;
        }

        

        public void AddRelation(Node to, int count)
        {
            var rel = new Relation(this, to, count);

            this._relations.Add(rel);
            to._relations.Add(rel);
        }



        public IEnumerable<Node> SourceNodes() => this.Relations.Where(r => r.Destination == this).Select(r => r.Source).Union(this.Relations.Where(r => r.Destination == this).SelectMany(r => r.Source.SourceNodes()));

        public int DestinationsCount(int quantity = 1) => this.Relations.Where(r => r.Source == this).Sum(r => (r.Count * quantity) + r.Destination.DestinationsCount(r.Count * quantity));
    }


    public class Relation
    {
        public Node Source { get; }
        public Node Destination { get; }

        public int Count { get; }



        public Relation(Node from, Node to, int count)
        {
            this.Source = from;
            this.Destination = to;
            this.Count = count;
        }
    }
}
