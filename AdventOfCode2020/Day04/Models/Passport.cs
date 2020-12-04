using System;
using System.Linq;
using System.Collections.Generic;

using Day04.Constants;


namespace Day04.Models
{
    public class Passport
    {
        public string PassportId { get; }
        public string? CountryId { get; }

        public int IssueYear { get; }
        public int ExpirationYear { get; }

        public int BirthYear { get; }
        public Height Height { get; }
        public Color HairColor { get; }
        public EyeColor EyeColor { get; }


        public bool IsValid
            => (this.PassportId.Length == 9 && this.PassportId.All(char.IsDigit)) &&
               (this.IssueYear >= 2010 && this.IssueYear <= 2020) &&
               (this.ExpirationYear >= 2020 && this.ExpirationYear <= 2030) &&
               (this.BirthYear >= 1920 && this.BirthYear <= 2002) &&
               (this.Height.IsValid);



        public Passport(string passportId, string? countryId, int issueYear, int expirationYear, int birthYear, Height height, Color hairColor, EyeColor eyeColor)
        {
            this.PassportId = passportId;
            this.CountryId = countryId;

            this.IssueYear = issueYear;
            this.ExpirationYear = expirationYear;

            this.BirthYear = birthYear;
            this.Height = height;
            this.HairColor = hairColor;
            this.EyeColor = eyeColor;
        }



        public static bool ContainsRequiredProperties<T>(IDictionary<string, T> properties)
            => ContainsRequiredProperties(properties.Keys);

        public static bool ContainsRequiredProperties(IEnumerable<string> properties)
            => PassportProperty.Required.All(m => properties.Contains(m));

        public static bool ContainsRequiredProperties(ISet<string> properties)
            => PassportProperty.Required.IsSubsetOf(properties);


        public static Passport? FromDictionary(IDictionary<string, string> properties)
        {
            if (!ContainsRequiredProperties(properties))
            {
                return null;
            }


            if (
                !int.TryParse(properties[PassportProperty.IssueYear], out int issueYear) ||
                !int.TryParse(properties[PassportProperty.ExpirationYear], out int expirationYear) ||
                !int.TryParse(properties[PassportProperty.BirthYear], out int birthYear))
            {
                return null;
            }


            var height = Height.FromString(properties[PassportProperty.Height]);
            if (height is null)
            {
                return null;
            }

            var hairColor = Color.FromString(properties[PassportProperty.HairColor]);
            if(hairColor is null)
            {
                return null;
            }

            var eyeColor = properties[PassportProperty.EyeColor];
            if (!EyeColorName.ValidColors.Contains(eyeColor))
            {
                return null;
            }


            return new Passport(
                passportId: properties[PassportProperty.PassportId],
                countryId: properties.ContainsKey(PassportProperty.CountryId) ? properties[PassportProperty.CountryId] : null,

                issueYear: issueYear,
                expirationYear: expirationYear,

                birthYear: birthYear,
                height: height,
                hairColor: hairColor,
                eyeColor: new EyeColor(eyeColor)
            );
        }
    }
}
