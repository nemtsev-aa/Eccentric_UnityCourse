using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera RaycastCamera;

    private Vector3 _startPoint;
    private Vector3 _cameraStartPosition;
    private Plane _plane;

    private void Start() {
        _plane = new Plane(Vector3.up, Vector3.zero);
    }

    private void Update() {
        Ray ray = RaycastCamera.ScreenPointToRay(Input.mousePosition);

        float distance;
        _plane.Raycast(ray, out distance);
        Vector3 point = ray.GetPoint(distance);

        if (Input.GetMouseButtonDown(2)) {
            _startPoint = point;
            _cameraStartPosition = transform.position;
        }

        if (Input.GetMouseButton(2)) {
            Vector3 offset = point - _startPoint;
            transform.position = _cameraStartPosition - offset;
        }

        transform.Translate(0f, 0f, Input.mouseScrollDelta.y);
        RaycastCamera.transform.Translate(0f, 0f, Input.mouseScrollDelta.y);
    }

    void LateUpdate() {

        Vector2 mousePos = Input.mousePosition; 
        mousePos.x /= Screen.width;
        mousePos.y /= Screen.height;

        Vector2 delta = mousePos - new Vector2(0.5f, 0.5f);

        float sideBorder = Mathf.Min(Screen.width, Screen.height) / 20f;

        float xDist = Screen.width * (0.5f - Mathf.Abs(delta.x));
        float yDist = Screen.height * (0.5f - Mathf.Abs(delta.y));

        if (xDist < sideBorder || yDist < sideBorder) {
            delta = delta.normalized;
            delta *= Mathf.Clamp01(1 - Mathf.Min(xDist, yDist) / sideBorder);

            transform.Translate(delta * 10 * Time.deltaTime, Space.Self);
        }
    }
}
