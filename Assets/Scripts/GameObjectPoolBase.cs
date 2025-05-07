using UnityEngine;

public abstract class GameObjectPoolBase : MonoBehaviour
{
    public abstract GameObject GetFromPool();
    public abstract void ReturnToPool(GameObject obj);
}