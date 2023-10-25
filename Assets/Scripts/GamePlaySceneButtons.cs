using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlaySceneButtons : MonoBehaviour
{
	[Header("Audio Sources")]
	[SerializeField] private AudioSource _soundsSource;
	[SerializeField] private AudioSource _musicSource;

	[Header("Menus")]
	[SerializeField] private GameObject _pauseMenu;
	[SerializeField] private GameObject _quitMenu;
	[SerializeField] private GameObject _mainUI;
	[SerializeField] private GameObject _quitGameOverMenu;
	[SerializeField] private GameObject _gameOverMenu;

	private bool _isGameOver;

	private void Start()
	{
		_isGameOver = false;
		SaveLoad.LoadGame();

		SettingsScript.Pause = false;

		_soundsSource.volume = SettingsScript.Sounds;
		_musicSource.volume = SettingsScript.Music;

		_mainUI.SetActive(true);
		_pauseMenu.SetActive(false);
		_quitMenu.SetActive(false);
	}

	private void Update()
	{
		if (!_isGameOver)
		{
			if (Input.GetKeyDown(KeyCode.Escape) && !SettingsScript.Pause)
			{
				Pause();
			}
			else if (Input.GetKeyDown(KeyCode.Escape) && SettingsScript.Pause)
			{
				UnPause();
			}
		}
	}

	public void Pause()
	{
		_soundsSource.Play();
		_mainUI.SetActive(false);
		_pauseMenu.SetActive(true);
		_quitMenu.SetActive(false);
		StopTime();
	}

	public void UnPause()
	{
		_soundsSource.Play();
		_mainUI.SetActive(true);
		_pauseMenu.SetActive(false);
		_quitMenu.SetActive(false);
		StartTime();
	}

	public void Quit()
	{
		_mainUI.SetActive(false);
		_pauseMenu.SetActive(false);
		_quitMenu.SetActive(true);
		_gameOverMenu.SetActive(false);
	}

	public void QuitGameOver()
	{
		_mainUI.SetActive(false);
		_pauseMenu.SetActive(false);
		_quitGameOverMenu.SetActive(true);
		_gameOverMenu.SetActive(false);
	}

	public void GameOver()
	{
		_isGameOver = true;
		StopTime();
		_mainUI.SetActive(false);
		_pauseMenu.SetActive(false);
		_quitMenu.SetActive(false);
		_gameOverMenu.SetActive(true);
		_quitGameOverMenu.SetActive(false);
	}

	public void QuitGame()
	{
		SaveLoad.SaveGame(SettingsScript.MaxScore);
		Application.Quit();
	}

	public void ClickButton()
	{
		_soundsSource.Play();
	}

	public void Menu()
	{
		SaveLoad.SaveGame(SettingsScript.MaxScore);
		StartTime();
		SceneManager.LoadScene("MainMenuScene");
	}

	public void Repeat()
	{
		_isGameOver = false;
		SaveLoad.SaveGame(SettingsScript.MaxScore);
		StartTime();
		SceneManager.LoadScene("GameplayScene");
	}

	private void StopTime()
	{
		Time.timeScale = 0;
		SettingsScript.Pause = true;
	}

	private void StartTime()
	{
		Time.timeScale = 1;
		SettingsScript.Pause = false;
	}
}
