using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Observer_Link_
{
    class Node
    {
        int x;
        int y;
        List<Subject> subjects;
        Subject subject;
        public int X 
        {
            get { return x; }

            set
            {
                x = value;
                foreach (var item in subjects)
                {
                    item?.StartEvent();
                }
            }

        }
        public int Y
        {
            get { return y; }

            set
            {
                y = value;
                foreach (var item in subjects)
                {
                    item?.StartEvent();
                }
            }

        }
        public Node(int x, int y)
        {
            subjects = new List<Subject>();
            X = x;
            Y = y;
        }

        public void Move(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Update(Subject sub)
        {
            subjects.Add(sub);
            subject = sub;
        }

        public Node Clone()
        {
            //Node nodeTemp = (Node)this.MemberwiseClone();
            Node temp = new Node(X, Y);
            return temp;
        }
    }
}
