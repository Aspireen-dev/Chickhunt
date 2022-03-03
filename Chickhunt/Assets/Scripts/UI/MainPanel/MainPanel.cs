using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainPanel : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private TextMeshProUGUI timeRemainingText;
    [SerializeField]
    private TextMeshProUGUI nbArrowsText;
    [SerializeField]
    private TextMeshProUGUI nbChickenKilledText;

    // Touch area to check for aim
    private Rect touchArea;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 topLeftPosition = new Vector2(600, 0);
        Vector2 size = new Vector2(Screen.width - 600, Screen.height);
        touchArea = new Rect(topLeftPosition, size);
    }

    public Vector2 GetTouchDelta()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touchArea.Contains(touch.position) && touch.phase == TouchPhase.Moved)
                {
                    return touch.deltaPosition;
                }
            }
        }
        return Vector2.zero;
    }

    public void SetMaxHealth(int health)
    {
        healthBar.SetMaxHealth(health);
    }

    public void SetHealth(int health)
    {
        healthBar.SetHealth(health);
    }

    public void SetTimeRemainingText(int time)
    {
        timeRemainingText.text = "Time : " + time.ToString();
    }

    public void SetNbArrowsText(int nbArrows)
    {
        nbArrowsText.text = nbArrows.ToString();
    }

    public void SetNbChickenKilled(int nbChickens, int nbChickenToSpawn)
    {
        nbChickenKilledText.text = nbChickens.ToString() + "/" + nbChickenToSpawn;
    }
}
