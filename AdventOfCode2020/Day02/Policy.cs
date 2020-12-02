using System.Linq;


namespace Day02
{
    public class Policy
    {
        public int Min { get; }
        public int Max { get; }

        public char Letter { get; }

        public string Password { get; }



        public Policy(int min, int max, char letter, string password)
        {
            this.Min = min;
            this.Max = max;
            this.Letter = letter;
            this.Password = password;
        }



        public bool CheckPasswordAgainstCountPolicy()
        {
            var count = this.Password.Count(c => c == this.Letter);

            return count >= this.Min && count <= this.Max;
        }

        public bool CheckPasswordAgainstPositionPolicy()
        {
            return (this.Password[this.Min - 1] == this.Letter) ^ (this.Password[this.Max - 1] == this.Letter);
        }
    }
}
