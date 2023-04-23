using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _winObject;
    [SerializeField] private GameObject _loseObject;
    [SerializeField] private UnityEvent _onWin;
    public UnityEvent OnWin => _onWin;
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Win()
    {
        _winObject.SetActive(true);
        _onWin?.Invoke();

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