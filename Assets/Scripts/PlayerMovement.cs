using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float speed;
    private Vector2 moveDirection;
    public InputActionReference move;

    [Header("Movement Bounds")]
    public BoxCollider2D movementBounds;

    private Vector2 minBounds;
    private Vector2 maxBounds;

    [HideInInspector] public bool canMove = false;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        minBounds = movementBounds.bounds.min;
        maxBounds = movementBounds.bounds.max;
    }

    void Update()
    {
        if (!canMove)
        {
            moveDirection = Vector2.zero;
            return;
        }

        moveDirection = move.action.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            rb2D.linearVelocity = Vector2.zero;
            return;
        }

        rb2D.linearVelocity = moveDirection * speed;

        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 pos = rb2D.position;

        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);

        rb2D.position = pos;
    }
}

