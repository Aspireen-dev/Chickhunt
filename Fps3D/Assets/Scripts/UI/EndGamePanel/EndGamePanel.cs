using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI bestScoreText;

    public void SetScoreText(int score, int bestScore)
    {
        scoreText.text = "Score : " + score.ToString();
        bestScoreText.text = "best score : " + bestScore.ToString();
    }

    public void OnPlayBtnClick()
    {
        GameManager.Instance.Play();
    }

    public void OnMenuBtnClick()
    {
        GameManager.Instance.Menu();
    }
}
