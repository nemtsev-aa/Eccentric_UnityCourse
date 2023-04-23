using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Task 
{
    [Tooltip("Тип объекта")]
    public ItemType ItemType;
    [Tooltip("Количество для сбора")]
    public int Number;
    [Tooltip("Требуемый уровень")]
    public int Level;
}
public class Level : MonoBehaviour
{
    [Tooltip("Количество шаров на уровень")]
    public int NumberOfBalls = 11;
    [Tooltip("Предел уровней у создаваемых шаров")]
    public int MaxCreatedBallLevel;
    [Tooltip("Список задач на уровень")]
    [SerializeField] private Task[] _tasks;

    
    
    public Task[] Tasks => _tasks;
    public static Level Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnValidate()
    {
        if (MaxCreatedBallLevel <= 0)
            MaxCreatedBallLevel = 1;
    }
}
