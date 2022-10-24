using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_Before
{
    //Movie – класс, который представляет данные о фильме.
    class Movie
    {
        public const int CHILDRENS = 2;
        public const int REGUAL = 0;
        public const int NEW_RELEASE = 1;

        private string _title;
        private int _priceCode;

        public Movie(string title, int priceCode)
        {
            _title = title;
            _priceCode = priceCode;
        }

        public int getPriceCode()
        {
            return _priceCode;
        }

        public void setPriceCode(int arg)
        {
            _priceCode = arg;
        }

        public string getTitle()
        {
            return _title;
        }
    }
}
