using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBank : MonoBehaviour
{
    [SerializeField] GameObject _changeParticle;
    [SerializeField] private float _resetTime;

    [SerializeField] private float _time;
    //—татус зар€дного устройства
    [SerializeField] private bool _status;
   
    private void Start()
    {
        _status = true;
        _time = _resetTime;
        _changeParticle.SetActive(true);
    }

    /// <summary>
    /// ѕолучение статуса зар€дного устройства
    /// </summary>
    /// <param name="dron"></param>
    public bool GetStatus()
    {
        return _status;
    }
    /// <summary>
    /// ѕолучение статуса зар€дного устройства
    /// </summary>
    /// <param name="dron"></param>
    public void SetStatus(bool value)
    {
        _status = value;
        _time = _resetTime;
    }
    private void Update()
    {
        if (!_status)
        {
            ResetPowerBank();
        }
    }
    private void ResetPowerBank()
    {
        _changeParticle.SetActive(false);
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            _status = true;
            _changeParticle.SetActive(true);
        }
    }
}
