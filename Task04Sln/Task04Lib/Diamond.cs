namespace Task04Lib
{
    public class Diamond
    {
        public Diamond(string name, double weight, int facetingQuality)
        {
            Name = name;
            Weight = weight;
            FacetingQuality = facetingQuality;
        }

        public string Name { get; set; }

        public double Weight { get; set; }

        public int FacetingQuality { get; set; }

        public virtual double Quality()
        {
            return 0.4 * Weight + 0.6 * FacetingQuality;
        }

        public override string ToString()
        {
            return $"Diamond name: {Name}\n" +
                   $"Diamond weight in carats: {Weight:######0.##}\n" +
                   $"Faceting quality: {FacetingQuality}\n" +
                   $"Quality: {Quality():######0.####}";
        }
    }
}