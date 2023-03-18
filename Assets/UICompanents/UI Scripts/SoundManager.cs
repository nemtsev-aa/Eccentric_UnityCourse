using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Tooltip("�������� �������� ��������")]
    [SerializeField] private GameProcessManager _gameProcessManager;
    [SerializeField] private Scrollbar _soundVolumeController;
    [Header("Sound Effects")]
    [Tooltip("�������� �������� ��������")]
    [SerializeField] private AudioSource _soundEffectsSourse;
    [Tooltip("���� ����� ������")]
    [SerializeField] private AudioClip _coinPicSound;
    [Tooltip("���� ���������� ��������� ����")]
    [SerializeField] private AudioClip _fallingSound;
    [Tooltip("���� �������� ��������� ����")]
    [SerializeField] private AudioClip _winSound;

    private void Start()
    {
        _soundVolumeController.value = _soundEffectsSourse.volume;
    }
    /// �������� �� ������� 
    private void OnEnable()
    {
        _gameProcessManager.OnWin += _gameProcessManager_OnWin;
        _gameProcessManager.OnLose += _gameProcessManager_OnLose;
    }

    /// ������� �� ������� 
    private void OnDisable()
    {
        _gameProcessManager.OnWin -= _gameProcessManager_OnWin;
        _gameProcessManager.OnLose -= _gameProcessManager_OnLose;
    }

    private void �ollecting�oins()
    {
        _soundEffectsSourse.PlayOneShot(_coinPicSound, 1f);
    }

    private void _gameProcessManager_OnWin()
    {
        _soundEffectsSourse.PlayOneShot(_winSound, 1f);
    }

    private void _gameProcessManager_OnLose()
    {
        _soundEffectsSourse.PlayOneShot(_fallingSound, 1f);
    }

    public void ShowSoundVolumeController()
    {
        _soundVolumeController.interactable = !_soundEffectsSourse.mute;
    }
}
