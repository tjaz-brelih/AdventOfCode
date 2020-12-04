using System.Collections.Generic;


namespace Day04.Constants
{
    public static class HeightUnit
    {
        public const string Centimeter = "cm";
        public const string Inch = "in";



        public static readonly ISet<string> ValidUnits = new HashSet<string>
        {
            Centimeter,
            Inch
        };
    }
}
