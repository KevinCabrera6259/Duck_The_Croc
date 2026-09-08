using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject upgradesMenu;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip loseSound;

    [Header("Timing")]
    [SerializeField] private float gameOverDelay = 1f;
    [SerializeField] private LevelAudioManager levelAudio;

    void Start()
    {
        gameOverPanel.SetActive(false);
        upgradesMenu.SetActive(false);
    }

    public void StartGameOver()
    {
        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        if (audioSource != null && loseSound != null)
            audioSource.PlayOneShot(loseSound);

        if (levelAudio != null)
            levelAudio.StopRiver();

        yield return new WaitForSecondsRealtime(gameOverDelay);

        ShowGameOver();
    }


    private void ShowGameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void TryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu");
    }

    public void OpenUpgrades()
    {
        gameOverPanel.SetActive(false);
        upgradesMenu.SetActive(true);
    }

    public void BackFromUpgrades()
    {
        upgradesMenu.SetActive(false);
        gameOverPanel.SetActive(true);
    }
}
