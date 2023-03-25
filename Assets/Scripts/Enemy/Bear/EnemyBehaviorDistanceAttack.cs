using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Enemy.Bear
{
    public class EnemyBehaviorDistanceAttack : IEnemyBehavior
    {
        public EnemyBehaviorDistanceAttack(EnemyBehaviorsManager enemy)
        {
            _enemy = enemy;
        }

        private EnemyBehaviorsManager _enemy;
        private EnemyHealth _currentEnemyHealth;

        // Время с прошлого переключения
        private float _timer;

        public void Enter()
        {
            _currentEnemyHealth = _enemy.EnemyHealth;
            Debug.Log("Enter EnemyBehaviorDistanceAttack"); 
        }

        public void Exit()
        {
            Debug.Log("Exit EnemyBehaviorDistanceAttack");
        }

        public void Play()
        {
            _timer += Time.deltaTime;
            if (_timer > _enemy.AttackPeriod)
            {
                _timer = 0;
                _enemy.Animator.SetTrigger("DistanceAttack");
                _currentEnemyHealth.StopInvulnerable();
            }
        }
    }
}
