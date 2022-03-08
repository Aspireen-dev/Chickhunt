using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private int score;
    private int timeRemaining;
    private int currentTimeRemaining;

    [HideInInspector]
    public bool isPaused = false;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 60;
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            // TODO
            case "Tutorial":
                isPaused = true;
                Time.timeScale = 0f;
                score = 0;
                break;
            case "Game":
                isPaused = false;
                Time.timeScale = 1f;
                timeRemaining = 60;
                score = 0;
                currentTimeRemaining = timeRemaining;
#if !(UNITY_ANDROID || UNITY_IOS)
                HideCursor();
#endif
                StartCoroutine(StartChrono());
                break;
            default:
                isPaused = false;
                Time.timeScale = 1f;
                break;
        }
    }

    private IEnumerator StartChrono()
    {
        while (currentTimeRemaining > 0)
        {
            currentTimeRemaining--;
            UI.Instance.SetTimeRemainingText(currentTimeRemaining);
            yield return new WaitForSeconds(1f);
        }
        EndGame(false);
    }

    public void ChickenKilled(int value)
    {
        currentTimeRemaining += 5;
        score += value;
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Pause()
    {
#if !(UNITY_ANDROID || UNITY_IOS)
        ShowCursor();
#endif
        isPaused = true;
        UI.Instance.Pause();
        InputManager.Instance.DisableShoot();
        Time.timeScale = 0f;
    }

    public void UnPause()
    {
#if !(UNITY_ANDROID || UNITY_IOS)
        HideCursor();
#endif
        isPaused = false;
        UI.Instance.UnPause();
        InputManager.Instance.EnableShoot();
        Time.timeScale = 1f;
    }

    public void EndGame(bool allChickenKilled)
    {
        if (allChickenKilled)
        {
            score += currentTimeRemaining * 100;
        }
        int bestScore = GetBestScore();
        StopAllCoroutines();
#if !(UNITY_ANDROID || UNITY_IOS)
        ShowCursor();
#endif
        UI.Instance.EndGame(score, bestScore);
        InputManager.Instance.enabled = false;
        Time.timeScale = 0f;
    }

    private int GetBestScore()
    {
        int bestScore = PlayerPrefs.GetInt("bestScore", 0);
        if (bestScore < score)
        {
            bestScore = score;
            PlayerPrefs.SetInt("bestScore", bestScore);
        }
        return bestScore;
    }

    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
