using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;

    [Header("Gameplay References")]
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Spawner spawner;

    public LevelAudioManager levelAudio;

    private bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void TogglePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

        player.canMove = false;
        spawner.canSpawn = false;

        if (levelAudio != null)
            levelAudio.PauseRiver();

        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        player.canMove = true;
        spawner.canSpawn = true;

        if (levelAudio != null)
            levelAudio.ResumeRiver();

        isPaused = false;
    }

    //  BOTÓN SETTINGS (EN PAUSE MENU)
    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    //  BOTÓN BACK (EN SETTINGS)
    public void BackFromSettings()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu");
    }
}
