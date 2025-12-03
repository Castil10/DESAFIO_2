namespace ShopSmart.Core.DataStructures;

/// <summary>
/// Pila genérica utilizada para registrar acciones y permitir deshacer.
/// </summary>
public class StackCustom<T>
{
    private readonly List<T> _items = new();

    public int Count => _items.Count;

    public void Push(T value)
    {
        _items.Add(value);
    }

    public T Pop()
    {
        if (_items.Count == 0)
        {
            throw new InvalidOperationException("La pila está vacía.");
        }

        var value = _items[^1];
        _items.RemoveAt(_items.Count - 1);
        return value;
    }

    public T Peek()
    {
        if (_items.Count == 0)
        {
            throw new InvalidOperationException("La pila está vacía.");
        }

        return _items[^1];
    }
}
