using UnityEngine;
using UnityEngine.UI;

public class CrosshairFollowRaycast : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private Image crosshairImage;
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private LayerMask targetLayers;

    void Update()
    {
        Ray ray = new Ray(shootOrigin.position, shootOrigin.forward);
        RaycastHit hit;

        Vector3 targetPoint;

        
        if (Physics.Raycast(ray, out hit, maxDistance, targetLayers))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(maxDistance);
        }

        Vector3 screenPos = cam.WorldToScreenPoint(targetPoint);
        crosshairImage.transform.position = screenPos;
    }
}