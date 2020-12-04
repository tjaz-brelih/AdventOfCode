using System;

using Day04.Constants;


namespace Day04.Models
{
    public record EyeColor
    {
        public string Color { get; }



        public EyeColor(string color)
        {
            this.Color = EyeColorName.ValidColors.Contains(color) ? color : throw new ArgumentException("Invalid color name", nameof(color));
        }
    }
}
