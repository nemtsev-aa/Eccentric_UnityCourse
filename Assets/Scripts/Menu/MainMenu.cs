using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Tooltip("Метка - количество монет")]
    [SerializeField] private TextMeshProUGUI _coinsText;
    [Tooltip("Метка - текущий уровень")]
    [SerializeField] private TextMeshProUGUI _levelText;
    [Tooltip("Кнопка - старт")]
    [SerializeField] private Button _startButton;

    private void Start()
    {
        _coinsText.text = Progress.Instance.Coins.ToString();
        _levelText.text = "Level " + Progress.Instance.Level.ToString();
        _startButton.onClick.AddListener(StartLevel); 
    }

    private void StartLevel()
    {
        SceneManager.LoadScene(Progress.Instance.Level); 
    }
}
