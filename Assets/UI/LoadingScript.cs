using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class LoadingScript : MonoBehaviour
{
    [SerializeField] Slider loading;

    private AsyncOperation loadingSceneOperation;

    private void Start()
    {
    }

    private void Update()
    {
        if (loadingSceneOperation != null)
        {
            loading.value = Mathf.Lerp(loading.value, loadingSceneOperation.progress,
                Time.deltaTime * 2);
        }
    }

    public void StartLoading()
    {
        loadingSceneOperation = SceneManager.LoadSceneAsync("GameplayScene");

        loading.value = 0;
    }
}
