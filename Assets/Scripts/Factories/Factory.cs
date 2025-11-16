using UnityEngine;

public abstract class Factory 
{
    /// <summary>
    /// Создаёт и возвращает объект в выключенном состоянии!
    /// </summary>
    /// <returns></returns>
    public abstract GameObject GetItem();
}
