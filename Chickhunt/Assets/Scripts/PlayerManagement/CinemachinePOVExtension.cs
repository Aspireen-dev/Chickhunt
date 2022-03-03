using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{
    private float horizontalSpeed = 8f;
    private float verticalSpeed = 8f;
    private float clampAngle = 70f;

    [SerializeField]
    private MainPanel mainPanel;

    private InputManager inputManager;
    private Vector3 startingRotation;

    protected override void Awake()
    {
        // InputManager must be called before this script to be referenced
        // Go to Edit -> Project Settings -> Script Execution Order, and set a smaller value to InputManager than the one set to this script
        inputManager = InputManager.Instance;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (Application.isPlaying)
        {
            if (vcam.Follow)
            {
                if (stage == CinemachineCore.Stage.Aim)
                {
                    if (startingRotation == null) startingRotation = transform.localRotation.eulerAngles;
                    Vector2 deltaInput = mainPanel.GetTouchDelta();
                    startingRotation.x += deltaInput.x * horizontalSpeed * Time.deltaTime;
                    startingRotation.y += deltaInput.y * verticalSpeed * Time.deltaTime;
                    startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                    state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
                }
            }
        }
    }
}
