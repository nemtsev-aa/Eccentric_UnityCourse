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

        private float _timer;

        public void Enter()
        {
            Debug.Log("Invulnerable Enter");
            _enemy.Shield.SetActive(true);
            _enemy.IsInvulnerable = true;
        }

        public void Exit()
        {
            Debug.Log("Invulnerable Exit");
        }

        public void Play()
        {
            _timer += Time.deltaTime;
            if (_timer >= _enemy.InvulnerablePeriod)
            {
                _timer = 0;
                _enemy.Shield.SetActive(false);
                _enemy.IsInvulnerable = false;
            }
        }
    }
}

