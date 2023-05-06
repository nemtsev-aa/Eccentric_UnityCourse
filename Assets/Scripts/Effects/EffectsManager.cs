using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [Tooltip("Список применённых постоянных эффектов")]
    [SerializeField] private List<ContinuousEffect> _continuousEffectsApplied = new List<ContinuousEffect>();
    [Tooltip("Список применённых временных эффектов")]
    [SerializeField] private List<OneTimeEffect> _oneTimeEffectsApplied = new List<OneTimeEffect>();

    [Tooltip("Список неприменённых постоянных эффектов")]
    [SerializeField] private List<ContinuousEffect> _continuousEffects = new List<ContinuousEffect>();
    [Tooltip("Список неприменённых постоянных эффектов")]
    [SerializeField] private List<OneTimeEffect> _oneTimeEffects = new List<OneTimeEffect>();

    [SerializeField] private CardManager _cardManager;
    private void Awake()
    {
        for (int i = 0; i < _continuousEffects.Count; i++) // Заменяем содержимое списка копиями, чтобы не изменять оригиналы
        {
            _continuousEffects[i] = Instantiate(_continuousEffects[i]);
        }

        for (int i = 0; i < _oneTimeEffects.Count; i++) // Заменяем содержимое списка копиями, чтобы не изменять оригиналы
        {
            _oneTimeEffects[i] = Instantiate(_oneTimeEffects[i]);
        }
    }

    [ContextMenu("ShowCards")]
    public void ShowCards()
    {
        List<Effect> effectsToShow = new List<Effect>();    //Список эффектов из которых будут выбраны 3 случайных

        for (int i = 0; i < _continuousEffectsApplied.Count; i++) // Проверяем список применённых постоянных эффектов
        {
            if (_continuousEffectsApplied[i].Level < 10)        // Если уровень эффекта менее 10-го - добавляем его в список
                effectsToShow.Add(_continuousEffectsApplied[i]);
        }

        for (int i = 0; i < _oneTimeEffectsApplied.Count; i++) // Проверяем список применённых временных эффектов
        {
            if (_oneTimeEffectsApplied[i].Level < 10)        // Если уровень эффекта менее 10-го - добавляем его в список
                effectsToShow.Add(_oneTimeEffectsApplied[i]);
        }

        if (_continuousEffectsApplied.Count < 4)        // Если количество применённых постоянных эффектов меньше 4 - добавляем все оставшиеся поятоянные эффекты в список
            effectsToShow.AddRange(_continuousEffects);

        if (_oneTimeEffectsApplied.Count < 4)        // Если количество применённых временных эффектов меньше 4 - добавляем все оставшиеся временные эффекты в список
            effectsToShow.AddRange(_oneTimeEffects);

        int numberOfCardsToShow = Mathf.Min(effectsToShow.Count, 3); // Количество карт, которые будут показаны
        int[] randomIndexes = RandomSort(effectsToShow.Count, numberOfCardsToShow); // Список случайных индексов 

        List<Effect> effectsForCards = new List<Effect>(); //Список эффектов со случайными индексами для показа с помощью карточек
        for (int i = 0; i < randomIndexes.Length ; i++)
        {
            int index = randomIndexes[i];
            effectsForCards.Add(effectsToShow[index]);
        }

        _cardManager.ShowCards(effectsForCards);
    }

    private int[] RandomSort(int length, int number)
    {
        int[] array = new int[length];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i;
        }

        for (int i = 0; i < array.Length; i++)
        {
            int oldValue = array[i];
            int newIndex = Random.Range(0, array.Length);
            array[i] = array[newIndex];
            array[newIndex] = oldValue;
        }

        int[] result = new int[number];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = array[i];
        }

        return result;
    }

    public void AddEffect(Effect effect)
    {
        if (effect is ContinuousEffect c_effect) //Выбанный эффект - постоянный
        {
            if (!_continuousEffectsApplied.Contains(c_effect)) // Выбранный эффект не находится в списке применённых постоянных
            {
                _continuousEffectsApplied.Add(c_effect); // Добавляем эффект в список применённых постоянных эффектов
                _continuousEffects.Remove(c_effect); // Удаляем эффект из общего списка поятоянных эффектов
            }
        }
        if (effect is OneTimeEffect o_effect) //Выбанный эффект - временный
        {
            if (!_oneTimeEffectsApplied.Contains(o_effect)) // Выбранный эффект не находится в списке применённых временных
            {
                _oneTimeEffectsApplied.Add(o_effect); // Добавляем эффект в список временных применённых эффектов
                _oneTimeEffects.Remove(o_effect); // Удаляем эффект из общего списка временных эффектов
            }
        }

        effect.Activate(); // Активируем эффект
    }
}
