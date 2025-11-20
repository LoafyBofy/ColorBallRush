using UnityEngine;

public abstract class Factory<T> where T : MonoBehaviour
{
    /// <summary>
    /// Создаёт и возвращает объект в выключенном состоянии!
    /// </summary>
    /// <returns></returns>
    public abstract T GetItem();
}
