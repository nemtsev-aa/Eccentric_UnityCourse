using UnityEngine;
using UnityEngine.Events;

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
           
        }

        public void Exit()
        {

        }

        public void Play()
        {
            
        }
    }
}
