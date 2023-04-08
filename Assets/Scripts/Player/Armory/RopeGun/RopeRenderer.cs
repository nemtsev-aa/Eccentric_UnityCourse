using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    [Tooltip("��������� - �������� ������������ �����")]
    public LineRenderer LineRenderer;
    [Tooltip("���������� ��������� �����")]
    [SerializeField] private int _segmentsCount = 10;

    public void Draw(Vector3 startPoint, Vector3 endPoint, float length)
    {
        LineRenderer.enabled = true;
        // ���������� ����� �������
        float interpolant = Vector3.Distance(startPoint, endPoint);
        // �������� ���������� �����
        float offset = Mathf.Lerp(length / 2f, 0f, interpolant);
        // ������� ����� ���������� ��� ������ ������ �����
        Vector3 startPointDown = startPoint + Vector3.down * offset;
        // ������� ����� ���������� ��� ������ ����� �����
        Vector3 endPointDown = endPoint + Vector3.down * offset;

        LineRenderer.positionCount = _segmentsCount + 1;
        for (int i = 0; i < _segmentsCount +1; i++)
        {
            // ������������� ��������� ����� ����� � ������������ � ������ ������ �����
            LineRenderer.SetPosition(i, Bezier.GetPoint(startPoint, startPointDown, endPointDown, endPoint, (float)i / _segmentsCount));
        }
    }

    public void Hide()
    {
        LineRenderer.enabled = false;
    }

}
