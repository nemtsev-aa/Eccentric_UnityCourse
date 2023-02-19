using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinExit : MonoBehaviour
{
    [Tooltip("Рампа")]
    [SerializeField] private GameObject _ramp;
    [Tooltip("Лестница")]
    [SerializeField] private GameObject _ladder;

    /// <summary>
    /// Экстра победа
    /// </summary>
    public static event System.Action OnExtraWin;

    private void Start()
    {
        _ramp.SetActive(true);
        _ladder.SetActive(false);
    }
    public void OnWinExit()
    {
        _ramp.SetActive(false);
        _ladder.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMove>())
        {
            OnExtraWin?.Invoke();
        }
    }
}
