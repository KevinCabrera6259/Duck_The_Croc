using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartLevel()
    {
        SceneNavigation.PushScene(
            SceneManager.GetActiveScene().name
        );

        SceneManager.LoadScene("Level_Runner");
    }

    public void OpenSettings()
    {
        SceneNavigation.PushScene(
            SceneManager.GetActiveScene().name
        );

        SceneManager.LoadScene("Settings");
    }

    public void OpenUpgrades()
    {
        SceneNavigation.PushScene(
            SceneManager.GetActiveScene().name
        );

        SceneManager.LoadScene("Upgrades");
    }

    public void OpenMenu()
    {
        // Volver al menú principal reinicia la navegación
        SceneNavigation.Clear();
        SceneManager.LoadScene("Main_Menu");
    }

    [Header("Help Menu")]
    public GameObject Help_Menu;

    public void OpenHelp_Menu()
    {
        Help_Menu.SetActive(true);
    }

    public void CloseHelp_Menu()
    {
        Help_Menu.SetActive(false);
    }

    public void OpenExit_Menu()
    {
        SceneNavigation.PushScene(
            SceneManager.GetActiveScene().name
        );

        SceneManager.LoadScene("Exit_Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
