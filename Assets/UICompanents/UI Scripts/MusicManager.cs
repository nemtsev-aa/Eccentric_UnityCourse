using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{ 
    [SerializeField] private Scrollbar _musicVolumeController;
    
    [Header("Music")]
    [Tooltip("Источник музыки")]
    [SerializeField] private AudioSource _musicSource;
    [Tooltip("Mузыка в меню")]
    [SerializeField] private AudioClip _uiMusicClip;
    [Tooltip("Музыка в игре")]
    [SerializeField] private AudioClip _gameMusicClip;
   
    private void Start()
    {
        _musicVolumeController.value = _musicSource.volume;
    }

    public void PlayMusic(PageName pageName)
    {
        if (pageName == PageName.Game)
        {
            PlayGameMusic();
        }
        else
        {
            PlayUIMusic();
        }
    }

    public void OnWin()
    {
        _musicSource.Stop();
    }

    public void OnLose()
    {
        _musicSource.Stop();
    }

    private void PlayUIMusic()
    {
        _musicSource.clip = _uiMusicClip;
        if (!_musicSource.isPlaying)
        {
            _musicSource.Play();
        } 
    }

    private void PlayGameMusic()
    {
        _musicSource.clip = _gameMusicClip;
        if (!_musicSource.isPlaying)
        {
            _musicSource.Play();
        }
    }

    public void ShowMusicVolumeController()
    {
        _musicVolumeController.interactable = !_musicSource.mute;
    }
}
