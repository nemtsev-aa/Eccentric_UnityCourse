using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyWaves
{
    [Tooltip("������ �����")]
    public EnemyAnimal Enemy;
    [Tooltip("����������")]
    public float[] NumberPerSecond;
}

[CreateAssetMenu]
public class ChapterSettings : ScriptableObject
{
    [Tooltip("������ ����� � ������ ������")]
    public EnemyWaves[] EnemyWavesArray;
}
