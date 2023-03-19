using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum StartPoint
{
    Top,
    Bottom,
    Left,
    Right
}

public enum EndPoint
{
    Top,
    Bottom,
    Left,
    Right
}

public class Page : MonoBehaviour
{
    public PageName PageName;
    public StartPoint StartPoint;
    public EndPoint EndPoint;

    public float fadeTime = 1f;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;
    public List<GameObject> items = new List<GameObject>();
    
    public AudioClip popupSound;
    private AudioSource audioSource;

    public static event Action OnDescription; 
    public static event Action OffDescription;

    private void Start()
    {
        if (PageName != PageName.Home)
        {
            rectTransform.transform.localPosition = GetStartPosition();
        }
    }

    public void PanelFadeIn()
    {
        rectTransform.DOAnchorPos(new Vector2(0f, 0f), fadeTime, false).SetEase(Ease.InOutQuint).SetUpdate(true);
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1, fadeTime).SetUpdate(true);

            StartCoroutine(nameof(ItemAnimation));
        }

        if (PageName == PageName.Distraction)
        {
            OnDescription?.Invoke();
        }
        
    }

    public void PanelFaseOut()
    {
        //DOTween.timeScale = 1f;
        //rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        rectTransform.DOAnchorPos(GetEndPosition(), fadeTime, false).SetEase(Ease.InOutQuint).SetUpdate(true);

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.DOFade(0, fadeTime).SetUpdate(true);
        }
        
        if (PageName == PageName.Distraction)
        {
            OffDescription?.Invoke();
        }
    }

    IEnumerator ItemAnimation()
    {
        foreach (var item in items)
        {
            item.transform.localScale = Vector3.zero;
        }

        foreach (var item in items)
        {
            item.transform.DOScale(1f, fadeTime).SetUpdate(true).SetEase(Ease.Linear);
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    private Vector3 GetStartPosition()
    {
        Vector3 startPointValue = Vector3.zero;
        switch (StartPoint)
        {
            case StartPoint.Top:
                startPointValue = Vector3.up * 2500f;
                break;
            case StartPoint.Bottom:
                startPointValue = Vector3.down * 2500f;
                break;
            case StartPoint.Left:
                startPointValue = Vector3.left * 2500f;
                break;
            case StartPoint.Right:
                startPointValue = Vector3.right * 2500f;
                break;
            default:
                break;
        }
        return startPointValue;
    }

    private Vector2 GetEndPosition()
    {
        Vector2 endPointValue = Vector2.zero;
        switch (EndPoint)
        {
            case EndPoint.Top:
                endPointValue = Vector2.up * 2500f;
                break;
            case EndPoint.Bottom:
                endPointValue = Vector2.down * 2500f;
                break;
            case EndPoint.Left:
                endPointValue = Vector2.left * 2500f;
                break;
            case EndPoint.Right:
                endPointValue = Vector2.right * 2500f;
                break;
            default:
                break;
        }
        return endPointValue;
    }
}
