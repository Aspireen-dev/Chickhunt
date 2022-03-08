using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject phoneControlsPanel;
    [SerializeField]
    private GameObject pcControlsPanel;
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
#if UNITY_ANDROID || UNITY_IOS
        phoneControlsPanel.SetActive(true);
#else
        pcControlsPanel.SetActive(true);
#endif
        gameObject.SetActive(false);
    }

    public void OnQuitBtnClick()
    {
        Application.Quit();
    }
}
