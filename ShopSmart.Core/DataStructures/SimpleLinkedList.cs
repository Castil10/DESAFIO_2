namespace ShopSmart.Core.DataStructures;

/// <summary>
/// Lista enlazada simple usada para recorridos secuenciales del inventario.
/// </summary>
public class SimpleLinkedList<T>
{
    private class Node
    {
        public T Value { get; set; }
        public Node? Next { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }

    private Node? _head;

    public void InsertarInicio(T value)
    {
        var node = new Node(value) { Next = _head };
        _head = node;
    }

    public void InsertarFinal(T value)
    {
        var node = new Node(value);
        if (_head is null)
        {
            _head = node;
            return;
        }

        var current = _head;
        while (current.Next is not null)
        {
            current = current.Next;
        }

        current.Next = node;
    }

    public bool Eliminar(Func<T, bool> predicate)
    {
        Node? current = _head;
        Node? previous = null;

        while (current is not null)
        {
            if (predicate(current.Value))
            {
                if (previous is null)
                {
                    _head = current.Next;
                }
                else
                {
                    previous.Next = current.Next;
                }

                return true;
            }

            previous = current;
            current = current.Next;
        }

        return false;
    }

    public T? Buscar(Func<T, bool> predicate)
    {
        var current = _head;
        while (current is not null)
        {
            if (predicate(current.Value))
            {
                return current.Value;
            }

            current = current.Next;
        }

        return default;
    }

    public IEnumerable<T> Recorrer()
    {
        var current = _head;
        while (current is not null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }
}
