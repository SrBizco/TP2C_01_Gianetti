using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
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

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
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