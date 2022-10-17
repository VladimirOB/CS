using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Army_Interfaces_Attack_Fly_Walk_
{
    interface IWalk
    {
        void Stand();

        void Walk();

        void Stop();

        void Run();

        void GetCoords(int coord);
    }
}
