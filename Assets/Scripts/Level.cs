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
    [SerializeField] private int _numberOfBalls = 50;
    [Tooltip("Предел уровней у создаваемых шаров")]
    [SerializeField] private int _maxCreatedBallLevel = 1;
    [Tooltip("Список задач на уровень")]
    [SerializeField] private Task[] _tasks;

    public int NumberOfBalls => _numberOfBalls;
    public int MaxCreatedBallLevel => _maxCreatedBallLevel;
    public Task[] Tasks => _tasks;
    public static Level Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnValidate()
    {
        if (_maxCreatedBallLevel <= 0)
            _maxCreatedBallLevel = 1;
    }
}
