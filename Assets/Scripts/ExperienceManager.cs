using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    [Tooltip("Текущий уровень опыта")]
    [SerializeField] private float _experience = 0f;
    [Tooltip("Количество опыта до следующего уровня")]
    [SerializeField] private float _nextLevelExperience = 5f;
    [Tooltip("Надпись - значение уровня")]
    [SerializeField] private TextMeshProUGUI _levelText;
    [Tooltip("Изображение - шкала заполнения уровня")]
    [SerializeField] private Image _experienceScale;
    [Tooltip("Менеджер эффектов")]
    [SerializeField] private EffectsManager _effectsManager;
    [Tooltip("Кривая необходимого опыта")]
    [SerializeField] private AnimationCurve _experienceCurve;
    [Tooltip("Эффект - получение нового уровня")]
    [SerializeField] private ParticleSystem _upEffect;

    private int _level; // Текущий уровень

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
        _levelText.text = _level.ToString();
        _experience = 0;
        _nextLevelExperience = _experienceCurve.Evaluate(_level);

        ShowEffectToLevelUp();

        Invoke(nameof(ShowCards), 2f);
    }

    public void DisplayExperience()
    {
        _experienceScale.fillAmount = _experience / _nextLevelExperience;
    }

    private void ShowEffectToLevelUp()
    {
        _upEffect.Play();
    }

    private void ShowCards()
    {
        _effectsManager.ShowCards();
    }
}
