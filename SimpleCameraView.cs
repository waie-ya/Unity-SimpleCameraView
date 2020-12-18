using UnityEngine;

/// <summary>
/// このスクリプトをカメラに設定する
/// </summary>
[RequireComponent(typeof(Camera))]
public class SceneViewCamera : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10f)]
    private float wheelSpeed = 1f;

    [SerializeField, Range(0.1f, 10f)]
    private float moveSpeed = 0.3f;

    [SerializeField, Range(0.1f, 10f)]
    private float rotateSpeed = 0.3f;

    [SerializeField, Range(0.1f, 50f)]
    private float keyPressSpeed = 10.0f;

    private Vector3 preMousePos;

    private void Update()
    {
        MouseUpdate();
        return;
    }

    private void MouseUpdate()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0.0f)
            MouseWheel(scrollWheel);

        if (Input.GetMouseButtonDown(0) ||
           Input.GetMouseButtonDown(1) ||
           Input.GetMouseButtonDown(2))
            preMousePos = Input.mousePosition;

        MouseDrag(Input.mousePosition);

        KeyPress();
    }

    private void MouseWheel(float delta)
    {
        transform.position += transform.forward * delta * wheelSpeed;
        return;
    }

    private void MouseDrag(Vector3 mousePos)
    {
        Vector3 diff = mousePos - preMousePos;

        if (diff.magnitude < Vector3.kEpsilon)
            return;

        if (Input.GetMouseButton(2))
            transform.Translate(-diff * Time.deltaTime * moveSpeed);
        else if (Input.GetMouseButton(1))
            CameraRotate(new Vector2(-diff.y, diff.x) * rotateSpeed);

        preMousePos = mousePos;
    }

    private void KeyPress()
    {
        if (Input.GetKey(KeyCode.W)) { transform.position += transform.forward * Time.deltaTime * keyPressSpeed; }
        if (Input.GetKey(KeyCode.S)) { transform.position +=  -transform.forward * Time.deltaTime * keyPressSpeed; }
        if (Input.GetKey(KeyCode.D)) { transform.position += transform.right * Time.deltaTime * keyPressSpeed; }
        if (Input.GetKey(KeyCode.A)) { transform.position += -transform.right * Time.deltaTime * keyPressSpeed; }
        if (Input.GetKey(KeyCode.E)) { transform.position += transform.up * Time.deltaTime * keyPressSpeed; }
        if (Input.GetKey(KeyCode.Q)) { transform.position += -transform.up * Time.deltaTime * keyPressSpeed; }

    }

    public void CameraRotate(Vector2 angle)
    {
        transform.RotateAround(transform.position, transform.right, angle.x);
        transform.RotateAround(transform.position, Vector3.up, angle.y);
    }
}
