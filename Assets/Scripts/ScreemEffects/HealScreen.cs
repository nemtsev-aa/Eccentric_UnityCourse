using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealScreen : MonoBehaviour
{
    [SerializeField] private Image _healImage;

    public void StartHeal()
    {
        StartCoroutine(ShowEffect());
    }

    public IEnumerator ShowEffect()
    {
        Debug.Log("healImage");
        _healImage.enabled = true;
        for (float i = 0.5f; i > 0; i-=Time.deltaTime)
        {
            _healImage.color = new Color(0, 1, 0, i);
            yield return null;
        }
        _healImage.enabled = false;
    }
}
