using System.Text;


namespace Day20
{
    public class Tile
    {
        private (string Top, string Bottom, string Left, string Right) _corners;


        public int Name { get; }
        public char[,] Data { get; }

        public int NeighboursCount { get; set; }

        public (string Top, string Bottom, string Left, string Right) Corners => this._corners;



        public Tile(int name, char[,] data)
        {
            this.Name = name;
            this.Data = data;

            this.PopulateCorners();
        }



        private void PopulateCorners()
        {
            StringBuilder builder = new();

            for (int i = 0; i < this.Data.GetLength(1); i++)
            {
                builder.Append(this.Data[0, i]);
            }
            
            this._corners.Top = builder.ToString();
            builder.Clear();


            for (int i = 0; i < this.Data.GetLength(1); i++)
            {
                builder.Append(this.Data[this.Data.GetLength(1) - 1, i]);
            }
            
            this._corners.Bottom = builder.ToString();
            builder.Clear();


            for (int i = 0; i < this.Data.GetLength(0); i++)
            {
                builder.Append(this.Data[i, 0]);
            }

            this._corners.Left = builder.ToString();
            builder.Clear();

            
            for (int i = 0; i < this.Data.GetLength(0); i++)
            {
                builder.Append(this.Data[i, this.Data.GetLength(0) - 1]);
            }

            this._corners.Right = builder.ToString();
        }
    }
}
