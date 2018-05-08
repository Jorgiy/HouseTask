namespace HouseTask
{
    public abstract class Stationare
    {
        protected Stationare(string name, string documentation)
        {
            Name = name;
            Documentation = documentation;
        }

        public string Name { get; }

        public string Documentation { get; }
    }
}