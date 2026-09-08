using UnityEngine;

public class PrefabMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] public float _speed = 3f;
    [SerializeField] public float _destroyYPosition = -10f;

    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        // Gravity off
        _rb.gravityScale = 0;
    }

    void FixedUpdate()
    {
        // Downwards movement
        _rb.linearVelocity = Vector2.down * _speed;

        // Check if GO is out of the screen
        if (transform.position.y < _destroyYPosition)
        {
            Destroy(gameObject);
        }
    }
}
