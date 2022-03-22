using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject quiteMenu;
    [SerializeField] GameObject loadingMenu;

    [SerializeField] Slider music;
    [SerializeField] Slider sounds;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundsSource;

    [SerializeField] LoadingScript loadingScript;

    private AsyncOperation loadingSceneOperation;

    private void Start()
    {
        SaveLoad.LoadGame();

        Menu();
        musicSource.volume = SettingsScript.music;
        soundsSource.volume = SettingsScript.sounds;
    }

    public void Menu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        quiteMenu.SetActive(false);
        loadingMenu.SetActive(false);
    }

    public void Play()
    {
        SaveLoad.SaveGame(SettingsScript.maxScore);

        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        quiteMenu.SetActive(false);
        loadingMenu.SetActive(true);

        LoadGameScene();
    }

    public void Settings()
    {
        music.value = SettingsScript.music;
        sounds.value = SettingsScript.sounds;

        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        quiteMenu.SetActive(false);
        loadingMenu.SetActive(false);
    }

    public void Quit()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        quiteMenu.SetActive(true);
        loadingMenu.SetActive(false);
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

    public void SetMusic()
    {
        SettingsScript.music = music.value;
        musicSource.volume = music.value;
    }

    public void SetSounds()
    {
        SettingsScript.sounds = sounds.value;
        soundsSource.volume = sounds.value;
    }

    private bool end;
    private float progress;

    public void LoadGameScene()
    {
        loadingScript.StartLoading();
    }
}
