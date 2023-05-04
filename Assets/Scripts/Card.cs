using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Tooltip("Фон")]
    [SerializeField] private Image _iconBackground;
    [Tooltip("Изображение")]
    [SerializeField] private Image _iconImage;
    [Tooltip("Название эффекта")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [Tooltip("Описание")]
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [Tooltip("Уровень")]
    [SerializeField] private TextMeshProUGUI _levelText;
    [Tooltip("Кнопка")]
    [SerializeField] private Button _button;
    [Tooltip("Спрайт постоянного эффекта")]
    [SerializeField] private Sprite _continuousEffectSprite;
    [Tooltip("Спрайт временного эффекта")]
    [SerializeField] private Sprite _oneTimeEffectSprite;

    private Effect _effect;
    public void Show(Effect effect)
    {
        _effect = effect;
        _nameText.text = _effect.Name.ToString();
        _descriptionText.text = _effect.Description.ToString();
        _levelText.text = _effect.Level.ToString();
        _iconImage.sprite = _effect.Sprite;

        if (effect is ContinuousEffect)
            _iconBackground.sprite = _continuousEffectSprite;
        else
            _iconBackground.sprite = _oneTimeEffectSprite;
    }
}
