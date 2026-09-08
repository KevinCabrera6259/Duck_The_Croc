using System;
using UnityEngine;
using UnityEngine.UI;
public class CoinManager : MonoBehaviour
{
    // Singleton pattern para acceso global
    public static CoinManager Instance { get; private set; }

    // Evento que se dispara cuando se recolecta una moneda
    public event Action<int> OnCoinCollected;

    void Awake()
    {
        // Configurar singleton
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Descomenta si quieres que persista entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método público para disparar el evento
    public void CollectCoin(int value)
    {
        // Disparar evento si hay suscriptores
        OnCoinCollected?.Invoke(value);

        // Debug
        Debug.Log($"Evento de moneda disparado: +{value}");
    }

    // Método para suscribirse al evento
    public void SubscribeToCoinEvent(Action<int> callback)
    {
        OnCoinCollected += callback;
    }

    // Método para desuscribirse del evento
    public void UnsubscribeFromCoinEvent(Action<int> callback)
    {
        OnCoinCollected -= callback;
    }
}
