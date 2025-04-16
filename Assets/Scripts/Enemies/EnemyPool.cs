using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize = 10;

    private List<GameObject> pool;

    void Awake()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            pool.Add(enemy);
        }
    }

    public GameObject GetEnemyFromPool()
    {
        foreach (GameObject enemy in pool)
        {
            if (enemy != null && !enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }

        
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.SetActive(true);
        pool.Add(newEnemy);
        return newEnemy;
    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);
    }
}