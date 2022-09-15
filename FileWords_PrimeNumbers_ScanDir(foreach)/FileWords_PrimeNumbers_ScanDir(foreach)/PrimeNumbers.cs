using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWords_PrimeNumbers_ScanDir_foreach_
{

    /*2. Создать класс PrimeNumbers, который принимает в конструкторе диапазон чисел 
     * и позволяет в цикле foreach перебрать все простые числа в этом диапазоне*/
    internal class PrimeNumbers
    {
        int start, end, cmd = 0;

        public PrimeNumbers(int start, int end)
        {
            this.start = start; this.end = end+1;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = start; i < end; i++)
            {
                cmd = 0;
                if (i < 2)
                    cmd = 1;
                for (int k = 2; k < i/2+1; k++)
                {
                    if(i % k == 0 && i != k)
                    {
                        cmd = 1;
                    }
                }
                if (cmd == 0)
                    yield return i;
            }
        }

    }
}
