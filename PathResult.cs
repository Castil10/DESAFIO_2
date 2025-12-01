using System.Collections.Generic;

namespace TechopolisTransport.Domain
{
    public class PathResult
    {
        public List<Station> Path { get; }
        public int TotalTime { get; }

        public bool HasPath => Path.Count > 0 && TotalTime < int.MaxValue;

        public PathResult(List<Station> path, int totalTime)
        {
            Path = path;
            TotalTime = totalTime;
        }
    }
}
