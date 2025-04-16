using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    [Header("Capas")]
    [Tooltip("Capa(s) que el proyectil debe dañar (por ejemplo, Enemy)")]
    [SerializeField] private LayerMask damageLayers;
    [Tooltip("Capa(s) que el proyectil debe IGNORAR (por ejemplo, Player)")]
    [SerializeField] private LayerMask ignoreLayers;

    private Vector3 targetPosition;
    private float speed;
    private ProjectilePool pool;
    private bool isLaunched;
    private float projectileDamage;

    public void Launch(ProjectilePool pool, Vector3 target, float speed, float damage)
    {
        this.pool = pool;
        this.targetPosition = target;
        this.speed = speed;
        this.projectileDamage = damage;
        isLaunched = true;
    }

    void Update()
    {
        if (!isLaunched) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            isLaunched = false;
            pool.ReturnProjectileToPool(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        int layer = other.gameObject.layer;

        
        if (((1 << layer) & ignoreLayers) != 0)
            return;

        
        if (((1 << layer) & damageLayers) != 0)
        {       
            var enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(projectileDamage);
            }
            
            var civilian = other.GetComponent<CivilianFSM>();
            if (civilian != null)
            {
                civilian.Die(); 
            }
        }

        isLaunched = false;
        pool.ReturnProjectileToPool(gameObject);
    }
}