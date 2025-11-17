using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ColorsConfig", menuName = "Scriptable Objects/Colors")]
public class ColorsConfig : ScriptableObject
{
    [SerializeField] private List<Color> _colors = new();

    public Color GetRandomColor() => _colors.GetRandom();
}
