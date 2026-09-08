// CoinUIManager.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Si usas TextMeshPro

public class CoinUIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] public TextMeshProUGUI coinsTextTMP;

    [Header("Coin Settings")]
    private int currentCoins = 0;
    public int startingCoins = 0;

    [Header("Animation")]
    [SerializeField] public string collectionTrigger = "Collect";

    void Start()
    {
        // Inicializar contador
        currentCoins = startingCoins;
        UpdateUI();

        // Suscribirse al evento de monedas
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.OnCoinCollected += HandleCoinCollected;
        }
        else
        {
            Debug.LogError("CoinManager no encontrado en la escena!");
        }
    }

    void OnDestroy()
    {
        // Importante: Desuscribirse para evitar memory leaks
        if (CoinManager.Instance != null)
        {
            CoinManager.Instance.OnCoinCollected -= HandleCoinCollected;
        }
    }

    // Método que se ejecuta cuando se recolecta una moneda
    void HandleCoinCollected(int coinValue)
    {
        // Sumar monedas
        currentCoins += coinValue;

        // Actualizar UI
        UpdateUI();

        // Efecto de sonido adicional (opcional)
        PlayCoinSound();

        Debug.Log($"Moneda recibida! Total: {currentCoins}");
    }

    void UpdateUI()
    {
        if (coinsTextTMP != null)
        {
            coinsTextTMP.text = $"{currentCoins}";
        }
    }

    void PlayCoinSound()
    {
        // Aquí puedes agregar un sonido de UI
        // AudioManager.Instance.PlaySound("coinUI");
    }

    // Método público para obtener las monedas actuales
    public int GetCurrentCoins()
    {
        return currentCoins;
    }

    // Método público para gastar monedas
    public bool SpendCoins(int amount)
    {
        if (currentCoins >= amount)
        {
            currentCoins -= amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    // Método para resetear monedas
    public void ResetCoins()
    {
        currentCoins = startingCoins;
        UpdateUI();
    }
}