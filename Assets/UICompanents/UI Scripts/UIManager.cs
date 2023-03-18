using UnityEngine;
using TMPro;
using System;
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
    [Tooltip("Менеджер игрового процесса")]
    [SerializeField] private GameProcessManager _gameProcessManager;
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

    private void OnEnable()
    {
        _gameProcessManager.OnHome += _gameProcessManager_OnHome;
        _gameProcessManager.OnSettings += _gameProcessManager_OnSettings;
        _gameProcessManager.OnLevels += _gameProcessManager_OnLevels;   
        _gameProcessManager.OnGame += _gameProcessManager_OnGame;
        _gameProcessManager.OnDescription += _gameProcessManager_OnDescription;
        _gameProcessManager.OnPause += _gameProcessManager_OnPause;
        _gameProcessManager.OnWin += _gameProcessManager_OnWin;
        _gameProcessManager.OnLose += _gameProcessManager_OnLose;
        _gameProcessManager.OnAuthors += _gameProcessManager_OnAuthors;
        _gameProcessManager.OnShop += _gameProcessManager_OnShop;
    }

    private void OnDisable()
    {
        _gameProcessManager.OnHome -= _gameProcessManager_OnHome;
        _gameProcessManager.OnSettings -= _gameProcessManager_OnSettings;
        _gameProcessManager.OnLevels -= _gameProcessManager_OnLevels;
        _gameProcessManager.OnGame -= _gameProcessManager_OnGame;
        _gameProcessManager.OnPause -= _gameProcessManager_OnPause;
        _gameProcessManager.OnDescription -= _gameProcessManager_OnDescription;
        _gameProcessManager.OnWin -= _gameProcessManager_OnWin;
        _gameProcessManager.OnLose -= _gameProcessManager_OnLose;
        _gameProcessManager.OnAuthors -= _gameProcessManager_OnAuthors;
        _gameProcessManager.OnShop -= _gameProcessManager_OnShop;
    }

    private void _gameProcessManager_OnHome()
    {
        HidePage(_currentPageNumber);
        ShowPage(0);
    }
    private void _gameProcessManager_OnLevels()
    {
        HidePage(_currentPageNumber);
        ShowPage(1);
    }

    private void _gameProcessManager_OnSettings()
    {
        HidePage(_currentPageNumber);
        ShowPage(2);
    }

    private void _gameProcessManager_OnAuthors()
    {
        HidePage(_currentPageNumber);
        ShowPage(3);
    } 

    private void _gameProcessManager_OnShop()
    {
        HidePage(_currentPageNumber);
        ShowPage(4);
    }

    private void _gameProcessManager_OnGame()
    {
        HidePage(_currentPageNumber);
        ShowPage(5);
    }

    private void _gameProcessManager_OnDescription()
    {
        HidePage(_currentPageNumber);
        ShowPage(6);
    }

    private void _gameProcessManager_OnPause()
    {
        Debug.Log("OnPause");
        HidePage(_currentPageNumber);
        ShowPage(7);
    }

    private void _gameProcessManager_OnWin()
    {
        HidePage(_currentPageNumber);
        ShowPage(8);
    }

    private void _gameProcessManager_OnLose()
    {
        Debug.Log("OnLose");
        HidePage(_currentPageNumber);
        ShowPage(9);
    }

    public void HidePage(int pageNumber)
    {
        _closePageNumber = pageNumber;
        _pages[pageNumber].PanelFaseOut();
    }

    public void ShowPage(int PageNumber)
    {
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

    public void ComeBackPage()
    {
        int number = _closePageNumber;
        HidePage(_currentPageNumber);
        _closePageNumber =_currentPageNumber;

        _currentPageNumber = number;
        ShowPage(_currentPageNumber);

    }

    public void Descritption()
    {
        HidePage(_currentPageNumber);
        ShowPage(2);
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
