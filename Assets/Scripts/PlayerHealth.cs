using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxLives = 3;
    private int currentLives;

    [Header("Invincibility")]
    [SerializeField] private float invincibilityTime = 1.5f;
    private bool canTakeDamage = true;

    [Header("UI")]
    [SerializeField] private LivesUI livesUI;

    [Header("Visual Feedback")]
    [SerializeField] private float blinkInterval = 0.15f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameOverManager gameOverManager;

    void Start()
    {
        currentLives = maxLives;
        livesUI.UpdateHearts(currentLives);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit by: " + other.name);

        if (!canTakeDamage) return;

        if (other.CompareTag("Obstacle"))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        canTakeDamage = false;

        // Sonido de golpe
        if (audioSource != null && hitSound != null)
            audioSource.PlayOneShot(hitSound);

        currentLives--;
        livesUI.UpdateHearts(currentLives);

        if (currentLives <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    IEnumerator InvincibilityCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < invincibilityTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        spriteRenderer.enabled = true;
        canTakeDamage = true;
    }

    void Die()
    {
        Debug.Log("Player muerto");
        gameOverManager.StartGameOver();
        gameObject.SetActive(false);
    }
}
