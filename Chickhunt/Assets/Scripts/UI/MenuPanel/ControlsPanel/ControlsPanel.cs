using UnityEngine;

public class ControlsPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject mainPanel;

    public void OnBackBtnClick()
    {
        mainPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
