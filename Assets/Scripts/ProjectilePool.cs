using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int poolSize = 20;

    private List<GameObject> pool;

    void Awake()
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        int projectileLayer = LayerMask.NameToLayer("Projectile");
        Physics.IgnoreLayerCollision(playerLayer, projectileLayer, true);
        
        pool = new List<GameObject>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            var p = Instantiate(projectilePrefab);
            p.SetActive(false);
            pool.Add(p);
        }
    }

    public GameObject GetProjectileFromPool()
    {
        foreach (var p in pool)
        {
            if (!p.activeInHierarchy)
            {
                p.SetActive(true);
                return p;
            }
        }
        
        var newP = Instantiate(projectilePrefab);
        newP.SetActive(true);
        pool.Add(newP);
        return newP;
    }

    public void ReturnProjectileToPool(GameObject projectile)
    {
        projectile.SetActive(false);
    }
}
