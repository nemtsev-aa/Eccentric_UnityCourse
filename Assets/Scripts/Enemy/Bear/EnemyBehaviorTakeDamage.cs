using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Enemy.Bear
{
    public class EnemyBehaviorTakeDamage : IEnemyBehavior
    {
        public EnemyBehaviorTakeDamage(EnemyBehaviorsManager enemy)
        {
            _enemy = enemy;
        }

        private EnemyBehaviorsManager _enemy;

        public void Enter()
        {
            _enemy.Animator.SetTrigger("Damage");
            _enemy.Blink.StartBlink();
        }

        public void Exit()
        {
            
        }

        public void Play()
        {
            
        }
    }
}
