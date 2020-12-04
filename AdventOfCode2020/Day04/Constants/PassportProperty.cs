using System.Collections.Generic;


namespace Day04.Constants
{
    public static class PassportProperty
    {
        public const string PassportId = "pid";
        public const string CountryId = "cid";

        public const string IssueYear = "iyr";
        public const string ExpirationYear = "eyr";
        
        public const string BirthYear = "byr";
        public const string Height = "hgt";
        public const string HairColor = "hcl";
        public const string EyeColor = "ecl";



        public static readonly ISet<string> Required = new HashSet<string>
        {
            PassportId,

            IssueYear,
            ExpirationYear,

            BirthYear,
            Height,
            HairColor,
            EyeColor
        };
    }
}
