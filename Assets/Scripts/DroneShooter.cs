using UnityEngine;

public class DroneShooter : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRange = 100f;
    [SerializeField] private float damageAmount = 20f;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private float sphereRadius = 0.5f;
    [SerializeField] private LineRenderer laserLine;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, sphereRadius, out hit, fireRange, hitLayers))
        {
            Debug.Log("Disparo impactó en: " + hit.collider.name);

            
            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damageAmount);
            }

            
            if (laserLine != null)
            {
                StartCoroutine(ShowLaser(hit.point));
            }
        }
        else
        {
            if (laserLine != null)
            {
                StartCoroutine(ShowLaser(firePoint.position + firePoint.forward * fireRange));
            }
        }
    }

    System.Collections.IEnumerator ShowLaser(Vector3 hitPoint)
    {
        laserLine.SetPosition(0, firePoint.position);
        laserLine.SetPosition(1, hitPoint);
        laserLine.enabled = true;
        yield return new WaitForSeconds(0.05f);
        laserLine.enabled = false;
    }
}