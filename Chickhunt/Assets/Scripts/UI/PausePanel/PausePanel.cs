using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public void OnPlayBtnClick()
    {
        GameManager.Instance.UnPause();
        gameObject.SetActive(false);
    }

    public void OnMenuBtnClick()
    {
        GameManager.Instance.Menu();
    }
}
