using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreElementBall : ScoreElement
{
    [Tooltip("Метка для отображения уровня")]
    [SerializeField] private TextMeshProUGUI _levelText;
    [Tooltip("Изображение")]
    [SerializeField] private RawImage _ballImage;
    [Tooltip("Настройки мячей")]
    [SerializeField] private BallSettings _ballSettings;
   
    public override void Setup(Task task)
    {
        base.Setup(task);

        int number = (int)Mathf.Pow(2, task.Level + 1);     // Определяем значение надписи на основании уровня
        _levelText.text = number.ToString();    // Отображаем надпись 
        _ballImage.color = _ballSettings.BallMaterials[task.Level].color;   //Изменяем цвет шара в соответствии с уровнем
        Level = task.Level;     // Устанавливаем текущий уровень
    }
}
  