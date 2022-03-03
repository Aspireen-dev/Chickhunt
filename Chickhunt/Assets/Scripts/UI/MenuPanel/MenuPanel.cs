using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject controlsPanel;
    [SerializeField]
    private GameObject loadingPanel;

    public void OnPlayBtnClick()
    {
        GameManager.Instance.Play();
        loadingPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnControlsBtnClick()
    {
        controlsPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnQuitBtnClick()
    {
        Application.Quit();
    }
}
