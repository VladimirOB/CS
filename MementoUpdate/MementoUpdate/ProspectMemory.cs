using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MementoUpdate
{
    class ProspectMemory
    {
        private Memento _memento;
        Dictionary<double, Memento> _mementoPack;
        private double id;
        
        ushort secretKey = 0x00889; // Секретный ключ (16 bit).
        public ProspectMemory()
        {
            _mementoPack = new Dictionary<double, Memento>();
        }

        public void Add(Memento memento)
        {
            id = Convert.ToDouble(DateTime.Now.Hour.ToString() + "," + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString());
            Save(memento);
            _mementoPack.Add(id, memento);
        }

        void Save(Memento memento)
        {
            StreamWriter sw = new StreamWriter("../../../../db.txt", true);
            sw.Write(EncodeDecrypt(id)+ '|');
            foreach (var item in memento.dict)
            {
                sw.Write(EncodeDecrypt(item.Value) + "|");
            }
            sw.Write('\n');
            sw.Close();
            //File.AppendAllText("../../../../db.txt", EncodeDecrypt(id) + '|');
            //foreach (var item in memento.dict)
            //{
            //    File.AppendAllText("../../../../db.txt",
            //    EncodeDecrypt(item.Value) + "|");
            //}
        }

        public void Load(string path)
        {
            string temp = File.ReadAllText(path);
            string[] str = temp.Split(new[] { '|', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < str.Length; i+=4)
            {
                id = Convert.ToDouble(EncodeDecrypt(str[i]));
                _mementoPack.Add(id, new Memento(EncodeDecrypt(str[i + 1]), EncodeDecrypt(str[i + 2]), (EncodeDecrypt(str[i + 3]))));
            }
        }

        // кодировка - расшифровка.
        string EncodeDecrypt(object str)
        {
            string str2 = new string(str.ToString());
            char[] ch = str2.ToArray();
            string newStr = "";
            foreach (var c in ch) //производим шифрование каждого отдельного символа
            {
                newStr += Secret(c);
            }
            return newStr;
        }

        char Secret(char ch)
        {
            ch = (char)(ch ^ secretKey); //Производим XOR операцию (сложение по модулю 2)
            return ch;
        }

        public void ViewMemory()
        {
            foreach (var item in _mementoPack)
            {
                Console.WriteLine($"Id: {item.Key}");
                Console.WriteLine($"Name: {item.Value.dict["name"]}");
                Console.WriteLine($"Phone: {item.Value.dict["phone"]}");
                Console.WriteLine($"Budget: {item.Value.dict["budget"]}\n");
            }
        }

        public Memento Restore(double id)
        {
            if(_mementoPack.ContainsKey(id))
            {
                Memento temp = _mementoPack[id];
                _mementoPack.Remove(id);
                return temp;
            }
            else
            {
                Console.WriteLine("Invalid id");
                return null;
            }
            
        }

        public void Clear()
        {
            _mementoPack.Clear();
        }
    }
}
