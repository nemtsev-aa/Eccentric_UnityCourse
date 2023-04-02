using System.Linq;
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
        private float _timer;

        public void Enter()
        {
            Debug.Log("MeleeAttack Enter");
            MeleeAttack();
        }

        public void Exit()
        {
            Debug.Log("MeleeAttack Exit");
        }

        public void Play()
        {
            Debug.Log("MeleeAttack Play");
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
