using UnityEngine;
using System.Collections;
public class TidesMovement : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float speed = 3f;
    public float destroyYPosition = -10f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Desactivar gravedad
    }

    void FixedUpdate()
    {
        // Mover hacia abajo con física
        rb.linearVelocity = Vector2.down * speed;

        // Verificar si está fuera de pantalla
        if (transform.position.y < destroyYPosition)
        {
            Destroy(gameObject);
        }
    }
}
