using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LivesUI : MonoBehaviour
{
    [Header("Hearts")]
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    [Header("Animation")]
    [SerializeField] private float popScale = 1.3f;
    [SerializeField] private float popDuration = 0.15f;
    [SerializeField] private float blinkDuration = 0.6f;
    [SerializeField] private float blinkInterval = 0.1f;

    private int lastLives;
    private Vector3[] originalScales;

    void Awake()
    {
        // Guardar escala original
        originalScales = new Vector3[hearts.Length];
        for (int i = 0; i < hearts.Length; i++)
        {
            originalScales[i] = hearts[i].transform.localScale;
        }
    }

    public void UpdateHearts(int currentLives)
    {
        // Evita animaciones al iniciar
        if (currentLives < lastLives)
        {
            int lostIndex = currentLives;
            StartCoroutine(PopHeart(lostIndex));
            StartCoroutine(BlinkLastHeart(currentLives - 1));
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < currentLives ? fullHeart : emptyHeart;
            hearts[i].transform.localScale = originalScales[i];
            hearts[i].enabled = true;
        }

        lastLives = currentLives;
    }

    IEnumerator PopHeart(int index)
    {
        if (index < 0 || index >= hearts.Length) yield break;

        Transform heart = hearts[index].transform;

        heart.localScale = originalScales[index] * popScale;
        yield return new WaitForSeconds(popDuration);
        heart.localScale = originalScales[index];
    }

    IEnumerator BlinkLastHeart(int index)
    {
        if (index < 0 || index >= hearts.Length) yield break;

        Image heart = hearts[index];
        float elapsed = 0f;

        while (elapsed < blinkDuration)
        {
            heart.enabled = !heart.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        heart.enabled = true;
    }
}
