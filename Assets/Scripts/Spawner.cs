using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Tooltip("����������������")]
    [SerializeField] private float _sencentivity = 25f;
    [Tooltip("���������� �� ������")]
    [SerializeField] private float _maxPosition = 2.5f;

    // ������� ��������� ����������
    private float _xPosition;
    // ������ ��������� ����
    private float _oldMouseX;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _oldMouseX = Input.mousePosition.x;

        if (Input.GetMouseButton(0))
        {
            float delta = Input.mousePosition.x - _oldMouseX;
            _oldMouseX = Input.mousePosition.x;
            _xPosition += delta * _sencentivity / Screen.width;
            _xPosition = Mathf.Clamp(_xPosition, -_maxPosition, _maxPosition);
            transform.position = new Vector3(_xPosition, transform.position.y, transform.position.z);
        }
    }
}
