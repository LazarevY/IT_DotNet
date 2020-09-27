namespace Task04Lib
{
    public enum DiamondColor
    {
        White,
        Blue,
        Yellow
    }

    public class ColorizedDiamond : Diamond
    {
        private DiamondColor _color;

        public ColorizedDiamond(
            string name,
            double weight,
            int facetingQuality,
            DiamondColor color)
            :
            base(name, weight, facetingQuality)
        {
            _color = color;
        }

        public DiamondColor Color
        {
            get => _color;
            set => _color = value;
        }

        public override double Quality()
        {
            switch (_color)
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
            return $"Diamond name: {_name}\n" +
                   $"Diamond weight in carats: {_weight}\n" +
                   $"Faceting quality: {_facetingQuality}\n" +
                   $"Color: {_color.ToString()}\n" +
                   $"Quality: {Quality()}";
        }
    }
}