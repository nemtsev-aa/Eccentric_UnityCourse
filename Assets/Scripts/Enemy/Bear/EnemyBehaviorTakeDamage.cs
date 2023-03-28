using UnityEngine;

namespace Assets.Scripts.Enemy.Bear
{
    public class EnemyBehaviorTakeDamage : IEnemyBehavior
    {
        private EnemyBehaviorsManager _enemy;
        public EnemyBehaviorTakeDamage(EnemyBehaviorsManager enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            Debug.Log("TakeDamage Enter");
            _enemy.Animator.SetTrigger("Damage");
            _enemy.Blink.StartBlink();
        }

        public void Exit()
        {
            Debug.Log("TakeDamage Exit");
        }

        public void Play()
        {
            Debug.Log("TakeDamage Play");
        }
    }
}
