using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotatingPool<T>
{
    List<T> elements;
    List<T> nextElements;
    int index;
    
    public RandomRotatingPool(List<T> e)
    {
        elements = new List<T>(e);
        nextElements = new List<T>(e);
        shuffle();
        shuffle();
    }

    public T GetNext()
    {
        T elem = elements[index];
        index++;
        if (index == elements.Count)
            shuffle();

        return elem;
    }

    public List<T> Peek(int n)
    {
        if (n <= 0) return new List<T>();
        List<T> ret;

        if (n <= elements.Count - index)
        {
            ret = elements.GetRange(index, n);
        }
        else if(n <= elements.Count - index + nextElements.Count)
        {
            ret = elements.GetRange(index, elements.Count - index);
            ret.AddRange(nextElements.GetRange(0, n - elements.Count + index));
        }
        else
        {
            ret = elements.GetRange(index, elements.Count - index);
            ret.AddRange(new List<T>(nextElements));
        }
            

        return ret;
    }

    public T Peek()
    {
        return elements[index];
    }

    private void shuffle()
    {
        List<T> tmp = elements;
        elements = nextElements;
        nextElements = new List<T>();
        int n = tmp.Count;
        while (n > 1)
        {
            int k = Random.Range(0, n);
            nextElements.Add(tmp[k]);
            tmp.RemoveAt(k);
            n--;
        }
        nextElements.Add(tmp[0]);

        index = 0;
    }
}
