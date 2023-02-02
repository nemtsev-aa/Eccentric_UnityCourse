using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Book : MonoBehaviour
{
    [Header("Скорость перемещения")]
    public float _rotationSpeed;
    [Header("Текст надписи")]
    public string LebelText = "Неделя ";
    [Header("Компанент - надпись")]
    private TextMeshProUGUI tmpCompanent;
    private void Start()
    {
        Naming();
    }
    // Update is called once per frame
    void Update()
    {
        Rotation();
    }
    /// <summary>
    /// Именование книги
    /// </summary>
    private void Naming()
    {
        //Если имя книги не указано явно, оно определяется по имени префаба
        if (LebelText == "Неделя ")
        {
            string myName = gameObject.name;
            List<char> chars;
            string myNumber;

            if (myName.Length > 5)
            {
                chars = new List<char> { myName[myName.Length - 2], myName[myName.Length - 1]};
                myNumber = string.Concat(chars);
            }
            else
            {
                chars = new List<char> { myName[myName.Length - 1] };
                myNumber = string.Concat(chars);
            }

            LebelText = "Неделя " + myNumber;
        }

        tmpCompanent = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        tmpCompanent.text = LebelText;
    }
    /// <summary>
    /// Вращение книги
    /// </summary>
    private void Rotation()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
    }
    /// <summary>
    /// Столкновение с игроком
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMove>())
        {
            Destroy(gameObject);
        } 
    }
}
