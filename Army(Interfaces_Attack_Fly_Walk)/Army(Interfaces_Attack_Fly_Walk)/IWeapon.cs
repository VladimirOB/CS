using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Army_Interfaces_Attack_Fly_Walk_
{
    interface IWeapon
    {
        int Damage { get; }
        void Fire();
    }

    abstract class Weapon : IWeapon
    {
        public abstract int Damage { get; }
        public abstract void Fire();
    }

    class MachineGun : Weapon
    {
        public override int Damage => 50; 

        public override void Fire()
        {
            Console.WriteLine("тра та та та та та ... х150");
        }
    }
    class AK47 : Weapon
    {
        public override int Damage => 40;

        public override void Fire()
        {
            Console.WriteLine($"{GetType().Name} Damage: {Damage}");
            Console.WriteLine("тра та та та та та ... х30");
        }
    }
}
