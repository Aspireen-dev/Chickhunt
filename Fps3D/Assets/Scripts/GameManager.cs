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

    private int timeRemaining = 20;
    [HideInInspector]
    public int currentTimeRemaining;
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
#if UNITY_EDITOR || UNITY_STANDALONE
        Application.targetFrameRate = 80;
#elif UNITY_ANDROID || UNITY_IOS
        Application.targetFramerate = 60;
#endif
        SceneManager.sceneLoaded += Reset;
    }

    private void Reset(Scene scene, LoadSceneMode mode)
    {
        isPaused = false;
        currentTimeRemaining = timeRemaining;
        Time.timeScale = 1f;

        if (scene.name == "Game")
        {
            StartCoroutine(StartChrono());
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

    public void ChickenKilled()
    {
        currentTimeRemaining += 5;
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
        ShowCursor();
        isPaused = true;
        UI.Instance.Pause();
        InputManager.Instance.DisableShoot();
        Time.timeScale = 0f;
    }

    public void UnPause()
    {
        HideCursor();
        isPaused = false;
        UI.Instance.UnPause();
        InputManager.Instance.EnableShoot();
        Time.timeScale = 1f;
    }

    public void EndGame(bool allChickenKilled)
    {
        if (allChickenKilled)
        {
            Player.Instance.SetTimeScore(currentTimeRemaining);
        }
        StopAllCoroutines();
        ShowCursor();
        UI.Instance.EndGame();
        InputManager.Instance.enabled = false;
        Time.timeScale = 0f;
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
