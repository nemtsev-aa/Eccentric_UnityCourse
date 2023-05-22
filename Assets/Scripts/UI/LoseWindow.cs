using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseWindow : MonoBehaviour
{
    [Tooltip("������ ��� �����������")]
    [SerializeField] private Button _continueButton;

    private void Awake()
    {
        _continueButton.onClick.AddListener(Continue);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
