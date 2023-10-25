using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerGun : MonoBehaviour
{
	[SerializeField] private float _volume = 0.2f;
	[SerializeField] private Text _score;
	[SerializeField] private Text _ammo;
	[SerializeField] private GameObject _bullet;

	private AudioSource _audioSource;
	private int _bullets = 50;

	private void Start()
	{
		_audioSource = GetComponent<AudioSource>();
		_audioSource.volume = SettingsScript.Sounds * _volume;
		SettingsScript.Bullets = _bullets;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0) && !SettingsScript.Pause && SettingsScript.Bullets > 0)
		{
			Shot();
		}
		_ammo.text = "Ammo: " + SettingsScript.Bullets;
		_score.text = "Score: " + SettingsScript.Score;
	}

	private void Shot()
	{
		Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
		Instantiate(_bullet, transform.position, 
			Quaternion.LookRotation(new Vector3(hit.point.x - transform.position.x, 0, hit.point.z - transform.position.z)));
		SettingsScript.Bullets--;

		_audioSource.Play();
	}
}
