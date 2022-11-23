using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Game15
{

    public class Record : IComparable<Record>
    {
        public string? Name { get; set; }
        public int Number { get; set; }
        public int Time { get; set; }
        public int Steps { get; set; }

        public Record(int num, string name, int time, int steps)
        {
            Number = num;
            Name = name;
            Time = time;
            Steps = steps;
        }

        public int CompareTo(Record other)
        {
            return Time.CompareTo(other.Time);
        }

    }

    public partial class RecordsWindow : Window
    {
        public List<Record> rec { get;set;}
        public RecordsWindow()
        {
            InitializeComponent();
            LoadRecords();
        }

        private void LoadRecords()
        {
            string[] str = File.ReadAllText("records.txt").Split(new[] {' ', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            rec = new List<Record>();
            for (int i = 0, num = 1; i < str.Length; i+=3)
            {
                Record record = new Record(num++, str[i], Convert.ToInt32(str[i+1]), Convert.ToInt32(str[i + 2]));
                rec.Add(record);
            }
            rec.Sort();
            for (int i = 0; i < rec.Count; i++)
            {
                rec[i].Number = i + 1;
            }
            DataContext = this;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText("records.txt", "");
            MessageBox.Show("Reset records successful! Close a window.");
        }
    }
}
