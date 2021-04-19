using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void SetScoreText(int score)
    {
        scoreText.text = "Score : " + score.ToString();
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
