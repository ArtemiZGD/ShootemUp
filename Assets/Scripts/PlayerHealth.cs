using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GamePlaySceneButtons script;
    [SerializeField] private Text maxScore;
    [SerializeField] private Text score;
    [SerializeField] private Image[] HP;
    private int health;

    private void Start()
    {
        health = SettingsScript.startPlayerHealth;
    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
        }
        else
        {
            health--;
            Death();
        }

        ColorHP();
    }

    public void HealthUp()
    {
        if (health < 3)
        {
            health++;
        }

        ColorHP();
    }

    private void ColorHP()
    {
        for (int i = 0; i < SettingsScript.startPlayerHealth; i++)
        {
            if (i < health)
            {
                HP[i].color = Color.green;
            }
            else
            {
                HP[i].color = Color.gray;
            }
        }
    }

    private void Death()
    {
        SaveLoad.SaveGame(SettingsScript.score);

        score.text = "Score: " + SettingsScript.score;
        if (SettingsScript.score > SettingsScript.maxScore)
        {
            SettingsScript.maxScore = SettingsScript.score;
            maxScore.text = "New max score!";
        }
        else
        {
            maxScore.text = "Max score: " + SettingsScript.maxScore;
        }
        script.GameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ammo")
        {
            SettingsScript.bullets += 50;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Med")
        {
            HealthUp();
            Destroy(other.gameObject);
        }
    }
}
