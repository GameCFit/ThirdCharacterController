using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveble : MonoBehaviour
{
    private const string AxisHorizontal = "Horizontal";
    private const string AxisVertical = "Vertical";

    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;

    [SerializeField] private Joystick _joystick;
    [SerializeField] private DevicePlatform _platform;

    private Rigidbody _rigidbody;
    private Camera _camera;

    private float HorizontalInput() => _platform.GetPlatform == Platform.desktop ? Input.GetAxis(AxisHorizontal) : _joystick.Horizontal;
    private float VerticalInput() => _platform.GetPlatform == Platform.desktop ? Input.GetAxis(AxisVertical) : _joystick.Vertical;
    
    private Vector3 CameraForward()
    {
        Vector3 vector = _camera.transform.forward;
        vector.y = 0;
        vector.Normalize();
        return vector;
    }
    private Vector3 CameraRight()
    {
        Vector3 vector = _camera.transform.right;
        vector.y = 0;
        vector.Normalize();
        return vector;
    }

    private float GetMoreInput()
    {
        float vertical = VerticalInput() < 0 ? -VerticalInput() : VerticalInput();
        float horizontal = HorizontalInput() < 0 ? -HorizontalInput() : HorizontalInput();
        return vertical > horizontal ? vertical : horizontal;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
        
        _joystick.gameObject.SetActive(_platform.GetPlatform == Platform.mobile);
    }

    private void FixedUpdate()
    {
        if (HorizontalInput() != 0 || VerticalInput() != 0)
            Moving();
        else
            _playerAnimator.SetSpeed(0);
    }

    private void Moving()
    {
        Vector3 direction = (CameraForward() * VerticalInput() + CameraRight() * HorizontalInput()).normalized;
        float speed = GetMoreInput() > 0.7 ? _runSpeed : _walkSpeed;
        
        _rigidbody.MovePosition(transform.position + direction * speed);
        Rotation(direction);
    }

    private void Rotation(Vector3 direction) => _rigidbody.MoveRotation(Quaternion.LookRotation(direction));
}
