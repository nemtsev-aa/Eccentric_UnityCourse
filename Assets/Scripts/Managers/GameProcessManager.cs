using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStatus
{
    Menu,
    Start,
    Active,
    Pause,
    Lose,
    Win
}

public class GameProcessManager : MonoBehaviour
{
    public static GameProcessManager Instance;

    public GameStatus CurrentGameStatus;
    [Header("Game Settings")]
    [Tooltip("Время уровня")]
    [SerializeField] private int _gameTime = 180;
    private float _currentGameTime;
   
    [Header("Managers")]
    [SerializeField] private Enemies _enemies;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private MusicManager _musicManager;
    
    [Header("Levels")]
    [SerializeField] private GameObject _allLevelObjects;
    [SerializeField] private int _selectionLevel;
    [SerializeField] private List<GameObject> _levelObjects;

    [Header("MonoBehaviours")]
    [Tooltip("Скрипты отключаемые во время паузы")]
    [SerializeField] private MonoBehaviour[] ComponentsToDisable;

    public event Action<PageName> OnWindowShow;
    public event Action OnGame;
    public event Action OnPause;
    public event Action OnResume;
    public event Action OnWin;
    public event Action OnLose;

    private LevelManager _levelManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);
  
        OnGame += _timeManager.StartTimer;
        OnPause += _timeManager.PauseTimer;
        OnResume += _timeManager.ResumeGame;
        OnWin += _timeManager.StopTimer;
        OnWin += _soundManager.OnWin;
        OnWin += _musicManager.OnWin;
        OnLose += _timeManager.StopTimer;
        OnLose += _soundManager.OnLose;
        OnLose += _musicManager.OnLose;
        OnWindowShow += _uiManager.ShowWindow;
        OnWindowShow += _musicManager.PlayMusic;
        
    }

    private void Start()
    {
        CurrentGameStatus = GameStatus.Start;
        OnWindowShow?.Invoke(PageName.Home);
        HideLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurrentGameStatus == GameStatus.Pause)
                Resume();
            else if (CurrentGameStatus == GameStatus.Active)
                PauseGame();
        }

        _enemies.ShowNearEnemy();
    }

    public void StartGame()
    {
        _timeManager.SetGameTime(_gameTime);
        RecordGameTime(_gameTime);

        _levelManager.ResetLevel();

        CurrentGameStatus = GameStatus.Active;
        OnWindowShow?.Invoke(PageName.Game);
        OnGame?.Invoke();

        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        _timeManager.OnGameTimeOut += GameTimeOut;
        _timeManager.TikGameTime += RecordGameTime;

    }
 
    private void OnDisable()
    {
        _timeManager.OnGameTimeOut -= GameTimeOut;
        _timeManager.TikGameTime -= RecordGameTime;
    }

    private void GameTimeOut()
    {
        GameLose();
    }

    public void GameLose()
    {
        Time.timeScale = 1;
        CurrentGameStatus = GameStatus.Lose;
        OnWindowShow?.Invoke(PageName.Lose);
        OnLose?.Invoke();
        HideLevel();

        SetStatusComponentsToDisable(false);
    }

    public void GameWin()
    {
        Time.timeScale = 0;
        CurrentGameStatus = GameStatus.Win;
        OnWindowShow?.Invoke(PageName.Win);
        OnWin?.Invoke();
        HideLevel();
        SetStatusComponentsToDisable(false);
    }
    
    public void PauseGame()
    {
        CurrentGameStatus = GameStatus.Pause;
        OnWindowShow?.Invoke(PageName.Pause);
        OnPause?.Invoke();
        Time.timeScale = 0; 

        SetStatusComponentsToDisable(false);
    }

    public void Return()
    {
        CurrentGameStatus = GameStatus.Start;
        OnWindowShow?.Invoke(PageName.Game);

        Time.timeScale = 1;
        _timeManager.StartTimer();
    }

    public void Resume()
    {
        if (CurrentGameStatus == GameStatus.Win)
        {
            CurrentGameStatus = GameStatus.Start;
            OnWindowShow?.Invoke(PageName.Game);
            OnResume?.Invoke();
            Time.timeScale = 1;
        }
        else
        {
            SetStatusComponentsToDisable(true);
            Return();
        }
    }

    public void ShowHome()
    {
        Time.timeScale = 1;
        CurrentGameStatus = GameStatus.Menu;
        OnWindowShow?.Invoke(PageName.Home);
        HideLevel();
    }

    public void ShowSettings()
    {
        CurrentGameStatus = GameStatus.Menu;
        OnWindowShow?.Invoke(PageName.Settings);
        Time.timeScale = 0;
    }
    public void ShowDescription()
    {
        CurrentGameStatus = GameStatus.Menu;
        OnWindowShow?.Invoke(PageName.Distraction);
        Time.timeScale = 0;
    }

    public void ShowAuthors()
    {
        CurrentGameStatus = GameStatus.Menu;
        OnWindowShow?.Invoke(PageName.Authors);
        Time.timeScale = 1;
    }
    public void ShowShop()
    {
        CurrentGameStatus = GameStatus.Menu;
        OnWindowShow?.Invoke(PageName.Shop);
        Time.timeScale = 0;
    }
    public void ShowLevels()
    {
        CurrentGameStatus = GameStatus.Menu;
        OnWindowShow?.Invoke(PageName.Levels);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SelectionLevel(int levelIndex)
    {
        _selectionLevel = levelIndex;
    }

    public void ShowLevel()
    {
        _allLevelObjects.SetActive(true);
        GameObject newLevel = _levelObjects[_selectionLevel - 1];
        newLevel.SetActive(true);
        _levelManager = newLevel.GetComponent<LevelManager>();
        _enemies.BossKilled += _levelManager.ShowExit;
        _enemies.AllEnemiesDestroyed += _levelManager.ShowExit;

        StartGame();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HideLevel()
    {
        foreach (var iLevel in _levelObjects)
        {
            iLevel.SetActive(false);
        }
        _allLevelObjects.SetActive(false);
    }

    public void RecordGameTime(float timeValue)
    {
        _currentGameTime = timeValue;
        _uiManager.ShowGameTime(_currentGameTime, _gameTime);
    }

    private void SetStatusComponentsToDisable(bool value)
    {
        for (int i = 0; i < ComponentsToDisable.Length; i++)
        {
            ComponentsToDisable[i].enabled = value;
        }
    }
}
