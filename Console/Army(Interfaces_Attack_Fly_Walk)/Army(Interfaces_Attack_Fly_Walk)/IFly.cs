using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Army_Interfaces_Attack_Fly_Walk_
{
    interface IFly
    {
        void TakeOff();

        void Land();

        void Move(double x, double y, double z);

        // получить высоту полёта
        double Height
        {
            get;
        }

        // получить все пройденные координаты полёта
        double this[int pos]
        {
            get;
        }
    }
}
