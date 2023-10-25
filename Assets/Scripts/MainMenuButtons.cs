using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
	[Header("Menus")]
	[SerializeField] private GameObject _mainMenu;
	[SerializeField] private GameObject _settingsMenu;
	[SerializeField] private GameObject _quiteMenu;
	[SerializeField] private GameObject _loadingMenu;

	[Header("Sliders")]
	[SerializeField] private Slider _musicSlider;
	[SerializeField] private Slider _soundsSlider;

	[Header("Audio Sources")]
	[SerializeField] private AudioSource _musicSource;
	[SerializeField] private AudioSource _soundsSource;

	[Header("Loading Script")]
	[SerializeField] private LoadingScript _loadingScript;

	private void Start()
	{
		SaveLoad.LoadGame();

		Menu();
		_musicSource.volume = SettingsScript.Music;
		_soundsSource.volume = SettingsScript.Sounds;
	}

	public void Menu()
	{
		_mainMenu.SetActive(true);
		_settingsMenu.SetActive(false);
		_quiteMenu.SetActive(false);
		_loadingMenu.SetActive(false);
	}

	public void Play()
	{
		SaveLoad.SaveGame(SettingsScript.MaxScore);

		_mainMenu.SetActive(false);
		_settingsMenu.SetActive(false);
		_quiteMenu.SetActive(false);
		_loadingMenu.SetActive(true);

		LoadGameScene();
	}

	public void Settings()
	{
		_musicSlider.value = SettingsScript.Music;
		_soundsSlider.value = SettingsScript.Sounds;

		_mainMenu.SetActive(false);
		_settingsMenu.SetActive(true);
		_quiteMenu.SetActive(false);
		_loadingMenu.SetActive(false);
	}

	public void Quit()
	{
		_mainMenu.SetActive(false);
		_settingsMenu.SetActive(false);
		_quiteMenu.SetActive(true);
		_loadingMenu.SetActive(false);
	}

	public void QuitGame()
	{
		SaveLoad.SaveGame(SettingsScript.MaxScore);

#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	public void ClickButton()
	{
		_soundsSource.Play();
	}

	public void SetMusic()
	{
		SettingsScript.Music = _musicSlider.value;
		_musicSource.volume = _musicSlider.value;
	}

	public void SetSounds()
	{
		SettingsScript.Sounds = _soundsSlider.value;
		_soundsSource.volume = _soundsSlider.value;
	}

	public void LoadGameScene()
	{
		_loadingScript.StartLoading();
	}
}
