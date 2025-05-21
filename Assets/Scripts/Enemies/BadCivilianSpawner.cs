using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BadCivilianSpawner : MonoBehaviour
{
    [SerializeField] private BadCivilianPool pool;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private int civiliansToSpawn = 10;

    void Start()
    {
        for (int i = 0; i < civiliansToSpawn; i++)
        {
            Transform point = spawnPoints[i % spawnPoints.Count];
            SpawnAtPoint(point);
        }
    }

    private void SpawnAtPoint(Transform point)
    {
        GameObject civilian = pool.GetCivilian();

        NavMeshAgent agent = civilian.GetComponent<NavMeshAgent>();
        if (agent != null) agent.enabled = false;

        Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f));
        Vector3 finalPos = point.position + offset;

        if (agent != null) agent.Warp(finalPos);
        else civilian.transform.position = finalPos;

        civilian.transform.rotation = point.rotation;

        if (agent != null) agent.enabled = true;
        civilian.SetActive(true);

        // ✅ Registrar en el GameManager
        if (civilian.TryGetComponent<CivilianFSM>(out var fsm) && fsm.civilianType == CivilianType.Bad)
        {
            GameManager.Instance?.RegisterBadCivilian();
        }
    }
}
