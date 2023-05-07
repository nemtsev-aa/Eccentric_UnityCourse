using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Striker : MonoBehaviour
{
    [Tooltip("��������� �����")]
    [SerializeField] private float _distanceToAttack = 4f;
    [Tooltip("������")]
    [SerializeField] private GameObject _markerToAttack;
    [Tooltip("���������� ���� ��� ��������� ����")]
    [SerializeField] private LayerMask _layerMask;
    [Tooltip("��������")]
    [SerializeField] private Animator _animator;
    [Tooltip("������ ����������� ������")]
    [SerializeField] private RigidbodyMove _rigidBodyMove;

    private void FixedUpdate()
    {
        switch (_rigidBodyMove.CurrentMoveStatus)
        {
            case MoveStatus.Stop:
                List<EnemyAnimal> enemyAnimals = FindAllEnemys();
                if (enemyAnimals != null)
                {
                    Attack(enemyAnimals);

                }
                break;
            case MoveStatus.Active:
                _animator.SetBool("Attack", false);
                break;
            default:
                break;
        }
    }

    public void Attack(List<EnemyAnimal> animals)
    {
        List<EnemyAnimal> targetList = new List<EnemyAnimal>(); // ������ �����
        
        if (animals.Count == 1)
            targetList = animals;
        else
            targetList = CreateTargetList(animals); 
        
        Debug.Log("���������� ������: " + targetList.Count);
        
        EnemyAnimal targetToStrike = targetList[targetList.Count-1]; // ������� ���� ��� �����
        transform.LookAt(new Vector3(targetToStrike.transform.position.x, 0f, targetToStrike.transform.position.z));
        StrikeToEnemy(targetToStrike);
    }

    public void StrikeToEnemy(EnemyAnimal animal)
    {
        GameObject mark = Instantiate(_markerToAttack, animal.transform.position, animal.transform.rotation);
        mark.transform.parent = animal.transform;

        _animator.SetBool("Attack", true);
        Debug.Log("Attack " + animal.name.ToString());
    }

    private List<EnemyAnimal> FindAllEnemys() 
    {
        List<EnemyAnimal> enemyAnimals = new List<EnemyAnimal>(); //������ ���� ������ � ���� �����
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, _distanceToAttack,
            _layerMask, QueryTriggerInteraction.Ignore); // ������ ����������� � �������� ��������������� �����
        
        if (colliders.Length > 0)
            for (int i = 0; i < colliders.Length; i++) //����� ���� ������ � ���� �����
            {
                if (colliders[i].attachedRigidbody.GetComponent<EnemyAnimal>() is EnemyAnimal animal)
                    enemyAnimals.Add(animal);
            }

        if (enemyAnimals.Count > 0)
            return enemyAnimals;
        else
            return null;
    }

    private List<EnemyAnimal> CreateTargetList(List<EnemyAnimal> enemyAnimals)
    {
        List<EnemyAnimal> targetList = new List<EnemyAnimal>(); // ������ �����
        List<EnemyAnimal> enemys = enemyAnimals; // ������ ������

        for (int i = enemys.Count; i > 0; i--)
        {
            EnemyAnimal nearAnimal = FindNearEnemy(enemys); // ������� ����
            targetList.Add(nearAnimal); // ��������� ����� � ������ �����
            enemys.Remove(nearAnimal);  // ������� ����� �� ������ ������ ��������� ������
        }

        return targetList;
    }

    private EnemyAnimal FindNearEnemy(List<EnemyAnimal> enemyAnimals)
    {
        EnemyAnimal NearEnemy = enemyAnimals[0]; // ������� ����
        float distanceToNearEnemy = _distanceToAttack; // ��������� �� �������� �����

        for (int i = 0; i < enemyAnimals.Count; i++) // ���������� ������ ������, ����������� � ���� ���������
        {
            Vector3 enemyAnimalPosition = enemyAnimals[i].gameObject.transform.position; // ������� ������������ �����
            float distanceToEnemy = (transform.position - enemyAnimalPosition).magnitude; // ���������� �� ������������ �����
            if (distanceToEnemy < distanceToNearEnemy)
            {
                NearEnemy = enemyAnimals[i];
            }
        }

        return NearEnemy;
    }

   

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, _distanceToAttack);
    }
#endif
}
