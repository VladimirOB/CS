using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Army_Interfaces_Attack_Fly_Walk_
{
    interface IAttack
    {
        void Move();
        void Attack(int x, int y);

        void Retreat();

        void Stop();

        void Defend();

    }
}

