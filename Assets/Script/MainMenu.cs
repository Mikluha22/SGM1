using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Загружает случайную игровую сцену (кроме самой MainMenu)
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int randomIndex = Random.Range(1, sceneCount);
        SceneManager.LoadScene(randomIndex);
    }
}
