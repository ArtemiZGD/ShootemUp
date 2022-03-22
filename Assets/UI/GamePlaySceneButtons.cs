using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlaySceneButtons : MonoBehaviour
{
    [SerializeField] private AudioSource soundsSource;
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject quitMenu;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject quitGameOverMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private ShootingMovement playerScript;

    private bool gameOver;

    private void Start()
    {
        gameOver = false;
        SaveLoad.LoadGame();

        SettingsScript.pause = false;

        soundsSource.volume = SettingsScript.sounds;
        musicSource.volume = SettingsScript.music;

        mainUI.SetActive(true);
        pauseMenu.SetActive(false);
        quitMenu.SetActive(false);
    }

    private void Update()
    {
        if (!gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !SettingsScript.pause)
            {
                Pause();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && SettingsScript.pause)
            {
                UnPause();
            }
        }
    }

    public void Pause()
    {
        soundsSource.Play();
        mainUI.SetActive(false);
        pauseMenu.SetActive(true);
        quitMenu.SetActive(false);
        StopTime();
    }

    public void UnPause()
    {
        soundsSource.Play();
        mainUI.SetActive(true);
        pauseMenu.SetActive(false);
        quitMenu.SetActive(false);
        StartTime();
    }

    public void Quit()
    {
        mainUI.SetActive(false);
        pauseMenu.SetActive(false);
        quitMenu.SetActive(true);
        gameOverMenu.SetActive(false);
    }

    public void QuitGameOver()
    {
        mainUI.SetActive(false);
        pauseMenu.SetActive(false);
        quitGameOverMenu.SetActive(true);
        gameOverMenu.SetActive(false);
    }

    public void GameOver()
    {
        gameOver = true;
        StopTime();
        mainUI.SetActive(false);
        pauseMenu.SetActive(false);
        quitMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        quitGameOverMenu.SetActive(false);
    }

    public void QuitGame()
    {
        SaveLoad.SaveGame(SettingsScript.maxScore);
        Application.Quit();
    }

    public void ClickButton()
    {
        soundsSource.Play();
    }

    public void Menu()
    {
        SaveLoad.SaveGame(SettingsScript.maxScore);
        StartTime();
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Repeat()
    {
        gameOver = false;
        SaveLoad.SaveGame(SettingsScript.maxScore);
        StartTime();
        SceneManager.LoadScene("GameplayScene");
    }

    private void StopTime()
    {
        Time.timeScale = 0;
        SettingsScript.pause = true;
    }

    private void StartTime()
    {
        Time.timeScale = 1;
        SettingsScript.pause = false;
    }
}
