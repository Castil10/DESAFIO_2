namespace TechopolisTransport.Domain
{
    public class Route
    {
        public Station From { get; }
        public Station To { get; }
        public int TimeMinutes { get; set; }

        public Route(Station from, Station to, int timeMinutes)
        {
            From = from;
            To = to;
            TimeMinutes = timeMinutes;
        }

        public override string ToString()
            => $"{From.Name} -> {To.Name} ({TimeMinutes} min)";
    }
}
