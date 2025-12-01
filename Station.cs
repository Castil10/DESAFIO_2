using System;

namespace TechopolisTransport.Domain
{
    public class Station : IEquatable<Station>
    {
        public string Id { get; }
        public string Name { get; }

        public Station(string id, string name)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override string ToString() => Name;

        public bool Equals(Station? other)
        {
            if (other is null) return false;
            return Id.Equals(other.Id, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object? obj) => Equals(obj as Station);

        public override int GetHashCode() => Id.ToLowerInvariant().GetHashCode();
    }
}

