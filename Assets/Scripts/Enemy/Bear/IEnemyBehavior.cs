using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enemy.Bear
{
    public interface IEnemyBehavior
    {
        void Enter();
        void Exit();
        void Play();
    }
}
