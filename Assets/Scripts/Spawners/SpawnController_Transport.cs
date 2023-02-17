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
    //������ ����������� �� �����
    [SerializeField] private List<Transport> _transporterList;
    void Start()
    {
        CreateSupplier();
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
    public void CreateSupplier()
    {
        if (_transporterList.Count <= 1)
        {
            //������������� ����� �� ��������� ���������� ���������
            _timer = _timeSpawn;
            //�������� ���������� �� �������
            Transport nextSellerPrefab = �hoosingSupplier();
            //������ ���������� �� �������
            Transport nextSeller = Instantiate(nextSellerPrefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
            _transporterList.Add(nextSeller);
        }
       
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
    private void OnEnable()
    {
        SpawnController_�onsumer.No�ustomers += CreateSupplier;
        Transport.DestroyTrader += RemoveTransportFromList;
    }

    private void OnDisable()
    {
        SpawnController_�onsumer.No�ustomers -= CreateSupplier;
        Transport.DestroyTrader -= RemoveTransportFromList;
    }

    private void RemoveTransportFromList(Transport transport)
    {
        _transporterList.Remove(transport);
        Destroy(transport.gameObject);
    }
    /// <summary>
    /// ������� ������ ����������� ����������� �� �����
    /// </summary>
    public void ClearTransportList()
    {
        if (_transporterList.Count > 0)
        {
            foreach (var itransport in _transporterList)
            {
                if (itransport != null)
                {
                    Destroy(itransport.gameObject);
                }
            }
            _transporterList.Clear();
        }    
    }
}
