using UnityEngine;
using TMPro;
using System.Collections;

public class CountdownController : MonoBehaviour
{
    [SerializeField] public Spawner _spawnManager;
    [SerializeField] public PlayerMovement _player;
    [SerializeField] private GameObject _tutorial;
    public TextMeshProUGUI countdownText;  // Asigna el texto en el Inspector
    [Header("UI")]
    [SerializeField] private GameObject Pause_BT;

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        countdownText.text = "READY?...5";
        yield return new WaitForSeconds(1f);

        countdownText.text = "READY?...4";
        yield return new WaitForSeconds(1f);

        countdownText.text = "READY?...3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "READY?...2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "READY?...1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "     GO!   ";
        yield return new WaitForSeconds(1f);

        // Ocultar después
        _tutorial.gameObject.SetActive(false);

        //Activates player movement and spawn manager
        _player.canMove = true;
        _spawnManager.canSpawn = true;

        //Activates Pause_BT
        if (Pause_BT != null)
            Pause_BT.SetActive(true);
    }
}
