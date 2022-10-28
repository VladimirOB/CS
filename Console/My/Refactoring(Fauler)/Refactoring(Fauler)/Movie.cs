using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_Fauler
{
    abstract class Price
    {
        public abstract int getPriceCode();
        public abstract double getCharge(int daysRented);
    }

    class ChildrensPrice : Price
    {
        public override int getPriceCode()
        {
            return Movie.CHILDRENS;
        }
        public override double getCharge(int daysRented)
        {
            double result = 1.5;
            if (daysRented > 3)
                result += (daysRented - 3) * 1.5;
            return result;
        }
    }

    class NewReleasePrice : Price
    {
        public override int getPriceCode()
        {
            return Movie.NEW_RELEASE;
        }
        public override double getCharge(int daysRented)
        {
             return daysRented * 3;
        }
    }
    class RegularPrice : Price
    {
        public override int getPriceCode()
        {
            return Movie.REGULAR;
        }
        public override double getCharge(int daysRented)
        {
            double result = 2;
            result += 2;
            if (daysRented > 2)
                result += (daysRented - 2) * 1.5;
            return result;
        }
    }


    //Movie – класс, который представляет данные о фильме.
    class Movie
    {
        public const int CHILDRENS = 2;
        public const int REGULAR = 0;
        public const int NEW_RELEASE = 1;

        private string _title;
        private int _priceCode;
        private Price _price; //Strategy или State

        public Movie(string title, int priceCode)
        {
            _title = title;
            setPriceCode(priceCode);
        }

        public int getPriceCode()
        {
            return _price.getPriceCode();
        }

        public void setPriceCode(int arg)
        {
           switch(arg)
            {
                case REGULAR:
                    _price = new RegularPrice();
                    break;
                case CHILDRENS:
                    _price = new ChildrensPrice();
                    break;
                case NEW_RELEASE:
                    _price = new NewReleasePrice();
                    break;
                default:
                    throw new ArgumentException("Incorrect Price Code");
            }
        }

        public string getTitle()
        {
            return _title;
        }

        public double getChange(int daysRented)
        {
            return _price.getCharge(daysRented);

        }

        public int getFrequentRenterPoints(int daysRented)
        {
            if (getPriceCode() == NEW_RELEASE && daysRented > 1)
                return 2;
            else
                return 1;
        }
    }
}
