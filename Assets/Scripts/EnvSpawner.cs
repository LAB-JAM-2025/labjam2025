using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject treePrefab;
    public GameObject buildingPrefab;
    public GameObject volcanoPrefab;

    [Header("Settings")]
    public int amount = 30;
    public float radius = 10f;
    public float ascendHeight = 2f;
    public float ascendTime = 0.5f;
    public float objectScale = 0.02f;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    void Start()
    {
        SpawnObjects();
    }

    [ContextMenu("Spawn Objects")]
    public void SpawnObjects()
    {
        foreach (var obj in spawnedObjects)
            if (obj != null) Destroy(obj);

        spawnedObjects.Clear();

        for (int i = 0; i < amount; i++)
        {
            // Fibonacci sphere distribution
            float angle = i * Mathf.PI * (3 - Mathf.Sqrt(5));
            float y = 1f - (i / (float)(amount - 1)) * 2f;
            float r = Mathf.Sqrt(1f - y * y);
            float x = Mathf.Cos(angle) * r;
            float z = Mathf.Sin(angle) * r;

            Vector3 localPos = new Vector3(x, y, z) * radius;
            Vector3 worldPos = transform.position + localPos;

            // Rotate so UP points away from center
            Vector3 normal = (worldPos - transform.position).normalized;
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, normal);

            GameObject obj = Instantiate(treePrefab, worldPos, rot, transform);
            obj.transform.localScale = Vector3.one * objectScale;

            spawnedObjects.Add(obj);
        }
    }

    
    public void SwapPrefabs(GameObject nextPrefab)
    {
        if (spawnedObjects.Count > 0)
            StartCoroutine(SwapCoroutine(nextPrefab));
    }

    [ContextMenu("Swap To Trees")]

    void SwapToTrees() => SwapPrefabs(treePrefab);

    [ContextMenu("Swap To Buildings")]
    void SwapToBuildings() => SwapPrefabs(buildingPrefab);

    [ContextMenu("Swap To Volcanoes")]
    void SwapToVolcanoes() => SwapPrefabs(volcanoPrefab);


    private IEnumerator SwapCoroutine(GameObject nextPrefab)
{
    for (int i = 0; i < spawnedObjects.Count; i++)
    {
        GameObject oldObj = spawnedObjects[i];

        Vector3 pos = oldObj.transform.position;
        Quaternion rot = oldObj.transform.rotation;

        // Hide immediately
        oldObj.SetActive(false);
        Destroy(oldObj);

        GameObject newObj = Instantiate(nextPrefab, pos, rot, transform);
        newObj.transform.localScale = Vector3.one * objectScale;

        spawnedObjects[i] = newObj;
    }

    yield break;
}
    
    void OnEnable() 
    {
         KillCounter.OnSwitchWave += SwitchPrefabs;

    }

    void OnDisable()
    {
         KillCounter.OnSwitchWave -= SwitchPrefabs;
    }

    void SwitchPrefabs(int waveNumber)
    {
        switch (waveNumber)
        {
            case 2:
                SwapPrefabs(buildingPrefab);
                break;
            case 3:
                SwapPrefabs(volcanoPrefab);
                break;
            default:
                Debug.Log("Unknown wave number!");
                break;
        }
    }
}
