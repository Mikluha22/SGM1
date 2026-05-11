using UnityEngine;
using UnityEngine.UI;
using TMPro; // если TextMeshPro, иначе using UnityEngine.UI
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;

    [Header("Level Settings")]
    public float timeLimit = 60f;

    [Header("UI References")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI enemiesCountText;
    public GameObject winPanel;
    public GameObject losePanel;

    private float currentTime;
    private int enemiesAlive;
    private bool gameEnded;

    void Start()
    {
        currentTime = timeLimit;
        winPanel.SetActive(false);
        losePanel.SetActive(false);

        // Современный способ подсчёта врагов (без FindObjectsSortMode)
        enemiesAlive = FindObjectsByType<EnemyController>().Length;
        UpdateEnemiesUI();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (gameEnded) return;

        // Таймер
        currentTime -= Time.deltaTime;
        if (currentTime <= 0f)
        {
            currentTime = 0f;
            Lose("Время вышло!");
        }
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Проверка: если игрок умер, поражение (вызывается из BaseCharacter.Die)
    }

    public void EnemyKilled()
    {
        if (gameEnded) return;
        enemiesAlive--;
        UpdateEnemiesUI();
        if (enemiesAlive <= 0)
            Win();
    }

    public void PlayerDied()
    {
        if (gameEnded) return;
        Lose("Вас убили!");
    }

    void Win()
    {
        gameEnded = true;
        winPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GetComponent<AudioSource>().PlayOneShot(winSound); // или loseSound
    }

    void Lose(string reason)
    {
        gameEnded = true;
        losePanel.SetActive(true);
        // Можно установить текст причины в дочернем Text
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
        GetComponent<AudioSource>().PlayOneShot(loseSound); // или loseSound
    }

    void UpdateEnemiesUI()
    {
        if (enemiesCountText != null)
            enemiesCountText.text = "Врагов осталось: " + enemiesAlive;
    }

    // Методы для кнопок
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextIndex);
        else
            SceneManager.LoadScene("MainMenu"); // или на главное меню
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}