using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private Text scoreText;

    public void SetMaxHealth(int health)
    {
        healthBar.SetMaxHealth(health);
    }

    public void SetHealth(int health)
    {
        healthBar.SetHealth(health);
    }

    public void SetScoreText(int score)
    {
        scoreText.text = "Score : " + score.ToString();
    }
}
