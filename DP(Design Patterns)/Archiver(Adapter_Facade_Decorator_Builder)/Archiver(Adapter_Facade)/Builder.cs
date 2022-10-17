using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiver_Adapter_Facade_
{
    //Builder
    interface IBuilder
    {
        void ReadHeader(string name);
        void ReadBody();

        Product GetResult();

    }

    class RaRBuilder : IBuilder
    {
        private Product product = new Product();

        public void ReadHeader(string ext)
        {
            product.Add(ext);
        }

        public void ReadBody()
        {
            product.Add("Rar body");
        }

        public Product GetResult()
        {
            return product;
        }
    }

    class ZipBuilder : IBuilder
    {
        private Product product = new Product();

        public void ReadHeader(string name)
        {
            product.Add(name);
        }
        public void ReadBody()
        {
            product.Add("Zip body");
        }
        public Product GetResult()
        {
            return product;
        }
    }

    //Product
    class Product
    {
        ArrayList prod = new ArrayList();

        public void Add(string part)
        {
            prod.Add(part);
        }

        public void Arch()
        {
            foreach (var item in prod)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Archive end.");
        }

        public void UnArch()
        {
            foreach (var item in prod)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("UnArchive end.");
        }
    }
}
