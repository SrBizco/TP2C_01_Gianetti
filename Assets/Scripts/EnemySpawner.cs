using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float spawnRadius = 10f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyToSpawn = enemyPrefabs[randomIndex];

        Vector3 spawnPosition = transform.position + Random.onUnitSphere * spawnRadius;
        spawnPosition.y = Mathf.Abs(spawnPosition.y);

        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
    }
}