using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory_Archiver_
{
    //Product
    abstract class CompressObject
    {
        public abstract void Operation();
    }

    class ProductsContainer
    {
        List<CompressObject> lst = new List<CompressObject>();

        public void Add(CompressObject obj)
        {
            lst.Add(obj);
        }

        public void Run()
        {
            foreach (CompressObject obj in lst)
            {
                obj.Operation();
            }
        }
    }

    class AbstractFactory
    {
    }
}
