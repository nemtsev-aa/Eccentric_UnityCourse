using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Tooltip("��������� �������� ������")]
    [SerializeField] private Building _castle;
    [Tooltip("���� �������� - �����")]
    [SerializeField] private Transform _northCreateZone;
    [Tooltip("���� �������� - ��")]
    [SerializeField] private Transform _southCreateZone;
    [Tooltip("���� �������� - �����")]
    [SerializeField] private Transform _westCreateZone;
    [Tooltip("���� �������� - ������")]
    [SerializeField] private Transform _eastCreateZone;
    [Tooltip("������ �������� ������")]
    [SerializeField] private float _creationRadius;
    [Tooltip("������ ��� �������� ������")]
    [SerializeField] private ChapterSettings _chapterSettings;
    [Tooltip("������ ��������� ������")]
    [SerializeField] private List<Enemy> _enemyList = new List<Enemy>();

    private void Start() {
        StartNewWave(0);
    }

    public void StartNewWave(int wave)
    {
        StopAllCoroutines(); // ������������� ��� ���������� �������� ����� ������� �����
        for (int i = 0; i < _chapterSettings.EnemyWavesArray.Length; i++) // �������� �� ���� ��������� ������� ������
        {
            Enemy iEnemy = _chapterSettings.EnemyWavesArray[i].Enemy; // i-� ����
            float iEnemyNumber = _chapterSettings.EnemyWavesArray[i].NumberPerSecond[wave]; // ������������� �������� ������ � ������ �����
            if (iEnemyNumber > 0) // ���� �������� ������ �� �������������
                StartCoroutine(CreateEnemyInSeconds(iEnemy, iEnemyNumber));
        }
    }

    private IEnumerator CreateEnemyInSeconds(Enemy enemy, float enemyPerSecond)
    {
        while (true) // ����������� ����������
        {
            yield return new WaitForSeconds(1f / enemyPerSecond); // �������, ��������������� ������ ����� � �����  
            Create(enemy);
        }
    }

    public void CreateInCircle(Enemy enemy)
    {
        Vector2 randomPoint = Random.insideUnitCircle.normalized; // ��������� ����� �� ��������� ����������
        Vector3 position = new Vector3(randomPoint.x, 0f, randomPoint.y) * _creationRadius + _castle.gameObject.transform.position; // ��������� ����� ��� ��������
        Enemy newEnemy = Instantiate(enemy,position, Quaternion.identity); // ����� ���� � ����������� ���������
        newEnemy.Init(_castle); // ������� ����� �������� � ��������� �����
        newEnemy.EnemyKilled += RemoveEnemy;
        _enemyList.Add(newEnemy); // ��������� ����� � ������
    }

    public void Create(Enemy enemy) {
        
        Enemy newEnemy = new();
        int[] randomDirection = RandomSort(4, 1);
        Debug.Log(randomDirection[0]);
        switch (randomDirection[0]) {
            case 0:
                newEnemy = Instantiate(enemy, GetPointInNorthCreateZone(), Quaternion.identity); // ����� ���� � ����������� ���������
                break;
            case 1:
                newEnemy = Instantiate(enemy, GetPointInSouthCreateZone(), Quaternion.identity); // ����� ���� � ����������� ���������
                break;
            case 2:
                newEnemy = Instantiate(enemy, GetPointInWestCreateZone(), Quaternion.identity); // ����� ���� � ����������� ���������
                break;
            case 3:
                newEnemy = Instantiate(enemy, GetPointInEastCreateZone(), Quaternion.identity); // ����� ���� � ����������� ���������
                break;
            default:
                break;
        }

        newEnemy.transform.LookAt(_castle.gameObject.transform);
        newEnemy.Init(_castle); // ������� ����� �������� � ��������� ������
        newEnemy.EnemyKilled += RemoveEnemy;
        _enemyList.Add(newEnemy); // ��������� ����� � ������
    }

    private Vector3 GetPointInNorthCreateZone() {
        float x = Random.Range(-0.5f, 0.5f);
        float y = 0f;
        float z = Random.Range(-0.5f, 0.5f);

        return _northCreateZone.TransformPoint(x, y, z);
    }

    private Vector3 GetPointInSouthCreateZone() {
        float x = Random.Range(-0.5f, 0.5f);
        float y = 0f;
        float z = Random.Range(-0.5f, 0.5f);

        return _southCreateZone.TransformPoint(x, y, z);
    }

    private Vector3 GetPointInWestCreateZone() {
        float x = Random.Range(-0.5f, 0.5f);
        float y = 0f;
        float z = Random.Range(-0.5f, 0.5f);

        return _westCreateZone.TransformPoint(x, y, z);
    }

    private Vector3 GetPointInEastCreateZone() {
        float x = Random.Range(-0.5f, 0.5f);
        float y = 0f;
        float z = Random.Range(-0.5f, 0.5f);

        return _eastCreateZone.TransformPoint(x, y, z);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemyList.Remove(enemy); // ������� ����� �� ������
    }

    private int[] RandomSort(int length, int number) {
        int[] array = new int[length];
        for (int i = 0; i < array.Length; i++) {
            array[i] = i;
        }

        for (int i = 0; i < array.Length; i++) {
            int oldValue = array[i];
            int newIndex = Random.Range(0, array.Length);
            array[i] = array[newIndex];
            array[newIndex] = oldValue;
        }

        int[] result = new int[number];
        for (int i = 0; i < result.Length; i++) {
            result[i] = array[i];
        }

        return result;
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(_castle.gameObject.transform.position, Vector3.up, _creationRadius);
    }
#endif

}
