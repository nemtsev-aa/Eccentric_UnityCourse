using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Tooltip("Время уровня")]
    [SerializeField] private int _levelTime;
    [Tooltip("Персонаж")]
    [SerializeField] private GameObject _player;
    [Tooltip("Cтартовая позиция персонажа")]
    [SerializeField] private Transform _startTransform;

    public void ResetLevel()
    {
        ResetPlayerPosition();
        ResetPlayerHealth();
    }

    public void ResetPlayerPosition()
    {
        _player.transform.position = _startTransform.position;
    }

    public void ResetPlayerHealth()
    {
        _player.GetComponent<PlayerHealth>().ResetHealth();
    }

    public int GetLevelTime()
    {
        return _levelTime;
    }
}
