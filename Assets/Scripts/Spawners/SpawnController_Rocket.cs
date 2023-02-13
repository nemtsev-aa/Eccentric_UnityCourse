using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController_Rocket : MonoBehaviour
{
    [Tooltip("������� �����")]
    [SerializeField] private Rocket[] _rockets;

    [Tooltip("������ �������� ������")]
    [SerializeField] private int _rocketIndex;

    /// �������� �� ������e "��������� ��������"
    private void OnEnable()
    {
        Consumer.OnFailDelivery += CreateRocket;
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
        //������ ���������� �� �������
        Rocket nextRocket = Instantiate(nextRocketPrefab, consumer.transform.position, consumer.transform.rotation);
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

}
