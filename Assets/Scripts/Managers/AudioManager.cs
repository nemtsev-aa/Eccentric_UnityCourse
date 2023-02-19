using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Tooltip("�������� �����")]
    [SerializeField] private AudioSource _audioSource;
    [Tooltip("���� ����� ������")]
    [SerializeField] private AudioClip _coinPicSound;
    [Tooltip("���� ���������� ��������� ����")]
    [SerializeField] private AudioClip _fallingSound;
    [Tooltip("���� �������� ��������� ����")]
    [SerializeField] private AudioClip _winSound;

    /// �������� �� ������� 
    private void OnEnable()
    {
        PlayerMove.OnCoinCollecting += �ollecting�oins;
        CoinCounter.OnWin += Wins;
    }

    /// ������� �� ������� 
    private void OnDisable()
    {
        
    }

    ///���������� ������� "�������������� ������ � �����������"
    private void �ollecting�oins()
    {
        _audioSource.PlayOneShot(_coinPicSound, 1f);
    }
    
    ///���������� ������� "���������"
    private void Falling()
    {
        _audioSource.PlayOneShot(_fallingSound, 1f);
    }
   
    ///���������� ������� "������"
    private void Wins()
    {
        _audioSource.PlayOneShot(_winSound, 1f);
    }

}
