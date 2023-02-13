using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController_Transport : MonoBehaviour
{
    //����� ��������� ����������
    [SerializeField] private GameObject _spawnPoint;

    [Tooltip ("������� �����������")]
    [SerializeField] private Transport[] _suppliers;

    [Tooltip("������ ��������� ����������")]
    [SerializeField] private int _supplierIndex;
    
    [Tooltip("�������� ����� ���������� ������ ����������")]
    [SerializeField] private float _timeSpawn;

    //����� �� ��������� ���������� ����������
    private float _timer;

    //������ ���������� �����������
    private bool _spawnActive = true;
    void Start()
    {
        _timer = _timeSpawn;
    }

    void Update()
    {
        //���� ���� �� ���������
        if (_spawnActive)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                CreateSupplier();
            }
        }
        
    }
    /// �������� �� ������e "����� ����"
    private void OnEnable()
    {
        GameManager.OnGameOver += GameOver;
    }

    /// ������� �� ������� "����� ����"
    private void OnDisable()
    {
        GameManager.OnGameOver -= GameOver;
    }

    /// <summary>
    /// ������ ������ ����������
    /// </summary>
    private void CreateSupplier()
    {
        //������������� ����� �� ��������� ���������� ���������
        _timer = _timeSpawn;
        //�������� ���������� �� �������
        Transport nextSellerPrefab = �hoosingSupplier();
        //������ ���������� �� �������
        GameObject nextSeller = Instantiate(nextSellerPrefab.gameObject, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
    }
    /// <summary>
    /// ����� ������� ���������� ����������
    /// </summary>
    /// <param name="deliveryIndex"></param>
    private Transport �hoosingSupplier()
    {
        //(��� ����������) �� �������� ��� ���������� ������ ���������� �������� ������� ����� %
        
        //������ ���������� ���������� 
        _supplierIndex++;
        if (_supplierIndex >= _suppliers.Length)
        {
            _supplierIndex = 0;
        }
        //������ ���������� ����������
        Transport nextSeller = _suppliers[_supplierIndex];
        return nextSeller;
    }
    private void GameOver()
    {
        _spawnActive = false;
    }
}
