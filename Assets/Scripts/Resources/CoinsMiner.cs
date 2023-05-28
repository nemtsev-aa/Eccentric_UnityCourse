using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsMiner : MonoBehaviour
{
    
    [SerializeField] private float CoinPerSecond = 2f;
    [SerializeField] private TextMeshProUGUI _AddCoinsValueText;
    [SerializeField] private Animator _animator;
    
    private Resources _resources;
    private float _timer;

    private void Start() {
        _resources = Resources.Instance;
        _AddCoinsValueText.text = "+" + CoinPerSecond;
    }
   
    private void Update() {
        _timer += Time.deltaTime;
        if (_timer >= 1f) {
            _timer = 0;
            _resources.Money += (int)CoinPerSecond;
            _resources.ShowRemainder();
            _animator.SetTrigger("AddCoins");
        } else {
            _animator.SetTrigger("Idle");
        }
    }
}
