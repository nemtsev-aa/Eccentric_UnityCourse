using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    [Header("������� ���������")]
    public GameObject BooksContainer;
    [Header("����")]
    public GameObject ExtraMaterial;
    [Header("��������� ���������")]
    public GameObject finalText;
    [Header("������ � ���������� ��������� ����")]
    public TextMeshProUGUI BookCounterText;


    //������ ���� �� �����
    private readonly List<Book> _books = new();
    //���������� ��������� ����
    private int _bookCount;

    void Start()
    {
        //��������� ������ ��������� ����
        _books.AddRange(BooksContainer.GetComponentsInChildren<Book>());
        //�������� ��������� �������
        finalText.SetActive(false);
        //�������� ����
        ExtraMaterial.SetActive(false);
        //������ ������
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //���� ��� ������ ������� - ���������� ����
        if (_books.Count == 0)
        {
            ExtraMaterial.SetActive(true);
        }

    }
    
    /// �������� �� ������� "���� �����", "���� �����"
    private void OnEnable()
    {
        Book.OnBookPic += BookPic;
        BonusController.OnBonusPic += BonusPic;
    }
    
    /// ������� �� ������� "���� �����", "���� �����"
    private void OnDisable()
    {
        Book.OnBookPic -= BookPic;
        BonusController.OnBonusPic -= BonusPic;
    }
    
    /// ���������� ������� "���� �����"
    private void BookPic(Book book)
    {
        _bookCount++;
        BookCounterText.text = _bookCount.ToString();

        _books.Remove(book);

    }
    /// ���������� ������� "���� �����"
    private void BonusPic(BonusController bonus)
    {
        BookCounterText.text = ":)";
        finalText.SetActive(true);
    }

}
