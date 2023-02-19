using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinExit : MonoBehaviour
{
    [Tooltip("�����")]
    [SerializeField] private GameObject _ramp;
    [Tooltip("��������")]
    [SerializeField] private GameObject _ladder;

    /// <summary>
    /// ������ ������
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
