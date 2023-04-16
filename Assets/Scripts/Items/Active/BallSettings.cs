using UnityEngine;

[CreateAssetMenu]
public class BallSettings : ScriptableObject
{
    [Tooltip("Массив материалов шара")]
    public Material[] BallMaterials;
    [Tooltip("Массив материалов проекции")]
    public Material[] BallProjectionsMaterials;
}
