using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCreator : MonoBehaviour
{
    [Tooltip ("���������� ����������� �����")]
    [SerializeField] private int _coinCount;
    [Tooltip("�������� �������� �����")]
    [SerializeField] private float _resetCreateTime;
    [Tooltip("���� �������� �����")]
    [SerializeField] private Transform _coinCreateZone;
    [Tooltip("������ ������")]
    [SerializeField] private Coin _coinPrefab;
    [Tooltip("������ ��������� �����")]
    [SerializeField] private List<Coin> _coinList;

    Transform nearCoinTransform;
    //����� �� ��������� ��������� ������
    private float _time;

    void Start()
    {
        nearCoinTransform = new GameObject().transform;
        _coinList = new List<Coin>();
        /// ������ ��������� ���������� ����� ��� ��������
        if (_resetCreateTime == 0)
        {
            for (int i = 0; i < _coinCount; i++)
            {
                CreateNewCoin();
            }
        }
    }

    void Update()
    {
        ///������ ��������� ���������� ����� � ����������
        if (_resetCreateTime > 0)
        {
            _time += Time.deltaTime;
            if (_time >= _resetCreateTime)
            {
                _time = 0;
                CreateNewCoin();
            }
        }
    }
    /// <summary>
    /// �������� ����� ������
    /// </summary>
    private void CreateNewCoin()
    {
        if (_coinList.Count < _coinCount)
        {
             Coin newCoin = Instantiate(_coinPrefab, GetPointInCreateZone(), Quaternion.identity);
            newCoin.name = $"Coin_{_coinList.Count}";
            newCoin.SetTransformY(transform.position.y);
            newCoin.SetLifeTime(_resetCreateTime * 5f);
            _coinList.Add(newCoin);
        } 
    }
    /// <summary>
    /// ��������� ��������� ����� ��� �������� ������
    /// </summary>
    /// <returns></returns>
    private Vector3 GetPointInCreateZone()
    {
        float x = Random.Range(-0.5f, 0.5f);
        float y = 0f;
        float z = Random.Range(-0.5f, 0.5f);

        return _coinCreateZone.TransformPoint(x,y,z);
       
    }
    ///// <summary>
    ///// ������������ ���� �������� �����
    ///// </summary>
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawCube(_coinCreateZone.position, _coinCreateZone.localScale);
    //}

    /// <summary>
    /// ��������� ��������� �����
    /// </summary>
    /// <param name="playerTransform"></param>
    /// <returns></returns>
    public Transform GetNearCoin(Transform playerTransform)
    {
        if (_coinList.Count == 0)
        {
            CreateNewCoin();
            nearCoinTransform = _coinList[0].transform;
            return nearCoinTransform;
        }
        else
        {
            float distanceToPlayer = Mathf.Infinity;
            Vector3 playerPosition = playerTransform.position;
            foreach (Coin iCoin in _coinList)
            {
                Vector3 iCoinToPlayer = iCoin.transform.position - playerPosition;
                float iDistance = iCoinToPlayer.sqrMagnitude;
                if (iDistance < distanceToPlayer)
                {
                    nearCoinTransform = iCoin.transform;
                    distanceToPlayer = iDistance;
                }
            }
            return nearCoinTransform;
        }
    }

    private void OnEnable()
    {
        PlayerMove.OnCoinPic += RemoveCoinForList;
        Coin.OutLifeTime += RemoveCoinForList;
    }

    private void OnDisable()
    {
        PlayerMove.OnCoinPic -= RemoveCoinForList;
        Coin.OutLifeTime -= RemoveCoinForList;
    }
    /// <summary>
    /// �������� ��������� ������ �� ������ ����� � �����
    /// </summary>
    /// <param name="coin"></param>
    public void RemoveCoinForList(Coin coin)
    {
        if (coin != null)
        {
            _coinList.Remove(coin);
            Destroy(coin.gameObject);
        }
    }
    /// <summary>
    /// ������� ������ �����
    /// </summary>
    /// <param name="coin"></param>
    public void ClearCoinList()
    {
        if (_coinList.Count > 0)
        {
            foreach (Coin iCoin in _coinList)
            {
                RemoveCoinForList(iCoin);
            }
        }
        
    }

}
