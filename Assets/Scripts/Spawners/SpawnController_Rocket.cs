using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController_Rocket : MonoBehaviour
{
    [Tooltip("����")]
    [SerializeField] private Dron_Controller _target;

    [Tooltip("������� �����")]
    [SerializeField] private Rocket[] _rockets;

    //������ �������� ������
    private int _rocketIndex;

    //������ ����� �� �����
    [SerializeField] private List<Rocket> _rocketList;

    /// �������� �� ������e "��������� ��������"
    private void OnEnable()
    {
        Consumer.OnFailDelivery += CreateRocket;
        Rocket.OnDetonation += RemoveRocketFromList;
    }

    /// ������� �� ������� "��������� ��������"
    private void OnDisable()
    {
        Consumer.OnFailDelivery -= CreateRocket;
    }

    /// <summary>
    /// ������ ����� ������
    /// </summary>
    private void CreateRocket(Consumer consumer)
    {
        //�������� ������ �� �������
        Rocket nextRocketPrefab = �hoosingSupplier();
        //������ ������ �� �������
        Rocket nextRocket = Instantiate(nextRocketPrefab, consumer.transform.position, consumer.transform.rotation);
        //��������� ������ ����
        nextRocket.SetTarget(_target);
        //��������� ������ � ������
        _rocketList.Add(nextRocket); 
    }
    /// <summary>
    /// ����� ������� ��������� ������
    /// </summary>
    /// <param name="deliveryIndex"></param>
    private Rocket �hoosingSupplier()
    {
        //(��� ����������) �� �������� ��� ���������� ������ ���������� �������� ������� ����� %

        //������ ��������� ������
        _rocketIndex++;
        if (_rocketIndex >= _rockets.Length)
        {
            _rocketIndex = 0;
        }
        //������ ��������� ������
        Rocket nextRocket = _rockets[_rocketIndex];
        return nextRocket;
    }

    /// <summary>
    /// ������� ������ �� ������ �����
    /// </summary>
    /// <param name="rocket"></param>
    private void RemoveRocketFromList(Rocket rocket)
    {
        _rocketList.Remove(rocket);
        Destroy(rocket.gameObject);
    }

    /// <summary>
    /// ������� ������ ����� �� �����
    /// </summary>
    public void ClearRocketList()
    {
        if (_rocketList.Count > 0)
        {
            foreach (var iRocket in _rocketList)
            {
                if (iRocket != null)
                {
                    Destroy(iRocket.gameObject);
                }
            }
            _rocketList.Clear();
        }    
    }
}
