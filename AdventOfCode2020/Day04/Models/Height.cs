using System;

using Day04.Constants;


namespace Day04.Models
{
    public record Height
    {
        public string Unit { get; }
        public int Measure { get; }

        public bool IsValid => this.Unit switch
        {
            HeightUnit.Centimeter => this.Measure is >= 150 and <= 193,
            HeightUnit.Inch => this.Measure is >= 59 and <= 76,
            _ => false
        };



        public Height(string unit, int measure)
        {
            this.Unit = HeightUnit.ValidUnits.Contains(unit) ? unit : throw new ArgumentException("Invalid unit.", nameof(unit));
            this.Measure = measure;
        }



        public static Height? FromString(string height)
        {
            string unit;

            if (int.TryParse(height[0..^2], out int measure) && HeightUnit.ValidUnits.Contains(unit = height[^2..]))
            {
                return new Height(unit, measure);
            };

            return null;
        }
    }
}
