using System.Collections.Generic;
using UnityEngine;

public class CivilianPool : MonoBehaviour
{
    [SerializeField] private GameObject civilianPrefab;
    [SerializeField] private int poolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    public static CivilianPool Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        FillPool();
    }

    private void FillPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(civilianPrefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetCivilian()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            return obj;
        }

        return Instantiate(civilianPrefab, transform);
    }

    public void ReturnCivilianToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
