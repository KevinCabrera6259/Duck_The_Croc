using UnityEngine;
using System.Collections;

public class SpawnTides : MonoBehaviour
{
    [Header("Configuración de Spawn")]
    public GameObject[] tidesPrefabs; // Array de prefabs de obstáculos
    public float spawnRate = 2f; // Tiempo entre spawns
    public float spawnYPosition = 5f; // Posición Y fija donde spawnearán

    [Header("Límites y Margenes")]
    public float horizontalLimit = 8f;
    public float spawnMargin = 1f; // Margen desde los bordes

    [Header("Movimiento de Obstáculos")]
    public float obstacleSpeed = 3f; // Velocidad hacia abajo

    private Camera mainCamera;
    private float destroyBelowY; // Posición Y donde se destruyen los obstáculos

    void Start()
    {
        mainCamera = Camera.main;

        // Calcular posición Y donde destruir obstáculos (fuera de cámara por abajo)
        Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        destroyBelowY = bottomLeft.y - 2f; // Margen adicional de 2 unidades

        // Iniciar corrutina de spawn
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void SpawnObstacle()
    {
        if (tidesPrefabs.Length == 0)
        {
            Debug.LogWarning("No hay prefabs de obstáculos asignados!");
            return;
        }

        // Seleccionar prefab aleatorio
        GameObject selectedPrefab = tidesPrefabs[Random.Range(0, tidesPrefabs.Length)];

        // Calcular posición X aleatoria dentro de los límites
        float spawnX = Random.Range(-horizontalLimit + spawnMargin, horizontalLimit - spawnMargin);
        Vector3 spawnPosition = new Vector3(spawnX, spawnYPosition, 0);

        // Instanciar obstáculo
        GameObject newObstacle = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

        // Configurar movimiento y destrucción automática
        SetupObstacle(newObstacle);
    }

    void SetupObstacle(GameObject obstacle)
    {
        // Agregar componente de movimiento si no existe
        TidesMovement movement = obstacle.GetComponent<TidesMovement>();
        if (movement == null)
        {
            movement = obstacle.AddComponent<TidesMovement>();
        }

        // Configurar valores
        movement.speed = obstacleSpeed;
        movement.destroyYPosition = destroyBelowY;
    }
}

