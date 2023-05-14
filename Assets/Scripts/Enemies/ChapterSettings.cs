using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyWaves
{
    [Tooltip("Префаб врага")]
    public EnemyAnimal Enemy;
    [Tooltip("Количество")]
    public float[] NumberPerSecond;
}

[CreateAssetMenu]
public class ChapterSettings : ScriptableObject
{
    [Tooltip("Массив даннх о волнах врагов")]
    public EnemyWaves[] EnemyWavesArray;
}
