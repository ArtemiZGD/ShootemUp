using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField] private GamePlaySceneButtons _script;
	[SerializeField] private Text _maxScore;
	[SerializeField] private Text _score;
	[SerializeField] private Image[] _HP;
	private int _health;

	private void Start()
	{
		_health = SettingsScript.StartPlayerHealth;
	}

	public void HealthDown()
	{
		if (_health > 1)
		{
			_health--;
		}
		else
		{
			_health--;
			Death();
		}

		ColorHP();
	}

	public void HealthUp()
	{
		if (_health < 3)
		{
			_health++;
		}

		ColorHP();
	}

	private void ColorHP()
	{
		for (int i = 0; i < SettingsScript.StartPlayerHealth; i++)
		{
			if (i < _health)
			{
				_HP[i].color = Color.green;
			}
			else
			{
				_HP[i].color = Color.gray;
			}
		}
	}

	private void Death()
	{
		SaveLoad.SaveGame(SettingsScript.Score);

		_score.text = "Score: " + SettingsScript.Score;
		if (SettingsScript.Score > SettingsScript.MaxScore)
		{
			SettingsScript.MaxScore = SettingsScript.Score;
			_maxScore.text = "New max score!";
		}
		else
		{
			_maxScore.text = "Max score: " + SettingsScript.MaxScore;
		}
		_script.GameOver();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Ammo")
		{
			SettingsScript.Bullets += 50;
			Destroy(other.gameObject);
		}
		else if (other.tag == "Med")
		{
			HealthUp();
			Destroy(other.gameObject);
		}
	}
}
