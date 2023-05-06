using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    [Tooltip("������� ������� �����")]
    [SerializeField] private float _experience = 0f;
    [Tooltip("���������� ����� �� ���������� ������")]
    [SerializeField] private float _nextLevelExperience = 5f;
    [Tooltip("������� - �������� ������")]
    [SerializeField] private TextMeshProUGUI _levelText;
    [Tooltip("����������� - ����� ���������� ������")]
    [SerializeField] private Image _experienceScale;
    [Tooltip("�������� ��������")]
    [SerializeField] private EffectsManager _effectsManager;
    [Tooltip("������ ������������ �����")]
    [SerializeField] private AnimationCurve _experienceCurve;

    private int _level; // ������� �������

    private void Awake()
    {
        _nextLevelExperience = _experienceCurve.Evaluate(0);
    }

    public void AddExperience(float value)
    {
        _experience += value;
        if (_experience >= _nextLevelExperience)
        {
            UpLevel();
        }
        DisplayExperience();
    }

    public void UpLevel()
    {
        _level++;
        _effectsManager.ShowCards();
        _levelText.text = _level.ToString();
        _experience = 0;
        _nextLevelExperience = _experienceCurve.Evaluate(_level);
    }

    public void DisplayExperience()
    {
        _experienceScale.fillAmount = _experience / _nextLevelExperience;
    }
}
