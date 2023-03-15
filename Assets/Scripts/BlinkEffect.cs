using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    [SerializeField] private Renderer[] _renderers;

    public void StartBlink()
    {
        StartCoroutine(ShowEffect());
    }

    private IEnumerator ShowEffect()
    {
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material.SetColor("_EmissionColor", new Color(Mathf.Sin(30 * t) * 0.5f + 0.5f, 0, 0, 0));
            }
            Debug.Log("EmissionColor");
            yield return null;
        } 
    }
}
