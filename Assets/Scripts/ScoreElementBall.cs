using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreElementBall : ScoreElement
{
    [Tooltip("����� ��� ����������� ������")]
    [SerializeField] private TextMeshProUGUI _levelText;
    [Tooltip("�����������")]
    [SerializeField] private RawImage _ballImage;
    [Tooltip("��������� �����")]
    [SerializeField] private BallSettings _ballSettings;
   
    public override void Setup(Task task)
    {
        base.Setup(task);

        int number = (int)Mathf.Pow(2, task.Level + 1);     // ���������� �������� ������� �� ��������� ������
        _levelText.text = number.ToString();    // ���������� ������� 
        _ballImage.color = _ballSettings.BallMaterials[task.Level].color;   //�������� ���� ���� � ������������ � �������
        Level = task.Level;     // ������������� ������� �������
    }
}
  