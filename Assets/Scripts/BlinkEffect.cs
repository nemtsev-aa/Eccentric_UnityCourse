using System.Collections;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    [Tooltip("Средство визуализации частей объекта")]
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
                for (int m = 0; m < _renderers[i].materials.Length; m++)
                {
                    _renderers[i].materials[m].SetColor("_EmissionColor", new Color(Mathf.Sin(30 * t) * 0.5f + 0.5f, 0, 0, 0));
                }
            }
            yield return null;
        } 
    }
}
