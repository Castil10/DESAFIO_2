namespace ShopSmart.Core.DataStructures;

/// <summary>
/// Tabla hash simple con encadenamiento separado para colisiones.
/// </summary>
public class HashTableCustom<TKey, TValue> where TKey : notnull
{
    private readonly List<KeyValuePair<TKey, TValue>>[] _buckets;

    public HashTableCustom(int capacity = 101)
    {
        _buckets = new List<KeyValuePair<TKey, TValue>>[capacity];
        for (int i = 0; i < capacity; i++)
        {
            _buckets[i] = new List<KeyValuePair<TKey, TValue>>();
        }
    }

    private int GetBucketIndex(TKey key)
    {
        return Math.Abs(key.GetHashCode()) % _buckets.Length;
    }

    public void AddOrUpdate(TKey key, TValue value)
    {
        int index = GetBucketIndex(key);
        var bucket = _buckets[index];

        for (int i = 0; i < bucket.Count; i++)
        {
            if (EqualityComparer<TKey>.Default.Equals(bucket[i].Key, key))
            {
                bucket[i] = new KeyValuePair<TKey, TValue>(key, value);
                return;
            }
        }

        bucket.Add(new KeyValuePair<TKey, TValue>(key, value));
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        int index = GetBucketIndex(key);
        var bucket = _buckets[index];

        foreach (var pair in bucket)
        {
            if (EqualityComparer<TKey>.Default.Equals(pair.Key, key))
            {
                value = pair.Value;
                return true;
            }
        }

        value = default!;
        return false;
    }

    public bool Remove(TKey key)
    {
        int index = GetBucketIndex(key);
        var bucket = _buckets[index];

        for (int i = 0; i < bucket.Count; i++)
        {
            if (EqualityComparer<TKey>.Default.Equals(bucket[i].Key, key))
            {
                bucket.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public IEnumerable<KeyValuePair<TKey, TValue>> Items
    {
        get
        {
            foreach (var bucket in _buckets)
            {
                foreach (var pair in bucket)
                {
                    yield return pair;
                }
            }
        }
    }
}
