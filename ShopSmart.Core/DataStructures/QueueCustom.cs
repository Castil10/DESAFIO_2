namespace ShopSmart.Core.DataStructures;

/// <summary>
/// Cola genérica utilizada para gestionar pedidos pendientes.
/// </summary>
public class QueueCustom<T>
{
    private readonly LinkedList<T> _items = new();

    public int Count => _items.Count;

    public void Enqueue(T value)
    {
        _items.AddLast(value);
    }

    public T Dequeue()
    {
        if (_items.First is null)
        {
            throw new InvalidOperationException("La cola está vacía.");
        }

        var value = _items.First.Value;
        _items.RemoveFirst();
        return value;
    }

    public T Peek()
    {
        if (_items.First is null)
        {
            throw new InvalidOperationException("La cola está vacía.");
        }

        return _items.First.Value;
    }

    public IEnumerable<T> Recorrer()
    {
        return _items;
    }
}
