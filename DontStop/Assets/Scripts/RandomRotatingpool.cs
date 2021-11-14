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
