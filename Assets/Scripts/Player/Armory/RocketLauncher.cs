using UnityEngine;
using UnityEngine.UI;

public class RocketLauncher : Automat
{
    public Image Foreground;
    private float _time;

    private void Update()
    {
        _time += Time.deltaTime;

        SetChargeValue(_time, ShotPeriod);
        if (_time > this.ShotPeriod)
        {
            StopCharge();
            if (Input.GetMouseButton(0))
            {
                Shot();
                _time = 0;
                StartCharge();
            }
        }
    }

    public void SetChargeValue(float currentCharge, float maxCharge)
    {
        Foreground.fillAmount = 1 - currentCharge / maxCharge;
    }

    private void StartCharge()
    {
        Foreground.gameObject.SetActive(true);
    }

    private void StopCharge()
    {
        Foreground.gameObject.SetActive(false);
    }
}
