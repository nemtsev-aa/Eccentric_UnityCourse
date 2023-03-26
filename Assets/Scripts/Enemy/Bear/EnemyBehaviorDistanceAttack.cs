using UnityEngine;

namespace Assets.Scripts.Enemy.Bear
{
    public class EnemyBehaviorDistanceAttack : IEnemyBehavior
    {
        public EnemyBehaviorDistanceAttack(EnemyBehaviorsManager enemy)
        {
            _enemy = enemy;
        }

        private EnemyBehaviorsManager _enemy;

        public void Enter()
        {
            Debug.Log("DistanceAttack Enter");
            _enemy.Animator.SetTrigger("DistanceAttack");
        }

        public void Exit()
        {
            Debug.Log("DistanceAttack Exit");
        }

        public void Play()
        {
            Debug.Log("DistanceAttack Play");
        }
    }
}
