using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoseResult : MonoBehaviour
{
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject _loseTitle;
    public List<CanvasGroup> _loseCanvasGroup = new List<CanvasGroup>();
    public List<GameObject> _loseSkulls = new List<GameObject>();
    public List<GameObject> _loseIndicators = new List<GameObject>();

    private float fadeTime = 1f;

    public void StartLoseAnimation()
    {
        HideLoseGroup();

        RectTransform loseTitle = _loseTitle.GetComponent<RectTransform>();
        loseTitle.localPosition = new Vector3(0f, 110f, 0f);
        Sequence mySequence = DOTween.Sequence().SetUpdate(true);
        mySequence.SetDelay(0.03f);
        mySequence.SetEase(Ease.Linear);
        mySequence.Append(_loseTitle.transform.DOScale(1.5f, 0.03f).SetEase(Ease.InOutElastic));
        mySequence.Append(_loseTitle.transform.DOScale(1f, 0.03f).SetEase(Ease.InOutElastic));
        mySequence.Append(loseTitle.DOAnchorPos(new Vector2(0f, 305f), 0.3f, false).SetEase(Ease.InOutQuint));
        mySequence.AppendCallback(ShowSkulls);

        DOVirtual.DelayedCall(0.3f, () =>
        {
            ShowLoseIndicators();
        });
    }

    private void HideLoseGroup()
    {
        for (int i = 1; i < _loseCanvasGroup.Count; i++)
        {
            var canvasGroup = _loseCanvasGroup[i];

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
            }
        }
    }

    private void ShowSkulls()
    {
        var canvasGroup = _loseCanvasGroup[1];
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1, 0.2f).SetUpdate(true);
            foreach (var iSkull in _loseSkulls)
            {
                Shake(iSkull);
            }
        }
    }

    private void ShowLoseIndicators()
    {
        var canvasGroup = _loseCanvasGroup[2];
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.DOFade(1, fadeTime).SetUpdate(true);
            StartCoroutine(nameof(LoseIndicatorsAnimation));
        }
    }

    IEnumerator LoseIndicatorsAnimation()
    {
        foreach (var item in _loseIndicators)
        {
            item.transform.localScale = Vector3.zero;
        }

        foreach (var item in _loseIndicators)
        {
            item.transform.DOScale(1f, fadeTime).SetEase(Ease.InOutElastic).SetUpdate(true);
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
    
    private void Shake(GameObject obj)
    {
        float duration = 0.5f;
        float strength = 0.5f;

        obj.transform.DOScale(Vector3.one, 0.1f).SetUpdate(true);
        obj.transform.DOShakeScale(duration, strength).SetUpdate(true);
    }
}
