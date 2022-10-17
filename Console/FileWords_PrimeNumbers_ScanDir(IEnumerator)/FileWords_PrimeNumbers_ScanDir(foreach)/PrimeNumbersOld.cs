using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWords_PrimeNumbers_ScanDir_foreach_
{
    internal class PrimeNumbersOld : IEnumerable, IEnumerator
    {
        int start, end, curpos, cnt = 0;
        int[] result;

        public PrimeNumbersOld(int start, int endd)
        {
            this.start = start; this.end = endd+1;
            result = new int[end];

            int[] temp = new int[end];
            for (int i = start; i < end; i++)
            {
                temp[cnt++] = i;
            }
            cnt = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                for (int k = 2; k < temp[i] / 2; k++)
                {
                    if (temp[i] % k == 0 && temp[i] != k)
                    {
                        temp[i] = 0;
                        break;
                    }
                }
                if (temp[i] > 1 && temp[i] != 4)
                    result[cnt++] = temp[i];
            }
            curpos = cnt++;
        }

        public object Current
        {
            get
            {
                if (result[curpos] > 1)
                    return result[curpos-cnt];
                else
                    return 0;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if(curpos > 0)
            {
                curpos--;
                cnt -= 2;
                return true;
            }
            else
            {
                curpos = result.Length;
                return false;
            }
        }

        public void Reset()
        {
            curpos = result.Length;
        }
      
    }
}
