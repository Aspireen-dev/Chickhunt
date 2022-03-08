using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // ----- SINGLETON -----
    private static InputManager _instance;

    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private PlayerControls playerControls;
    private Player player;
    private GameManager gameManager;

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
        playerControls = new PlayerControls();
    }

    void OnEnable()
    {
        playerControls.Enable();
        EnableShoot();
        playerControls.Player.Pause.started += PauseWithContext;
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        player = Player.Instance;
        gameManager = GameManager.Instance;
    }

    // Enable listening of shoot events (mouse click)
    public void EnableShoot()
    {
        playerControls.Player.Shoot.started += AimWithContext;
        playerControls.Player.Shoot.canceled += ShootWithContext;
    }

    // Disable listening of shoots events (mouse click)
    public void DisableShoot()
    {
        playerControls.Player.Shoot.started -= AimWithContext;
        playerControls.Player.Shoot.canceled -= ShootWithContext;
    }

    private void AimWithContext(InputAction.CallbackContext context)
    {
        player.Aim();
    }

    private void ShootWithContext(InputAction.CallbackContext context)
    {
        player.Shoot();
    }

    private void PauseWithContext(InputAction.CallbackContext context)
    {
        if (gameManager.isPaused)
        {
            gameManager.UnPause();
        }
        else
        {
            gameManager.Pause();
        }
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }
}
