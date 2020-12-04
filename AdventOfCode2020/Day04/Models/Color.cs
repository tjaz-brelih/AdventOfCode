using System.Linq;


namespace Day04.Models
{
    public record Color
    {
        public string Hex { get; }



        private Color(string hex)
        {
            this.Hex = hex;
        }



        public static Color? FromString(string input)
        {
            var colorValid = input.Length == 7 && input.First() == '#' && input.Skip(1).All(c => char.IsDigit(c) || (c >= 'a' && c <= 'f'));
            if (!colorValid)
            {
                return null;
            }

            return new Color(input);
        }
    }
}
