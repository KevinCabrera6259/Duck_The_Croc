using UnityEngine;
using UnityEngine.Audio;

public class Coin : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;
    public float destroyYPosition = -10f;

    [Header("Coin Effects")]
    public int coinValue = 1;
    public float rotationSpeed = 100f;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 1f;
    public AudioClip collectSound;

    private Rigidbody2D rb;
    private Vector3 originalPos;
    private bool isCollected = false;
    private float floatTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 0;

        originalPos = transform.position;
    }

    void FixedUpdate()
    {
        if (isCollected) return;

        floatTimer += Time.fixedDeltaTime;

        // Movimiento base hacia abajo
        Vector2 velocity = Vector2.down * speed;

        // Agregar flotación a la velocidad
        float floatVelocity = Mathf.Cos(floatTimer * floatFrequency) * floatFrequency * floatAmplitude;
        velocity.y += floatVelocity;

        // Aplicar velocidad
        rb.linearVelocity = velocity;

        // Actualizar posición original para seguir bajando
        originalPos += Vector3.down * (speed * Time.fixedDeltaTime);

        // Verificar destrucción
        if (transform.position.y < destroyYPosition)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isCollected) return;

        // Solo rotación
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected || !other.CompareTag("Player")) return;

        isCollected = true;
        rb.linearVelocity = Vector2.zero;

        if (CoinManager.Instance != null)
            CoinManager.Instance.CollectCoin(coinValue);

        if (collectSound != null)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        Destroy(gameObject);
    }
}