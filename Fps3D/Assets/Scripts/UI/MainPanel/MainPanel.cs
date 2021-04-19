using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainPanel : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI timeRemainingText;
    [SerializeField]
    private TextMeshProUGUI nbArrowsText;

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

    public void SetTimeRemainingText(int time)
    {
        timeRemainingText.text = "Time : " + time.ToString();
    }

    public void SetNbArrowsText(int nbArrows)
    {
        nbArrowsText.text = nbArrows.ToString();
    }
}
