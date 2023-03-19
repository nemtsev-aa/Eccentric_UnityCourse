using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Tooltip("����� ������")]
    [SerializeField] private int _levelTime;
    [Tooltip("��������")]
    [SerializeField] private GameObject _player;
    [Tooltip("C�������� ������� ���������")]
    [SerializeField] private Transform _startTransform;

    public void ResetPlayerPosition()
    {
        _player.transform.position = _startTransform.position;
    }

    public int GetLevelTime()
    {
        return _levelTime;
    }
}
