namespace ShopSmart.Core.EstructurasDatos;

/// <summary>
/// Tabla hash sencilla con encadenamiento, alternativa liviana a Dictionary.
/// </summary>
public class HashTableCustom<TKey, TValue> where TKey : notnull
{
    private class Entrada
    {
        public TKey Clave { get; set; }
        public TValue Valor { get; set; }

        public Entrada(TKey clave, TValue valor)
        {
            Clave = clave;
            Valor = valor;
        }
    }

    private readonly List<List<Entrada>> _buckets;

    public HashTableCustom(int capacidad = 97)
    {
        if (capacidad <= 0)
        {
            capacidad = 97;
        }

        _buckets = Enumerable.Range(0, capacidad).Select(_ => new List<Entrada>()).ToList();
    }

    public void Add(TKey key, TValue value)
    {
        var bucket = ObtenerBucket(key);
        foreach (var entrada in bucket)
        {
            if (EqualityComparer<TKey>.Default.Equals(entrada.Clave, key))
            {
                throw new ArgumentException("La clave ya existe en la tabla");
            }
        }

        bucket.Add(new Entrada(key, value));
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var bucket = ObtenerBucket(key);
        foreach (var entrada in bucket)
        {
            if (EqualityComparer<TKey>.Default.Equals(entrada.Clave, key))
            {
                value = entrada.Valor;
                return true;
            }
        }

        value = default!;
        return false;
    }

    public bool Remove(TKey key)
    {
        var bucket = ObtenerBucket(key);
        for (int i = 0; i < bucket.Count; i++)
        {
            if (EqualityComparer<TKey>.Default.Equals(bucket[i].Clave, key))
            {
                bucket.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public TValue this[TKey key]
    {
        get
        {
            if (TryGetValue(key, out var value))
            {
                return value;
            }

            throw new KeyNotFoundException("Clave no encontrada");
        }
        set
        {
            var bucket = ObtenerBucket(key);
            for (int i = 0; i < bucket.Count; i++)
            {
                if (EqualityComparer<TKey>.Default.Equals(bucket[i].Clave, key))
                {
                    bucket[i].Valor = value;
                    return;
                }
            }

            bucket.Add(new Entrada(key, value));
        }
    }

    private List<Entrada> ObtenerBucket(TKey key)
    {
        int index = Math.Abs(key.GetHashCode()) % _buckets.Count;
        return _buckets[index];
    }
}
