using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Scrollbar _soundVolumeController;
    [Header("Sound Effects")]
    [Tooltip("Источник звуковых эффектов")]
    [SerializeField] private AudioSource _soundEffectsSourse;
    [Tooltip("Звук сбора монеты")]
    [SerializeField] private AudioClip _coinPicSound;
    [Tooltip("Звук неудачного окончания игры")]
    [SerializeField] private AudioClip _fallingSound;
    [Tooltip("Звук удачного окончания игры")]
    [SerializeField] private AudioClip _winSound;

    private void Start()
    {
        _soundVolumeController.value = _soundEffectsSourse.volume;
    }

    public void OnWin()
    {
        _soundEffectsSourse.PlayOneShot(_winSound, 1f);
    }

    public void OnLose()
    {
        _soundEffectsSourse.PlayOneShot(_fallingSound, 1f);
    }

    public void ShowSoundVolumeController()
    {
        _soundVolumeController.interactable = !_soundEffectsSourse.mute;
    }
}
