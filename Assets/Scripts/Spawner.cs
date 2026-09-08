using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private float _spawnRate = 2f;

    [Header("Spawn Area")]
    [SerializeField] private BoxCollider2D spawnArea;

    [Header("GO Movement")]
    [SerializeField] private float _speedMovement = 3f;

    private Camera _mainCamera;
    private float _destroyBelowY;

    [HideInInspector] public bool canSpawn = false;

    void Start()
    {
        _mainCamera = Camera.main;

        Vector3 bottomLeft = _mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        _destroyBelowY = bottomLeft.y - 2f;

        StartCoroutine(SpawnPrefabs());
    }

    IEnumerator SpawnPrefabs()
    {
        while (true)
        {
            if (canSpawn)
            {
                SpawnPrefabGO();
            }
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    void SpawnPrefabGO()
    {
        if (_prefabs.Length == 0) return;

        GameObject selectedPrefab = _prefabs[Random.Range(0, _prefabs.Length)];

        // 🔥 AQUÍ VA LA MODIFICACIÓN
        Vector2 min = spawnArea.bounds.min;
        Vector2 max = spawnArea.bounds.max;

        float spawnX = Random.Range(min.x, max.x);
        float spawnY = max.y; // aparecen desde arriba del área

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        GameObject newGO = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
        SetupGameObject(newGO);
    }

    void SetupGameObject(GameObject prefab)
    {
        PrefabMovement movement = prefab.GetComponent<PrefabMovement>();
        if (movement == null)
            movement = prefab.AddComponent<PrefabMovement>();

        movement._speed = _speedMovement;
        movement._destroyYPosition = _destroyBelowY;
    }
}

