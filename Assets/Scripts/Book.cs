using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Book : MonoBehaviour
{
    [Header("Скорость вращения")]
    public float RotationSpeed;
    [Header("Текст надписи - имя книги")]
    public string LebelText = "Неделя ";
    [Header("Компанент - надпись")]
    private TextMeshProUGUI _tmpCompanent;

    public static event System.Action<Book> OnBookPic;

    private void Start()
    {
        Naming();
    }
    // Update is called once per frame
    void Update()
    {
        Rotation();
    }

    /// Именование книги
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

        _tmpCompanent = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        _tmpCompanent.text = LebelText;
    }
    
    /// Вращение книги
    private void Rotation()
    {
        transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
    }
    
    /// Столкновение с игроком
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMover>())
        {
            OnBookPic?.Invoke(this);
            Destroy(gameObject);
        } 
    }
}
