using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private PlayerControls playerControls;

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
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

    public void EnableShoot()
    {
        playerControls.Player.Shoot.started += AimWithContext;
        playerControls.Player.Shoot.canceled += ShootWithContext;
    }

    public void DisableShoot()
    {
        playerControls.Player.Shoot.started -= AimWithContext;
        playerControls.Player.Shoot.canceled -= ShootWithContext;
    }

    private void AimWithContext(InputAction.CallbackContext context)
    {
        Player.Instance.Aim();
    }

    private void ShootWithContext(InputAction.CallbackContext context)
    {
        Player.Instance.Shoot();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumpThisFrame()
    {
        return playerControls.Player.Jump.triggered;
    }
}
