using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    [Header("Книжный контейнер")]
    public GameObject BooksContainer;
    [Header("Приз")]
    public GameObject ExtraMaterial;
    [Header("Финальное сообщение")]
    public GameObject finalText;
    [Header("Запись о количестве собранных книг")]
    public TextMeshProUGUI BookCounterText;


    //Список книг на сцене
    private readonly List<Book> _books = new();
    //Количество собранных книг
    private int _bookCount;

    void Start()
    {
        //Заполняем список префабами книг
        _books.AddRange(BooksContainer.GetComponentsInChildren<Book>());
        //Скрываем финальную надпись
        finalText.SetActive(false);
        //Скрываем приз
        ExtraMaterial.SetActive(false);
        //Прячем курсор
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Если все монеты собраны - показываем приз
        if (_books.Count == 0)
        {
            ExtraMaterial.SetActive(true);
        }

    }
    
    /// Подписка на события "Сбор книги", "Сбор приза"
    private void OnEnable()
    {
        Book.OnBookPic += BookPic;
        BonusController.OnBonusPic += BonusPic;
    }
    
    /// Отписка от событий "Сбор книги", "Сбор приза"
    private void OnDisable()
    {
        Book.OnBookPic -= BookPic;
        BonusController.OnBonusPic -= BonusPic;
    }
    
    /// Обработчик событий "Сбор книги"
    private void BookPic(Book book)
    {
        _bookCount++;
        BookCounterText.text = _bookCount.ToString();

        _books.Remove(book);

    }
    /// Обработчик событий "Сбор приза"
    private void BonusPic(BonusController bonus)
    {
        BookCounterText.text = ":)";
        finalText.SetActive(true);
    }

}
