using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Book : MonoBehaviour
{
    [Header("�������� �����������")]
    public float _rotationSpeed;
    [Header("����� �������")]
    public string LebelText = "������ ";
    [Header("��������� - �������")]
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
    /// ���������� �����
    /// </summary>
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

        tmpCompanent = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        tmpCompanent.text = LebelText;
    }
    /// <summary>
    /// �������� �����
    /// </summary>
    private void Rotation()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
    }
    /// <summary>
    /// ������������ � �������
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
