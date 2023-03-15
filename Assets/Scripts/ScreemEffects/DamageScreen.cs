using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageScreen : MonoBehaviour
{
    [SerializeField] private Image _damageImage;

    public void StartDamage()
    {
        Debug.Log("damageImage");
        StartCoroutine(ShowEffect());
    }

    public IEnumerator ShowEffect()
    {
        Debug.Log("damageImage");
        _damageImage.enabled = true;
        for (float i = 1; i > 0; i-=Time.deltaTime)
        {
            _damageImage.color = new Color(1, 0, 0, i);
            yield return null;
        }
        _damageImage.enabled = false;
    }
}
