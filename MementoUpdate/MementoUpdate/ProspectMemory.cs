using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MementoUpdate
{
    class ProspectMemory
    {
        private Memento _memento;
        Dictionary<uint, Memento> _mementoPack;
        private uint id;

        public ProspectMemory()
        {
            _mementoPack = new Dictionary<uint, Memento>();
        }

        public void Add(Memento memento)
        {
            Save(memento);
            _mementoPack.Add(id++, memento);
        }

        void Save(Memento memento)
        {
            //File.AppendAllText("../../../../db.json", id + JsonSerializer.Serialize<Memento>(memento));
            File.AppendAllText("../../../../db.txt",
                id.ToString() + "|"
                + memento.Name + "|"
                + memento.Phone + "|"
                + memento.Budget + "|\n");
        }

        public void Load(string path)
        {
            string temp = File.ReadAllText(path);
            string[] str = temp.Split(new[] { '|', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < str.Length; i+=4)
            {
                id = (uint)Convert.ToInt32(str[i]);
                _mementoPack.Add(id, new Memento(str[i + 1], str[i + 2], str[i + 3]));
            }
        }

        public void ViewMemory()
        {
            foreach (var item in _mementoPack)
            {
                Console.WriteLine($"Id: {item.Key}");
                Console.WriteLine($"Name: {item.Value.Name}");
                Console.WriteLine($"Phone: {item.Value.Phone}");
                Console.WriteLine($"Bugdet: {item.Value.Budget}\n");
            }
        }

        public Memento Restore(uint id)
        {
            Memento temp = _mementoPack[id];
            _mementoPack.Remove(id);
            return temp;
        }

        public Memento Memento
        {
            get { return _memento; }
            set { _memento = value; }
        }
    }
}
