using UnityEngine;

[CreateAssetMenu]
public class BallSettings : ScriptableObject
{
    [Tooltip("������ ���������� ����")]
    public Material[] BallMaterials;
    [Tooltip("������ ���������� ��������")]
    public Material[] BallProjectionsMaterials;
}
