using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Book : MonoBehaviour
{
    [Header("�������� ��������")]
    public float RotationSpeed;
    [Header("����� ������� - ��� �����")]
    public string LebelText = "������ ";
    [Header("��������� - �������")]
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

    /// ���������� �����
    private void Naming()
    {
        //���� ��� ����� �� ������� ����, ��� ������������ �� ����� �������
        if (LebelText == "������ ")
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

            LebelText = "������ " + myNumber;
        }

        _tmpCompanent = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        _tmpCompanent.text = LebelText;
    }
    
    /// �������� �����
    private void Rotation()
    {
        transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
    }
    
    /// ������������ � �������
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMover>())
        {
            OnBookPic?.Invoke(this);
            Destroy(gameObject);
        } 
    }
}
