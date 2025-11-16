using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DictionaryExtension 
{
    public static TKey GetRandomKey<TKey, TValue>(this Dictionary<TKey, TValue> dict)
    {
        var keys = dict.Keys.ToList();
        return keys[Random.Range(0, keys.Count)];
    }
}
