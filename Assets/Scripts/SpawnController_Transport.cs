using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController_Transport : MonoBehaviour
{
    //����� ��������� ����������
    [SerializeField] private GameObject _spawnPoint;

    [Tooltip ("������� �����������")]
    [SerializeField] private GameObject[] _suppliers;

    [Tooltip("������ ��������� ����������")]
    [SerializeField] private int _supplierIndex;
    
    [Tooltip("�������� ����� ���������� ������ ����������")]
    [SerializeField] private float _timeSpawn;

    //����� �� ��������� ���������� ����������
    [SerializeField] private float _timer;

    void Start()
    {
        _timer = _timeSpawn;
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            CreateSupplier();
        }
    }
    /// <summary>
    /// ������ ������ ����������
    /// </summary>
    private void CreateSupplier()
    {
        //������������� ����� �� ��������� ���������� ���������
        _timer = _timeSpawn;
        //�������� ���������� �� �������
        GameObject nextSellerPrefab = �hoosingSupplier();
        //������ ���������� �� �������
        GameObject nextSeller = Instantiate(nextSellerPrefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
    }
    /// <summary>
    /// ����� ������� ���������� ����������
    /// </summary>
    /// <param name="deliveryIndex"></param>
    private GameObject �hoosingSupplier()
    {
        //(��� ����������) �� �������� ��� ���������� ������ ���������� �������� ������� ����� %
        
        //������ ���������� ���������� 
        _supplierIndex++;
        if (_supplierIndex >= _suppliers.Length)
        {
            _supplierIndex = 0;
        }
        //������ ���������� ����������
        GameObject nextSeller = _suppliers[_supplierIndex];
        return nextSeller;
    }
}
