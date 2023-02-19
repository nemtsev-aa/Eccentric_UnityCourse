using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Tooltip("Источник звука")]
    [SerializeField] private AudioSource _audioSource;
    [Tooltip("Звук сбора монеты")]
    [SerializeField] private AudioClip _coinPicSound;
    [Tooltip("Звук неудачного окончания игры")]
    [SerializeField] private AudioClip _fallingSound;
    [Tooltip("Звук удачного окончания игры")]
    [SerializeField] private AudioClip _winSound;

    /// Подписка на события 
    private void OnEnable()
    {
        PlayerMove.OnCoinCollecting += СollectingСoins;
        CoinCounter.OnWin += Wins;
    }

    /// Отписка от события 
    private void OnDisable()
    {
        
    }

    ///Обработчик события "Взаимодействие игрока с поставщиком"
    private void СollectingСoins()
    {
        _audioSource.PlayOneShot(_coinPicSound, 1f);
    }
    
    ///Обработчик события "Поражение"
    private void Falling()
    {
        _audioSource.PlayOneShot(_fallingSound, 1f);
    }
   
    ///Обработчик события "Победа"
    private void Wins()
    {
        _audioSource.PlayOneShot(_winSound, 1f);
    }

}
