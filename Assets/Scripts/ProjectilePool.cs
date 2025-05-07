using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int poolSize = 20;

    private List<GameObject> pool;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject proj = Instantiate(projectilePrefab, transform);
            proj.SetActive(false);
            pool.Add(proj);
        }
    }

    public GameObject GetProjectileFromPool()
    {
        foreach (GameObject proj in pool)
        {
            if (!proj.activeInHierarchy)
            {
                proj.SetActive(true);
                return proj;
            }
        }

        GameObject newProj = Instantiate(projectilePrefab, transform);
        newProj.SetActive(true);
        pool.Add(newProj);
        return newProj;
    }

    public void ReturnProjectileToPool(GameObject proj)
    {
        proj.SetActive(false);
    }
}
