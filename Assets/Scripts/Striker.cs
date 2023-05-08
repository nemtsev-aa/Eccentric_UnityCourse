using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Striker : MonoBehaviour
{
    [Tooltip("Менеджер врагов")]
    [SerializeField] private Enemies _enemies;
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

    [SerializeField] private EnemyAnimal[] targetList;
    private void FixedUpdate()
    {
        switch (_rigidBodyMove.CurrentMoveStatus)
        {
            case MoveStatus.Stop:
                EnemyAnimal[] targetList = _enemies.GetNearest(transform.position, 5);
                if (targetList.Length > 0)
                {
                    Attack(targetList);
                }
                break;
            case MoveStatus.Active:
                _animator.SetBool("Attack", false);
                break;
            default:
                break;
        }
    }

    public void Attack(EnemyAnimal[] targetList)
    {
        Debug.Log("Количество врагов: " + targetList.Length);
        
        EnemyAnimal targetToStrike = targetList[0]; // Текущая цель для атаки
        transform.LookAt(new Vector3(targetToStrike.transform.position.x, 0f, targetToStrike.transform.position.z));
        StrikeToEnemy(targetToStrike);
    }

    public void StrikeToEnemy(EnemyAnimal animal)
    {
        if (!animal.transform.GetComponentInChildren<ShowTargetMark>())
        {
            _markerToAttack.GetComponent<ShowTargetMark>().SetTarget(animal.transform);
        }
       
        _animator.SetBool("Attack", true);
        Debug.Log("Attack " + animal.name.ToString());
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, _distanceToAttack);
    }
#endif
}
