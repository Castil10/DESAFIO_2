namespace ShopSmart.Core.DataStructures;

/// <summary>
/// Árbol binario de búsqueda usado para indexar productos por código.
/// </summary>
public class BinarySearchTree<T> where T : IComparable<T>
{
    private class Node
    {
        public T Value { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }

    private Node? _root;

    public void Insertar(T value)
    {
        _root = InsertarInterno(_root, value);
    }

    public T? Buscar(T value)
    {
        var current = _root;
        while (current is not null)
        {
            int comparison = value.CompareTo(current.Value);
            if (comparison == 0)
            {
                return current.Value;
            }

            current = comparison < 0 ? current.Left : current.Right;
        }

        return default;
    }

    public IEnumerable<T> RecorrerEnOrden()
    {
        return RecorrerInterno(_root);
    }

    private static Node InsertarInterno(Node? node, T value)
    {
        if (node is null)
        {
            return new Node(value);
        }

        int comparison = value.CompareTo(node.Value);
        if (comparison < 0)
        {
            node.Left = InsertarInterno(node.Left, value);
        }
        else if (comparison > 0)
        {
            node.Right = InsertarInterno(node.Right, value);
        }

        return node;
    }

    private static IEnumerable<T> RecorrerInterno(Node? node)
    {
        if (node is null)
        {
            yield break;
        }

        foreach (var left in RecorrerInterno(node.Left))
        {
            yield return left;
        }

        yield return node.Value;

        foreach (var right in RecorrerInterno(node.Right))
        {
            yield return right;
        }
    }
}
