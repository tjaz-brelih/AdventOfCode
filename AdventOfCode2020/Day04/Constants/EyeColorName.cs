using System.Collections.Generic;


namespace Day04.Constants
{
    public static class EyeColorName
    {
        public const string Amber = "amb";
        public const string Blue = "blu";
        public const string Brown = "brn";
        public const string Gray = "gry";
        public const string Green = "grn";
        public const string Hazel = "hzl";
        public const string Other = "oth";



        public static readonly ISet<string> ValidColors = new HashSet<string>
        {
            Amber,
            Blue,
            Brown,
            Gray,
            Green,
            Hazel,
            Other
        };
    }
}
