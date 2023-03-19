using UnityEngine;

public class HeadRotation : MonoBehaviour
{
    [Tooltip("����")]
    [SerializeField] private Transform _target;
    //[Tooltip("�������� �������� � ����")]
    //[SerializeField] private float _lerpSpeed = 10f;
    [Tooltip("������ �������� ������ �����")]
    [SerializeField] private Vector3 _headUp = new Vector3(15, 0, 0);
    [Tooltip("������ �������� ������ ����")]
    [SerializeField] private Vector3 _headDown = new Vector3(-15, 0, 0);

    void Update()
    {
        // �������� ������� �� ��� Y, ������������ ������
        float yAimOffset =  transform.position.y - _target.position.y;
        float interpolant = Mathf.InverseLerp(-5f, 5f, yAimOffset);
        // ������� ������ � ������������� �����������
        transform.localRotation = Quaternion.Lerp(Quaternion.Euler(_headUp), Quaternion.Euler(_headDown), interpolant);
    }
}
