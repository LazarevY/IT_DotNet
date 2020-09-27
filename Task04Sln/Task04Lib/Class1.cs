namespace Task04Lib
{
    public class Diamond
    {
        protected string _name;
        protected double _weight;
        protected int _facetingQuality;

        public Diamond(string name, double weight, int facetingQuality)
        {
            _name = name;
            _weight = weight;
            _facetingQuality = facetingQuality;
        }

        public virtual double Quality() => 0.4 * _weight + 0.6 * _facetingQuality;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public double Weight
        {
            get => _weight;
            set => _weight = value;
        }

        public int FacetingQuality
        {
            get => _facetingQuality;
            set => _facetingQuality = value;
        }
    }
}