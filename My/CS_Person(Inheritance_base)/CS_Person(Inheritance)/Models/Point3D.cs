using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Person_Inheritance_.Models
{
    class Point3D : Point2D
    {
        public int Z { get; set; }
        public Point3D(int x, int y, int z) : base(x,y)
        {
            Z = z;
        }

        public void Print3D()
        {
            base.Print2D();
            Console.WriteLine("Z: " + Z);
        }
    }
}
