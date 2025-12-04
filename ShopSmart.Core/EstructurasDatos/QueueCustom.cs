namespace ShopSmart.Core.EstructurasDatos;

/// <summary>
/// Cola utilizada para manejar pedidos en espera.
/// </summary>
public class QueueCustom<T>
{
    private readonly LinkedList<T> _items = new();

    public int Count => _items.Count;

    public void Enqueue(T item)
    {
        _items.AddLast(item);
    }

    public T Dequeue()
    {
        if (_items.First is null)
        {
            throw new InvalidOperationException("La cola está vacía");
        }

        var value = _items.First.Value;
        _items.RemoveFirst();
        return value;
    }

    public T Peek()
    {
        if (_items.First is null)
        {
            throw new InvalidOperationException("La cola está vacía");
        }

        return _items.First.Value;
    }
}
