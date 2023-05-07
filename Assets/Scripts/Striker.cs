using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Striker : MonoBehaviour
{
    [Tooltip("Дистанция атаки")]
    [SerializeField] private float _distanceToAttack = 4f;
    [Tooltip("Маркер")]
    [SerializeField] private GameObject _markerToAttack;
    [Tooltip("Физический слой для обработки атак")]
    [SerializeField] private LayerMask _layerMask;
    [Tooltip("Аниматор")]
    [SerializeField] private Animator _animator;
    [Tooltip("Скрипт перемещения игрока")]
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
        List<EnemyAnimal> targetList = new List<EnemyAnimal>(); // Список целей
        
        if (animals.Count == 1)
            targetList = animals;
        else
            targetList = CreateTargetList(animals); 
        
        Debug.Log("Количество врагов: " + targetList.Count);
        
        EnemyAnimal targetToStrike = targetList[targetList.Count-1]; // Текущая цель для атаки
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
        List<EnemyAnimal> enemyAnimals = new List<EnemyAnimal>(); //Список всех врагов в зоне атаки
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, _distanceToAttack,
            _layerMask, QueryTriggerInteraction.Ignore); // Массив коллайдеров с которыми взаимодействует игрок
        
        if (colliders.Length > 0)
            for (int i = 0; i < colliders.Length; i++) //Поиск всех врагов в зоне атаки
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
        List<EnemyAnimal> targetList = new List<EnemyAnimal>(); // Список целей
        List<EnemyAnimal> enemys = enemyAnimals; // Список врагов

        for (int i = enemys.Count; i > 0; i--)
        {
            EnemyAnimal nearAnimal = FindNearEnemy(enemys); // Ближний враг
            targetList.Add(nearAnimal); // Добавляем врага в список целей
            enemys.Remove(nearAnimal);  // Удаляем врага из общего списка доступных врагов
        }

        return targetList;
    }

    private EnemyAnimal FindNearEnemy(List<EnemyAnimal> enemyAnimals)
    {
        EnemyAnimal NearEnemy = enemyAnimals[0]; // Ближний враг
        float distanceToNearEnemy = _distanceToAttack; // Дистанция до ближнего врага

        for (int i = 0; i < enemyAnimals.Count; i++) // Перебираем список врагов, находящихся в зоне поражения
        {
            Vector3 enemyAnimalPosition = enemyAnimals[i].gameObject.transform.position; // Позиция проверяемого врага
            float distanceToEnemy = (transform.position - enemyAnimalPosition).magnitude; // Расстояние до проверяемого врага
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
