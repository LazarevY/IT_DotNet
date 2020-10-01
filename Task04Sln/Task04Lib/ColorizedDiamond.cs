namespace Task04Lib
{
    public enum DiamondColor
    {
        White = 0,
        Blue = 1,
        Yellow = 2
    }

    public class ColorizedDiamond : Diamond
    {
        public ColorizedDiamond(
            string name,
            double weight,
            int facetingQuality,
            DiamondColor color)
            :
            base(name, weight, facetingQuality)
        {
            Color = color;
        }

        public DiamondColor Color { get; set; }

        public override double Quality()
        {
            switch (Color)
            {
                case DiamondColor.Blue:
                    return base.Quality() + 1;
                case DiamondColor.Yellow:
                    return base.Quality() - 0.5;
                default:
                    return base.Quality();
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}\n" +
                   $"Color: {Color.ToString()}";
        }
    }
}