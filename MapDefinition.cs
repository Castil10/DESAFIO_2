using System;
using System.Collections.Generic;
using System.Linq;
using TechopolisTransport.Domain;

namespace TechopolisTransport
{
    public class MapDefinition
    {
        public string Name { get; }
        public IReadOnlyList<Station> Stations { get; }
        public IReadOnlyList<MapRoute> Routes { get; }

        public MapDefinition(string name, IEnumerable<Station> stations, IEnumerable<MapRoute> routes)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Stations = stations?.ToList() ?? throw new ArgumentNullException(nameof(stations));
            Routes = routes?.ToList() ?? throw new ArgumentNullException(nameof(routes));
        }

        public void Apply(Graph graph)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));

            graph.Clear();
            var stationById = Stations.ToDictionary(s => s.Id, StringComparer.OrdinalIgnoreCase);

            foreach (var station in Stations)
            {
                graph.AddStation(station);
            }

            foreach (var route in Routes)
            {
                if (!stationById.TryGetValue(route.FromId, out var from) ||
                    !stationById.TryGetValue(route.ToId, out var to))
                {
                    throw new InvalidOperationException($"Ruta inválida en el mapa {Name}: {route.FromId} -> {route.ToId}");
                }

                graph.AddRoute(from, to, route.TimeMinutes);
            }
        }
    }

    public record MapRoute(string FromId, string ToId, int TimeMinutes);
}
