using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private Transform[] spawnPoints;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = enemyPool.GetEnemyFromPool();

        if (enemy != null)
        {
            enemy.transform.position = GetRandomSpawnPosition();
            enemy.transform.rotation = Quaternion.identity;
            enemy.SetActive(true);

            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.Initialize(enemyPool);
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No hay puntos de spawn asignados.");
            return transform.position;
        }

        int index = Random.Range(0, spawnPoints.Length);
        return spawnPoints[index].position;
    }
}