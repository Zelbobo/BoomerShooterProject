using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCamera : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float sensitivityJoystick;
    [SerializeField] private float sensitivityTouch;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    [Header("Inputs")]
    [SerializeField] private Joystick cameraJoystick;

    #region [PrivateVars]

    private float mouseX;
    private float mouseY;

    private Camera _camera;

    #endregion

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Inputs();
        CameraRotation();
    }

    private void CameraRotation()
    {
        mouseY = Mathf.Clamp(mouseY, minY, maxY);

        _camera.transform.localRotation = Quaternion.AngleAxis(mouseY, Vector3.right);
        transform.rotation = Quaternion.AngleAxis(mouseX, Vector3.up);
    }

    private void Inputs()
    {
        if ((cameraJoystick.Horizontal != 0) || (cameraJoystick.Vertical != 0))
        {
            mouseX += cameraJoystick.Horizontal * sensitivityJoystick;
            mouseY -= cameraJoystick.Vertical * sensitivityJoystick;
        }
    }

    public void SetRotation(Vector2 rotation)
    {
        mouseX += rotation.x * sensitivityTouch;
        mouseY -= rotation.y * sensitivityTouch;
    }
}
