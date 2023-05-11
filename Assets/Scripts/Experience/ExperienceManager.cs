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
    [Tooltip("������ - ��������� ������ ������")]
    [SerializeField] private ParticleSystem _upEffect;
    [Tooltip("���� - ��������� �����")]
    [SerializeField] private AudioClip _experienceSound;
    [Tooltip("���� - ��������� ������")]
    [SerializeField] private AudioClip _levelUpSound;

    private int _level = -1; // ������� �������
    private AudioSource _audioSource; // �����

    private void Awake()
    {
        _nextLevelExperience = _experienceCurve.Evaluate(0);
        _audioSource = GetComponent<AudioSource>();
    }

    public void AddExperience(float value)
    {
        _experience += value;
        _audioSource.PlayOneShot(_experienceSound);
        if (_experience >= _nextLevelExperience)
        {
            UpLevel();
        }
        DisplayExperience();
    }

    public void UpLevel()
    {
        _level++;
        ShowEffectToLevelUp();
        _experience = 0;
        _nextLevelExperience = _experienceCurve.Evaluate(_level);

        Invoke(nameof(ShowCards), 2f);
    }

    public void DisplayExperience()
    {
        _experienceScale.fillAmount = _experience / _nextLevelExperience;
    }

    private void ShowEffectToLevelUp()
    {
        _upEffect.Play();
        _levelText.text = _level.ToString();
        _audioSource.PlayOneShot(_levelUpSound);
    }

    private void ShowCards()
    {
        _effectsManager.ShowCards();
    }
}
