using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Tooltip("Окно - победа")]
    [SerializeField] private GameObject _winObject;
    [Tooltip("Окно - поражение")]
    [SerializeField] private GameObject _loseObject;
    [Tooltip("Событие - победа")]
    public UnityEvent OnWin;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Win()
    {
        _winObject.SetActive(true);
        OnWin?.Invoke();

        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        Progress.Instance.SetLevel(currentLevelIndex++);
        Progress.Instance.AddCoins(50);
    }

    public void Lose()
    {
        _loseObject.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(Progress.Instance.Level + 1);
    }
    
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}