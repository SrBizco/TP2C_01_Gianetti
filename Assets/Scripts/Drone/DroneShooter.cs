using UnityEngine;
using System.Collections;

public class DroneShooter : MonoBehaviour
{
    [Header("Punto de disparo")]
    [SerializeField] private Transform firePoint;

    [Header("L�ser")]
    [SerializeField] private LineRenderer laserLine;
    [SerializeField] private float fireRange = 100f;
    [SerializeField] private float laserSphereRadius = 0.5f;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private float laserDamage = 20f;

    [Header("Proyectiles")]
    [SerializeField] private ProjectilePool projectilePool;
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private float projectileDamage = 20f;

    private bool laserActive = true;

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsPaused()) return;

        if (Input.GetKeyDown(KeyCode.L))
            laserActive = !laserActive;

        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    private void Shoot()
    {
        if (laserActive)
            ShootLaser();
        else
            ShootProjectile();
    }

    private void ShootLaser()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        Vector3 end = firePoint.position + firePoint.forward * fireRange;

        RaycastHit[] hits = Physics.SphereCastAll(ray, laserSphereRadius, fireRange, hitLayers);

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Projectile"))
                continue;

            end = hit.point;

            IDamageable target = hit.collider.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(laserDamage);
                break;
            }
        }

        if (laserLine != null)
            StartCoroutine(ShowLaser(end));
    }

    private IEnumerator ShowLaser(Vector3 hitPoint)
    {
        laserLine.SetPosition(0, firePoint.position);
        laserLine.SetPosition(1, hitPoint);
        laserLine.enabled = true;
        yield return new WaitForSeconds(0.05f);
        laserLine.enabled = false;
    }

    private void ShootProjectile()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;
        Vector3 target = firePoint.position + firePoint.forward * fireRange;

        if (Physics.Raycast(ray, out hit, fireRange, hitLayers))
            target = hit.point;

        GameObject projGO = projectilePool.GetProjectileFromPool();
        projGO.transform.position = firePoint.position;
        projGO.transform.rotation = Quaternion.identity;

        var proj = projGO.GetComponent<Projectile>();
        if (proj != null)
            proj.Launch(projectilePool, target, projectileSpeed, projectileDamage);
    }
}
