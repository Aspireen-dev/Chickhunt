using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private Animator crosshairAnim;

    [SerializeField]
    private Image top;
    [SerializeField]
    private Image bottom;
    [SerializeField]
    private Image left;
    [SerializeField]
    private Image right;

    private static Crosshair _instance;

    public static Crosshair Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        crosshairAnim = GetComponent<Animator>();
    }

    public void Aim()
    {
        crosshairAnim.SetBool("isAiming", true);
    }

    public void Shoot()
    {
        crosshairAnim.SetBool("isAiming", false);
    }

    public void UpdateCrosshair(bool enemyFound)
    {
        if (enemyFound)
        {
            top.color = Color.red;
            bottom.color = Color.red;
            left.color = Color.red;
            right.color = Color.red;
        }
        else
        {
            top.color = Color.white;
            bottom.color = Color.white;
            left.color = Color.white;
            right.color = Color.white;
        }
    }
}
