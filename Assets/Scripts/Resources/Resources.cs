using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public static Resources Instance;
    
    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public int Money;
    [Header("View")]
    [SerializeField] private TextMeshProUGUI _moneyCountText;
    [Header("SoundsEffect")]
    [SerializeField] private AudioClip _noGold;
    private AudioSource _audioSource;


    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        ShowRemainder();
    }

    public void NoGoldSoundEffect() {
        _audioSource.clip = _noGold;
        _audioSource.Play();
    }

    public void ShowRemainder() {
        _moneyCountText.text = Money.ToString();
    }

}
