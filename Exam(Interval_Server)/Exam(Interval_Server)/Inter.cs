using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Interval_Server_
{
    class Inter
    {
        double start;
        double end;

        public Inter(double start, double end)
        {
            Start = start;
            End = end;
        }

        public double Start { get { return start; } set { start = value; } }
        public double End { get { return end; } set { end = value; } }
    }
}
