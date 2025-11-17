
using UnityEngine;

public interface IColor 
{
    public Color CurrentColor { get; set; }

    public void ChangeColorToRandom();
}
