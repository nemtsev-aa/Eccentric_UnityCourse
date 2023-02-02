using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [Header("�������� �����������")]
    [Range(3, 7)]
    public float Speed = 3f;
    [Header("���������")]
    [Range(20, 25)]
    public float Boost = 25f;
    [Header("�������� ��������")]
    [Range(-600, 600)]
    public float RotationSpeed = -600f;
    [Header("������� ������")]
    [Range(0.7f, 0.9f)]
    public float LocalScaleY = 0.7f;

    void Update()
    {
        Move();
        Rotation();
        Compression();
    }

    /// ����������� ������
    private void Move()
    {
        //�������� �� �������������� ���
        float h = Input.GetAxis("Horizontal");
        //�������� �� ������������ ���
        float v = Input.GetAxis("Vertical");
        //����� ��������
        Vector3 offset;

        //��������� ��������� ���������
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //�������� � ����������
            offset = new Vector3(h, 0, v) * Boost * Time.deltaTime;
        }
        else
        {
            //���������� ��������
            offset = new Vector3(h, 0, v) * Speed * Time.deltaTime;
        }

        transform.Translate(offset);
    }
    /// ������� ������
    private void Rotation()
    {
        //�������� ���� �� �������������� ���
        float yRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, yRotation * RotationSpeed * Time.deltaTime, 0);
    }
    /// ��������� ������� ������
    private void Compression()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            //��������� ������
            transform.localScale = new Vector3(1f, LocalScaleY, 1f);
        }
        else
        {
            //���������� ������
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }     
}
