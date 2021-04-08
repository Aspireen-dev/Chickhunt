using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    public void SetScoreText(int score)
    {
        scoreText.text = "Score : " + score.ToString();
    }

    public void OnPlayBtnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMenuBtnClick()
    {
        //TODO
    }
}
