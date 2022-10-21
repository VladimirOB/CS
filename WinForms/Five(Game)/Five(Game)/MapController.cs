using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Five_Game_
{
    public static class MapController
    {
        public static int mapSize = 16;
        public const int cellSize = 25; // размер клетки

        public static int[,] map = new int[mapSize, mapSize];
        public static Button[,] buttons = new Button[mapSize, mapSize];

        public static Image spriteSet;

        private static Form form;

        public static void Init(Form current)
        {
            form = current;
            spriteSet = new Bitmap("tiles.png");
            ConfigureMapSize(current);
            InitMap();
            InitButtons(current);
        }

        private static void ConfigureMapSize(Form current)
        {
            current.Width = mapSize * cellSize + 20;
            current.Height = (mapSize+2) * cellSize;
        }

        private static void InitMap()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int k = 0; k < mapSize; k++)
                {
                    map[i, k] = 0;
                }
            }
        }
        private static void InitButtons(Form current)
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int k = 0; k < mapSize; k++)
                {
                    Button button = new Button();
                    button.Location = new Point(k * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.Image = FindNeededImage(0, 0);
                    button.MouseUp += new MouseEventHandler(OnButtonPressedMouse);
                    current.Controls.Add(button);
                    buttons[i, k] = button;
                }
            }
        }
        private static void OnButtonPressedMouse(object sender, MouseEventArgs e)
        {
            Button pressedButton = sender as Button;
            int iButton = pressedButton.Location.Y / cellSize; // координаты нажатой кнопки
            int jButton = pressedButton.Location.X / cellSize;
            if (map[iButton, jButton] != 1)
            {
                map[iButton, jButton] = 1;
                pressedButton.Image = FindNeededImage(1, 0);
                //pressedButton.Enabled = false;

            }

            //метод для анализа угрозы игрока.

            //метод для оптимального хода
            
            Random r = new Random();
            int k=0, l=0;
            while (map[k,l] != 2)
            {
                k = r.Next(iButton - 1, iButton + 2);
                l = r.Next(jButton - 1, jButton + 2);
                if (!IsInBorder(k, l) || map[k, l] != 1) // если не в границах или крест
                {
                    map[k, l] = 2;
                    buttons[k, l].Image = FindNeededImage(2, 0);
                    buttons[k, l].Enabled = false;
                    break;
                }
                
            }
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {

                }
            }
        }

        public static void Danger(int i, int j)
        {  

           if (map[i, j] == 1)

          
        }

        public static Image FindNeededImage(int xPos, int yPos)
        {
            Image image = new Bitmap(cellSize, cellSize);
            Graphics g = Graphics.FromImage(image);
            //вырезать конкретную картинку из атласа
            g.DrawImage(spriteSet, new Rectangle(new Point(0, 0), new Size(cellSize, cellSize)), 0 + 32 * xPos, 0 + 32 * yPos, 33, 33, GraphicsUnit.Pixel);

            return image;
        }

        private static bool IsInBorder(int i, int j)
        {
            if (i < 0 || j < 0 || j > mapSize - 1 || i > mapSize - 1)
                return false;
            return true;
        }
    }
}
