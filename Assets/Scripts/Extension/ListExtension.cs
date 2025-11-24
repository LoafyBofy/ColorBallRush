using System.Collections.Generic;
using UnityEngine;

public static class ListExtension
{
    public static T GetRandom<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static T GetRandomAndRemove<T>(this List<T> list)
    {
        int index = Random.Range(0, list.Count);
        var item = list[index];
        list.RemoveAt(index);
        return item;
    }
}
