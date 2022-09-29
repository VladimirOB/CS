using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Exam_Interval_Server_
{
    /*1. Задача на 6 баллов

    Разработать класс-коллекцию, который позволяет хранить список интервалов. 
    Любой интервал имеет начало и конец, которые заданы 2 числами типа double. 
    Интервал представляет собой отдельный класс. 

    Класс коллекции имеет следующие методы:
    - конструктор по умолчанию
    - конструктор, принимающий один интервал
    - конструктор копирования
    - Add(Interval interval) - добавление в коллекцию
    - RemoveByPos(int pos) - удаление интервала по позиции
    - RemoveByValue - удаление всех интервалов, с началом меньшим, чем значение
    - RemoveByLength - удаление всех интервалов, с длиной, меньше заданной
    - свойство для чтения Count - количество интервалов
    - GetLongest() - получить самый длинный интервал
    - GetShortest() - получить самый короткий интервал
    - operator + , operator += - добавление интервала в коллекцию интервалов
    - operator[] (индексатор) - получить интервал по номеру
    - operator ==, != - сравнение коллекций интервалов
    - свойство для чтения bool HasHoles сообщает, содержит ли коллекция интервалов "дыры" внутри, то есть, все ли интервалы перекрывают друг друга
    - свойства для чтения Start, End координаты начала первого и конца последнего по координатам интервала
    - энумератор, который позволяет перебирать все интервалы в цикле foreach
    - Save, Load -  сохрание, загр в файл
    */
    [Serializable]
    class Intervals
    {
        List<Inter> intervals;
        public int Count 
        {
            get
            {
                return intervals.Count;
            }
        }
        public Intervals()
        {
            intervals = new List<Inter>();
        }
        public Intervals(Inter interval)
        {
            intervals = new List<Inter>();
            Add(interval);
        }

        public Intervals(Intervals inters) // copy
        {
            intervals = new List<Inter>(inters.intervals);
        }

        public void Add(Inter interval)
        {
            if(interval.Start > interval.End)
            {
                double temp = interval.Start;
                interval.Start = interval.End;
                interval.End = temp;
            }
            intervals.Add(interval);
        }

        public void RemoveByPos(int pos)
        {
            int cnt = 0;
            foreach (var current in intervals)
            {
                if(cnt == pos)
                {
                    intervals.Remove(current);
                    break;
                }
                cnt++;
            }
        }

        public void RemoveByValue(double value) // удаление всех интервалов, с началом меньшим, чем значение
        {
            foreach (var current in intervals)
            {
                if (current.Start < value)
                {
                    intervals.Remove(current);
                    RemoveByValue(value);
                    break;
                }
            }
        }


        public void RemoveByLength(double length) // - удаление всех интервалов, с длиной, меньше заданной
        {
            foreach (var current in intervals)
            {
                if (current.End - current.Start < length)
                {
                    intervals.Remove(current);
                    RemoveByLength(length);
                    break;
                }
            }
        }

        public Inter? GetLongest()
        {
            
            Inter temp = new Inter(0, 0);
            foreach (Inter current  in intervals)
            {
                if (current.End - current.Start > temp.End - temp.Start)
                    temp = current;
            }
            return temp;
        }

        public Inter? GetShortest()
        {
            Inter temp = intervals[0];
            foreach (Inter current in intervals)
            {
                if (current.End - current.Start < temp.End - temp.Start)
                    temp = current;
            }
            return temp;
        }

        public static Intervals operator +(Intervals intervals, Inter inter) // добавление интервала в коллекцию интервалов += авто
        {
            intervals.Add(inter);
            return intervals;
        }

        public static bool operator==(Intervals intervals, Intervals intervals2)
        {
            if (intervals.Count == intervals2.Count)
            {
                for (int i = 0; i < intervals.Count; i++)
                {
                    if (intervals[i].Start != intervals2[i].Start && intervals[i].End != intervals2[i].End)
                        return false;
                }
                return true;
            }
            else
                return false;
        }
        public static bool operator !=(Intervals intervals, Intervals intervals2)
        {
            if (intervals.Count == intervals2.Count)
            {
                for (int i = 0; i < intervals.Count; i++)
                {
                    if (intervals[i].Start != intervals2[i].Start && intervals[i].End != intervals2[i].End)
                        return true;
                }
                return false;
            }
            else
                return true;
        }

        public Inter this[int pos]
        {

            get { if (pos >= 0 && pos < Count) return intervals[pos]; else throw new Exception("Incorrect index!"); }
            set { if (pos >= 0 && pos < Count) intervals[pos] = value; else throw new Exception("Incorrect index!"); }
        }

        //свойство для чтения bool HasHoles сообщает,
        //содержит ли коллекция интервалов "дыры" внутри, то есть, все ли интервалы перекрывают друг друга
        
       
        public bool HasHoles
        {
            get
            {
                double start = double.MaxValue;
                double end = double.MinValue;

                for (int i = 0; i < Count; i++)
                {
                    if (intervals[i].Start < start)
                    {
                        start = intervals[i].Start;
                    }
                    if (intervals[i].End > end)
                    {
                        end = intervals[i].End;
                    }
                }
                Dictionary<double, bool> check = new Dictionary<double, bool>();
                for (double i = start; i < end; i++)
                {
                    check.Add(i, false);
                }
                for (int i = 0; i < Count; i++)
                {
                    for (double k = intervals[i].Start; k < intervals[i].End; k++)
                    {
                        if(check.ContainsKey(k))
                        {
                            check[k] = true;
                        }
                    }
                }
                for (double i = start; i < end; i++)
                {
                    if (check[i] == false)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        //- свойства для чтения Start, End координаты начала первого и конца последнего по координатам интервала
        public double Start
        {
            get
            {
                double temp = intervals[0].Start;
                foreach (var item in intervals)
                {
                    if (item.Start < temp)
                        temp = item.Start;
                }
                return temp;
            }
        }

        public double End
        {
            get
            {
                double temp = intervals[0].Start;
                foreach (var item in intervals)
                {
                    if (item.Start > temp)
                        temp = item.Start;
                }
                return temp;
            }
        }

        public void SaveJS()
        {
            string res = JsonSerializer.Serialize(this);
            File.WriteAllText("../../../../db.json", res);
        }

        public void Save(string path)
        {
            try 
            { 
                StreamWriter sw = new StreamWriter(path);
                foreach (var current in intervals)
                {
                    sw.WriteLine(current.Start + " " + current.End);
                }
                sw.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
}

        public void Load(string path)
        {
            intervals.Clear();
            try
            {
                //StreamReader sr = new StreamReader(path);
                string str = File.ReadAllText(path);
                string[] str2 = str.Split(' ', '\r', '\n');
                str2 = str2.Where(a => a != "").ToArray();
                for (int i = 0; i < str2.Length; i+=2)
                {
                    if(i+1 != str2.Length)
                    {
                        Add(new Inter(Convert.ToDouble(str2[i]), Convert.ToDouble(str2[i+1])));
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var current in intervals)
            {
                yield return $"{current.Start} : {current.End}";
            }
        }
    }
}
