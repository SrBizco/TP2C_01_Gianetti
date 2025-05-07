using UnityEngine;
using UnityEngine.SceneManagement;

public class DroneHealth : Entity
{
    [SerializeField] private float minImpactVelocity = 5f;
    [SerializeField] private LayerMask ignoredImpactLayers;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & ignoredImpactLayers) != 0)
            return;

        float impactForce = collision.relativeVelocity.magnitude;

        if (impactForce >= minImpactVelocity)
        {
            float damage = impactForce * 1f;
            TakeDamage(damage);
        }
    }

    public override void Die()
    {
        Debug.Log("El dron ha sido destruido.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
