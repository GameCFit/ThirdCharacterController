using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float sensitivity = 900f;
    [SerializeField] private float minVerticalAngle = -60f;
    [SerializeField] private float maxVerticalAngle = 60f;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float _yOffset;

    private float xRotation;
    private float yRotation;

    private float mouseX;
    private float mouseY;

    private void Start()
    {
        xRotation = transform.eulerAngles.y;
        yRotation = transform.eulerAngles.x;
    }

    private void FixedUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        xRotation += mouseX;
        yRotation -= mouseY;

        yRotation = Mathf.Clamp(yRotation, minVerticalAngle, maxVerticalAngle);

        transform.eulerAngles = new Vector3(yRotation, xRotation, 0);

        Vector3 position = target.position - transform.forward * distance;

        transform.position = position;

        transform.LookAt(target.position + Vector3.up * _yOffset);
    }

    public void GetMouseInput()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
    }

    public void ResetMouseInput()
    {
        mouseX = 0;
        mouseY = 0;
    }
}