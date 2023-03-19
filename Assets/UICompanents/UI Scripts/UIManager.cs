using UnityEngine;
using TMPro;
using System.Collections.Generic;

public enum PageName
{
    Home,
    Levels,
    Settings,
    Authors,
    Shop,
    Game,
    Distraction,
    Pause,
    Win,
    Lose
}

public class UIManager : MonoBehaviour
{
    [Tooltip("Камера UI")]
    [SerializeField] private Camera _uiCamera;
    [Tooltip("Номер активного окна UI")]
    [SerializeField] int _currentPageNumber = 0;
    [Tooltip("Список окон UI")]
    [SerializeField] private List<Page> _pages = new List<Page>();
    [Tooltip("Метки для отображения времени")]
    [SerializeField] private List<TextMeshProUGUI> _gameTimeTexts;
    [Tooltip("Метки для отображения количества попаданий")]
    [SerializeField] private List<TextMeshProUGUI> _hitCountTexts;

    private int _closePageNumber;

    void Start()
    {
        ShowPage(_currentPageNumber);
    }

    public void ShowWindow(PageName pageName)
    {
        if (pageName != PageName.Game)
        {
            _uiCamera.gameObject.SetActive(true);
            _uiCamera.enabled = true;
            
        }
        else
        {
            _uiCamera.gameObject.SetActive(false);
            _uiCamera.enabled = false;
        }
        
        for (int i = 0; i < _pages.Count; i++)
        {
            Page iPage = _pages[i];
            if (iPage.name == pageName.ToString())
            {
                ShowPage(i);
            }
        }
    }

    public void ShowPage(int PageNumber)
    {
        
        HidePage(_currentPageNumber);

        _currentPageNumber = PageNumber;
        _pages[PageNumber].PanelFadeIn();

        if (PageNumber == 8)
        {
            _pages[PageNumber].gameObject.GetComponent<WinResult>().StartWinAnimation();
        }
        else if (PageNumber == 9)
        {
            _pages[PageNumber].gameObject.GetComponent<LoseResult>().StartLoseAnimation();
        }
    }

    public void HidePage(int pageNumber)
    {
        _closePageNumber = pageNumber;
        _pages[pageNumber].PanelFaseOut();
    }

    public void ComeBackPage()
    {
        int number = _closePageNumber;
        HidePage(_currentPageNumber);
        _closePageNumber =_currentPageNumber;

        _currentPageNumber = number;
        ShowPage(_currentPageNumber);
    }

    public void ShowGameTime(float timeValue, float gameTime)
    {
        string _textValue = (Mathf.RoundToInt(timeValue)).ToString();
        
        foreach (var iGameTimeText in _gameTimeTexts)
        {
            iGameTimeText.text = _textValue;
        } 
    }

    public void ShowHitCount(int hitCountValue)
    {
        string _textValue = Mathf.RoundToInt(hitCountValue).ToString();
        foreach (var iHitCountText in _hitCountTexts)
        {
            iHitCountText.text = _textValue;
        }
    }
}
