using UnityEngine;

namespace Assets.Scripts.Enemy.Bear
{
    public class EnemyBehaviorInvulnerable : IEnemyBehavior
    {
        private EnemyBehaviorsManager _enemy;

        public EnemyBehaviorInvulnerable(EnemyBehaviorsManager enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            Debug.Log("Invulnerable Enter");
        }

        public void Exit()
        {
            Debug.Log("Invulnerable Exit");
        }

        public void Play()
        {
            
        }
    }
}

