using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemy : MonoBehaviour
{
	[SerializeField] private float chanceToSpawnAmmo = 0.5f;
	[SerializeField] private float chanceToSpawnMed = 0.1f;

	[Min(22)]
	[SerializeField] private float spawnDistance = 22f;

	[SerializeField] private List<GameObject> spawnPoint;
	[SerializeField] private GameObject enemy1;
	[SerializeField] private GameObject enemy2;
	[SerializeField] private GameObject enemy3;
	[SerializeField] private GameObject turret;

	[SerializeField] private GameObject ammo;
	[SerializeField] private GameObject med;

	[SerializeField] private GameObject boxesPrefab;

	[SerializeField] private AnimationCurve enemyCount;
	[SerializeField] private AnimationCurve chanceToSpawnEnemy1;
	[SerializeField] private AnimationCurve chanceToSpawnEnemy2;
	[SerializeField] private AnimationCurve chanceToSpawnEnemy3;
	[SerializeField] private AnimationCurve chanceToSpawnTurret;

	[SerializeField] private Text enemies;
	[SerializeField] private Text wave;

	private GameObject _player;
	private List<GameObject> _spawnPointToUse = new List<GameObject>();
	private int _waveNum;

	private bool _ammoSpawned;
	private bool _medSpawned;

	private GameObject _boxes;

	private void Start()
	{
		_boxes = Instantiate(boxesPrefab);
		_player = GameObject.FindGameObjectWithTag("Player");
		_waveNum = 0;
		NewWave();
	}

	private void Update()
	{
		if (transform.childCount == 0)
		{
			NewWave();
		}
		enemies.text = "Enemies left: " + transform.childCount;
		wave.text = "Wave: " + _waveNum;
	}

	private void NewWave()
	{
		Destroy(_boxes);
		_boxes = Instantiate(boxesPrefab);

		_ammoSpawned = false;
		_medSpawned = false;

		for (int i = 0; i < spawnPoint.Count; i++)
		{
			_spawnPointToUse.Add(spawnPoint[i]);
		}
		_waveNum++;

		for (int i = 0; i < enemyCount.Evaluate(_waveNum); i++)
		{
			SpawnNewEnemy();
		}
		for (int i = 0; i < _spawnPointToUse.Count; i++)
		{
			int randomSpawnPoint = Random.Range(0, _spawnPointToUse.Count);

			if ((_player.transform.position - _spawnPointToUse[randomSpawnPoint].transform.position).magnitude < spawnDistance)
			{
				if (!_ammoSpawned)
				{
					Instantiate(ammo, _spawnPointToUse[randomSpawnPoint].transform.position, Quaternion.identity, _boxes.transform);
					_ammoSpawned = true;
				}
				else if (!_medSpawned)
				{
					Instantiate(med, _spawnPointToUse[randomSpawnPoint].transform.position, Quaternion.identity, _boxes.transform);
					_medSpawned = true;
				}
				_spawnPointToUse.RemoveAt(randomSpawnPoint);
			}
		}
	}

	private void SpawnNewEnemy()
	{
		float randomNum = Random.Range(0, 
			chanceToSpawnEnemy1.Evaluate(_waveNum) + 
			chanceToSpawnEnemy2.Evaluate(_waveNum) + 
			chanceToSpawnEnemy3.Evaluate(_waveNum) +
			chanceToSpawnTurret.Evaluate(_waveNum));

		int randomSpawnPoint = 0;

		if (Random.Range(0, 1.0f) > chanceToSpawnAmmo)
		{
			_ammoSpawned = true;
		}
		if (Random.Range(0, 1.0f) > chanceToSpawnMed)
		{
			_medSpawned = true;
		}

		while (_spawnPointToUse.Count > 0)
		{
			randomSpawnPoint = Random.Range(0, _spawnPointToUse.Count);

			if ((_player.transform.position - _spawnPointToUse[randomSpawnPoint].transform.position).magnitude < spawnDistance)
			{
				if (!_ammoSpawned)
				{
					Instantiate(ammo, _spawnPointToUse[randomSpawnPoint].transform.position, Quaternion.identity, _boxes.transform);
					_ammoSpawned = true;
				}
				else if (!_medSpawned)
				{
					Instantiate(med, _spawnPointToUse[randomSpawnPoint].transform.position, Quaternion.identity, _boxes.transform);
					_medSpawned = true;
				}
				_spawnPointToUse.RemoveAt(randomSpawnPoint);
			}
			else
			{
				break;
			}
		} 


		if (_spawnPointToUse.Count > 0)
		{
			if (randomNum < chanceToSpawnEnemy1.Evaluate(_waveNum))
			{
				Instantiate(enemy1, _spawnPointToUse[randomSpawnPoint].transform.position, Quaternion.identity, transform);
			}
			else if (randomNum < chanceToSpawnEnemy2.Evaluate(_waveNum) + chanceToSpawnEnemy1.Evaluate(_waveNum))
			{
				Instantiate(enemy2, _spawnPointToUse[randomSpawnPoint].transform.position, Quaternion.identity, transform);
			}
			else if (randomNum < chanceToSpawnEnemy3.Evaluate(_waveNum) + chanceToSpawnEnemy2.Evaluate(_waveNum) + chanceToSpawnEnemy1.Evaluate(_waveNum))
			{
				Instantiate(enemy3, _spawnPointToUse[randomSpawnPoint].transform.position, Quaternion.identity, transform);
			}
			else
			{
				Instantiate(turret, _spawnPointToUse[randomSpawnPoint].transform.position, Quaternion.identity, transform);
			}

			_spawnPointToUse.RemoveAt(randomSpawnPoint);
		}
	}
}
