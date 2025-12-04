using System.Collections;

namespace ShopSmart.Core.EstructurasDatos;

/// <summary>
/// Lista enlazada simple utilizada para recorridos secuenciales de productos.
/// </summary>
/// <typeparam name="T">Tipo almacenado.</typeparam>
public class SimpleLinkedList<T> : IEnumerable<T>
{
    private class Nodo
    {
        public T Valor { get; set; }
        public Nodo? Siguiente { get; set; }

        public Nodo(T valor)
        {
            Valor = valor;
        }
    }

    private Nodo? _cabeza;

    public void InsertarInicio(T valor)
    {
        var nuevo = new Nodo(valor) { Siguiente = _cabeza };
        _cabeza = nuevo;
    }

    public void InsertarFinal(T valor)
    {
        var nuevo = new Nodo(valor);
        if (_cabeza is null)
        {
            _cabeza = nuevo;
            return;
        }

        var actual = _cabeza;
        while (actual.Siguiente is not null)
        {
            actual = actual.Siguiente;
        }

        actual.Siguiente = nuevo;
    }

    public bool Eliminar(T valor)
    {
        Nodo? actual = _cabeza;
        Nodo? previo = null;
        while (actual is not null)
        {
            if (Equals(actual.Valor, valor))
            {
                if (previo is null)
                {
                    _cabeza = actual.Siguiente;
                }
                else
                {
                    previo.Siguiente = actual.Siguiente;
                }

                return true;
            }

            previo = actual;
            actual = actual.Siguiente;
        }

        return false;
    }

    public T? Buscar(T valor)
    {
        var actual = _cabeza;
        while (actual is not null)
        {
            if (Equals(actual.Valor, valor))
            {
                return actual.Valor;
            }

            actual = actual.Siguiente;
        }

        return default;
    }

    public void ClearAndCopyFrom(SimpleLinkedList<T> otraLista)
    {
        _cabeza = null;
        foreach (var valor in otraLista)
        {
            InsertarFinal(valor);
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var actual = _cabeza;
        while (actual is not null)
        {
            yield return actual.Valor;
            actual = actual.Siguiente;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
