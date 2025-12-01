using System;
using System.Collections.Generic;
using System.Linq;

namespace TechopolisTransport.Domain
{
    public class Graph
    {
        private readonly Dictionary<Station, List<Route>> _adjacency =
            new Dictionary<Station, List<Route>>();

        public IReadOnlyCollection<Station> Stations => _adjacency.Keys;

        public void Clear()
        {
            _adjacency.Clear();
        }

        public void AddStation(Station station)
        {
            if (!_adjacency.ContainsKey(station))
                _adjacency[station] = new List<Route>();
        }

        // Grafo NO dirigido: se agrega en ambos sentidos
        public void AddRoute(Station from, Station to, int timeMinutes)
        {
            if (timeMinutes <= 0)
                throw new ArgumentException("El tiempo debe ser mayor a cero.", nameof(timeMinutes));

            AddStation(from);
            AddStation(to);

            // from -> to
            var route = _adjacency[from].FirstOrDefault(r => r.To.Equals(to));
            if (route == null)
                _adjacency[from].Add(new Route(from, to, timeMinutes));
            else
                route.TimeMinutes = timeMinutes;

            // to -> from
            var backRoute = _adjacency[to].FirstOrDefault(r => r.To.Equals(from));
            if (backRoute == null)
                _adjacency[to].Add(new Route(to, from, timeMinutes));
            else
                backRoute.TimeMinutes = timeMinutes;
        }

        public IEnumerable<Route> GetRoutes()
        {
            var set = new HashSet<string>();

            foreach (var kvp in _adjacency)
            {
                foreach (var r in kvp.Value)
                {
                    // clave única sin duplicar A-B y B-A
                    var key = string.Join("->",
                        new[] { r.From.Id, r.To.Id }.OrderBy(x => x));

                    if (set.Add(key))
                        yield return r;
                }
            }
        }

        public Station? FindStationByName(string name)
        {
            return _adjacency.Keys.FirstOrDefault(
                s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        // ---------- BFS ----------
        public List<Station> Bfs(Station origin)
        {
            var visited = new HashSet<Station>();
            var queue = new Queue<Station>();
            var result = new List<Station>();

            visited.Add(origin);
            queue.Enqueue(origin);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(current);

                foreach (var route in _adjacency[current])
                {
                    if (visited.Add(route.To))
                        queue.Enqueue(route.To);
                }
            }

            return result;
        }

        // ---------- DFS ----------
        public List<Station> Dfs(Station origin)
        {
            var visited = new HashSet<Station>();
            var result = new List<Station>();

            void DfsVisit(Station node)
            {
                visited.Add(node);
                result.Add(node);

                foreach (var route in _adjacency[node])
                {
                    if (!visited.Contains(route.To))
                        DfsVisit(route.To);
                }
            }

            DfsVisit(origin);
            return result;
        }

        // ---------- Dijkstra: ruta más corta ----------
        public PathResult ShortestPath(Station origin, Station destination)
        {
            var dist = new Dictionary<Station, int>();
            var prev = new Dictionary<Station, Station?>();
            var unvisited = new List<Station>(_adjacency.Keys);

            foreach (var v in unvisited)
            {
                dist[v] = int.MaxValue;
                prev[v] = null;
            }

            dist[origin] = 0;

            while (unvisited.Count > 0)
            {
                var u = unvisited.OrderBy(v => dist[v]).First();
                unvisited.Remove(u);

                if (u.Equals(destination))
                    break;

                if (dist[u] == int.MaxValue)
                    break; // no hay camino alcanzable

                foreach (var route in _adjacency[u])
                {
                    var alt = dist[u] + route.TimeMinutes;
                    if (alt < dist[route.To])
                    {
                        dist[route.To] = alt;
                        prev[route.To] = u;
                    }
                }
            }

            if (dist[destination] == int.MaxValue)
                return new PathResult(new List<Station>(), int.MaxValue);

            var path = new List<Station>();
            var curr = destination;

            while (curr != null)
            {
                path.Insert(0, curr);
                curr = prev[curr]!;
            }

            return new PathResult(path, dist[destination]);
        }
    }
}
