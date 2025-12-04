using System.Collections;

namespace ShopSmart.Core.EstructurasDatos;

/// <summary>
/// Árbol binario de búsqueda para indexar productos por código.
/// </summary>
public class BST<T> : IEnumerable<T> where T : IComparable<T>
{
    private class Nodo
    {
        public T Valor { get; set; }
        public Nodo? Izquierda { get; set; }
        public Nodo? Derecha { get; set; }

        public Nodo(T valor)
        {
            Valor = valor;
        }
    }

    private Nodo? _raiz;

    public void Insertar(T valor)
    {
        _raiz = InsertarInterno(_raiz, valor);
    }

    private Nodo InsertarInterno(Nodo? nodo, T valor)
    {
        if (nodo is null)
        {
            return new Nodo(valor);
        }

        int comparacion = valor.CompareTo(nodo.Valor);
        if (comparacion < 0)
        {
            nodo.Izquierda = InsertarInterno(nodo.Izquierda, valor);
        }
        else if (comparacion > 0)
        {
            nodo.Derecha = InsertarInterno(nodo.Derecha, valor);
        }
        return nodo;
    }

    public bool Contiene(T valor)
    {
        var actual = _raiz;
        while (actual is not null)
        {
            int comparacion = valor.CompareTo(actual.Valor);
            if (comparacion == 0)
            {
                return true;
            }

            actual = comparacion < 0 ? actual.Izquierda : actual.Derecha;
        }

        return false;
    }

    public IEnumerable<T> RecorrerEnOrden()
    {
        foreach (var valor in RecorrerEnOrdenInterno(_raiz))
        {
            yield return valor;
        }
    }

    private IEnumerable<T> RecorrerEnOrdenInterno(Nodo? nodo)
    {
        if (nodo is null)
        {
            yield break;
        }

        foreach (var izquierda in RecorrerEnOrdenInterno(nodo.Izquierda))
        {
            yield return izquierda;
        }

        yield return nodo.Valor;

        foreach (var derecha in RecorrerEnOrdenInterno(nodo.Derecha))
        {
            yield return derecha;
        }
    }

    public void Reconstruir(IEnumerable<T> elementos)
    {
        _raiz = null;
        foreach (var elemento in elementos)
        {
            Insertar(elemento);
        }
    }

    public IEnumerator<T> GetEnumerator() => RecorrerEnOrden().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
