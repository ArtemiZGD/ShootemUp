using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class LoadingScript : MonoBehaviour
{
	[SerializeField] private Slider _loading;

	private AsyncOperation _loadingSceneOperation;

	private void Update()
	{
		if (_loadingSceneOperation != null)
		{
			_loading.value = Mathf.Lerp(_loading.value, _loadingSceneOperation.progress,
				Time.deltaTime * 2);
		}
	}

	public void StartLoading()
	{
		_loadingSceneOperation = SceneManager.LoadSceneAsync("GameplayScene");

		_loading.value = 0;
	}
}
