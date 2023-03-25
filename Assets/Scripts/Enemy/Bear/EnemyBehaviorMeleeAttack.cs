using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.Enemy.Bear
{
    public class EnemyBehaviorMeleeAttack : IEnemyBehavior
    {
        public EnemyBehaviorMeleeAttack(EnemyBehaviorsManager enemy)
        {
            _enemy = enemy;
        }

        private EnemyBehaviorsManager _enemy;
        private EnemyHealth _currentEnemyHealth;
        // Время с прошлого переключения
        private float _timer;
        private LeftToRightMove _move;
        
        public void Enter()
        {
            Debug.Log("Enter EnemyBehaviorMeleeAttack");
            _currentEnemyHealth = _enemy.EnemyHealth;
            _move = _enemy.EnemyHealth.gameObject.GetComponent<LeftToRightMove>();
            _move.enabled = false;

        }

        public void Exit()
        {
            Debug.Log("Exit EnemyBehaviorMeleeAttack");
            _move.enabled = true;
            
        }

        public void Play()
        {
            _timer += Time.deltaTime;
            if (_timer > _enemy.AttackPeriod)
            {
                _timer = 0;
                MeleeAttack();
                _enemy.Animator.SetTrigger("MeleeAttack");
                _currentEnemyHealth.StopInvulnerable();
            }
        }

        private void MeleeAttack()
        {
            Debug.Log("MeleeAttack");
            Transform enemyTransform = _enemy.transform;
            Transform oldEnemyTransform = enemyTransform;
            Vector3 playerTransform = new Vector3(_enemy.PlayerTransform.position.x, _enemy.PlayerTransform.position.y + 2f, 0f);
            Sequence mySequence = DOTween.Sequence().SetUpdate(true);
            mySequence.SetDelay(0.03f);
            mySequence.SetEase(Ease.Linear);
            mySequence.Append(enemyTransform.DOMove(playerTransform, 0.3f, false).SetEase(Ease.InOutQuint));
            mySequence.Append(enemyTransform.DOMove(oldEnemyTransform.position, 0.3f, false).SetEase(Ease.InOutQuint));
        }
    }
}
