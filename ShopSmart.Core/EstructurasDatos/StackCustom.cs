namespace ShopSmart.Core.EstructurasDatos;

/// <summary>
/// Pila utilizada para manejar acciones del sistema (deshacer).
/// </summary>
public class StackCustom<T>
{
    private readonly List<T> _items = new();

    public int Count => _items.Count;

    public void Push(T item)
    {
        _items.Add(item);
    }

    public T Pop()
    {
        if (_items.Count == 0)
        {
            throw new InvalidOperationException("La pila está vacía");
        }

        int lastIndex = _items.Count - 1;
        T value = _items[lastIndex];
        _items.RemoveAt(lastIndex);
        return value;
    }

    public T Peek()
    {
        if (_items.Count == 0)
        {
            throw new InvalidOperationException("La pila está vacía");
        }

        return _items[^1];
    }
}
