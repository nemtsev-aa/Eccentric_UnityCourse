using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Tooltip("���")]
    [SerializeField] private Image _iconBackground;
    [Tooltip("�����������")]
    [SerializeField] private Image _iconImage;
    [Tooltip("�������� �������")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [Tooltip("��������")]
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [Tooltip("�������")]
    [SerializeField] private TextMeshProUGUI _levelText;
    [Tooltip("������")]
    [SerializeField] private Button _button;
    [Tooltip("������ ����������� �������")]
    [SerializeField] private Sprite _continuousEffectSprite;
    [Tooltip("������ ���������� �������")]
    [SerializeField] private Sprite _oneTimeEffectSprite;

    private Effect _effect;
    private EffectsManager _effectsManager;
    private CardsManager _cardManager;

    public void Init(EffectsManager effectsManager, CardsManager cardManager)
    {
        _effectsManager = effectsManager;
        _cardManager = cardManager;
        _button.onClick.AddListener(Click);
    }
   
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

    public void Click()
    {
        _effectsManager.AddEffect(_effect);
        _cardManager.Hide();
    }
}
