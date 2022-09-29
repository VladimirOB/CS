using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Interval_Server_
{
    class IntervalsDaughter : Intervals
    {
        //От класса IntervalCollection наследовать новый класс и добавить там opertor int () - подсчёт общей длины всех интервалов

        public static explicit operator int(IntervalsDaughter intervals)
        {
            double sum = 0;

            for (int i = 0; i < intervals.Count; i++)
            {
                sum += (intervals[i].End - intervals[i].Start);
            }
            return (int)Math.Round(sum);
        }
    }
}
