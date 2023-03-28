using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Tooltip("Время уровня")]
    [SerializeField] private int _levelTime;
    [Tooltip("Персонаж")]
    [SerializeField] private GameObject _player;
    [Tooltip("Выход")]
    [SerializeField] private GameObject _exit;
    [Tooltip("Эффект появления выхода")]
    [SerializeField] private ParticleSystem _exitParticle;

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

    public void ShowExit()
    {
        _exit.SetActive(true);
        _exit.transform.position = _player.transform.position + new Vector3(4f, 3f, 0f);
        Instantiate(_exitParticle, _exit.transform.position, _exit.transform.rotation);
    }
}
