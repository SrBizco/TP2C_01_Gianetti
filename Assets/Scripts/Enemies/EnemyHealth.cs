using UnityEngine;

public class EnemyHealth : Entity
{
    private EnemyPool pool;
    private GameManager gameManager;

    public void Initialize(EnemyPool objectPool)
    {
        pool = objectPool;
    }

    void OnEnable()
    {
        currentHealth = maxHealth;

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }

    public override void Die()
    {
        if (gameManager != null)
        {
            gameManager.IncreaseScore();
        }

        if (pool != null)
        {
            pool.ReturnEnemyToPool(gameObject);
        }
        else
        {
            Debug.LogWarning("EnemyPool no asignado al enemigo.");
            gameObject.SetActive(false);
        }
    }
}
