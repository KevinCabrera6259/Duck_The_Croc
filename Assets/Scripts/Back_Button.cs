using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void Back()
    {
        string previousScene = SceneNavigation.PopScene();

        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            SceneManager.LoadScene("Main_Menu");
        }
    }
}
