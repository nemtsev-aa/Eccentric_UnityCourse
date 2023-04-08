using UnityEngine;

namespace Assets.Scripts.Enemy.Bear
{
    public class EnemyBehaviorIdle : IEnemyBehavior
    {
        public EnemyBehaviorIdle(EnemyBehaviorsManager enemy)
        {
            _enemy = enemy;
        }

        private EnemyBehaviorsManager _enemy;

        public void Enter()
        {
            Debug.Log("Idle Enter");
        }

        public void Exit()
        {
            Debug.Log("Idle Exit");
        }

        public void Play()
        {
            Debug.Log("Idle Play");
        }

    }
}
