using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapper.Controllers
{
    public static class MapController
    {
        public static int mapSize = 20;
        public const int cellSize = 50; // Размер клетки
        public static int numberOfBombs = 20;
        private static int currentPictureToSet = 0;
        private static int countOfSteps;
        public static int[,] map = new int[mapSize, mapSize];
        public static Button[,] buttons = new Button[mapSize, mapSize];

        public static Image spriteSet; //атлас изображений

        private static bool isFirstStep;

        private static Point firstCoord;

        private static Form form;

        //инициализация
        public static void Init(Form current)
        {
            countOfSteps = 0;
            form = current;
            currentPictureToSet = 0;
            isFirstStep = true;
            spriteSet = new Bitmap("tiles.png");
            ConfigureMapSize(current);
            InitMap();
            InitButtons(current);
        }

        private static void ConfigureMapSize(Form current)
        {
            current.Width = mapSize * cellSize + 20;
            current.Height = (mapSize + 1) * cellSize;
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
            switch(e.Button.ToString()) // проверка на ЛКМ или ПКМ
            {
                case "Right":
                    OnRightButtonPressed(pressedButton);
                    break;
                case "Left":
                    OnLeftButtonPressed(pressedButton);
                    break;
            }
        }

        private static void OnRightButtonPressed(Button pressedButton)
        {
            currentPictureToSet++;
            currentPictureToSet %= 3; // разделить по модулю на 3, чтоб не становилась больше 3х
            int posX = 0;
            int posY = 0;
            switch(currentPictureToSet)
            {
                case 0: 
                    posX = 0;
                    posY = 0;
                    break;
                case 1: // флажок
                    posX = 0;
                    posY = 2;
                    break;
                case 2: // знак вопроса
                    posX = 2;
                    posY = 2;
                    break;
            }

            pressedButton.Image = FindNeededImage(posX,posY);
        }

        private static void OnLeftButtonPressed(Button pressedButton)
        {
            pressedButton.Enabled = false;
            int iButton = pressedButton.Location.Y / cellSize; // координаты нажатой кнопки
            int jButton = pressedButton.Location.X / cellSize;
            if (isFirstStep)
            {
                firstCoord = new Point(jButton, iButton);
                SeedMap();
                CountCellBomb();
                isFirstStep = false;
            }
            //открываем все пустые ячейки рядом
            OpenCells(iButton, jButton);

            if (map[iButton,jButton] == -1)
            {
                ShowAllBombs(iButton, jButton);
                MessageBox.Show("Поражение!", "Game over!");
                form.Controls.Clear(); // очистка формы
                Init(form); // перезапуск
            }
            if (mapSize*mapSize-countOfSteps == numberOfBombs)
            {
                ShowAllBombs(iButton, jButton);
                MessageBox.Show("Победа!", "Урааа!");
                form.Controls.Clear(); // очистка формы
                Init(form); // перезапуск
            }
        }

        public static Image FindNeededImage(int xPos, int yPos)
        {
            Image image = new Bitmap(cellSize, cellSize);
            Graphics g = Graphics.FromImage(image);
            //вырезать конкретную картинку из атласа
            g.DrawImage(spriteSet, new Rectangle(new Point(0,0), new Size(cellSize,cellSize)), 0+32*xPos, 0 + 32* yPos,33,33,GraphicsUnit.Pixel);

            return image;
        }

        //Закидывает бомбы на карту
        private static void SeedMap()
        {
            Random r = new Random();

            for (int i = 0; i < numberOfBombs; i++)
            {
                int posI = r.Next(0, mapSize - 1);
                int posJ = r.Next(0, mapSize - 1);
                while (map[posI,posJ] == -1 || (Math.Abs(posI-firstCoord.Y)<=1 && Math.Abs(posJ - firstCoord.X) <= 1)) // чтоб бомбы рядом не заспавнились
                {
                    posI = r.Next(0, mapSize - 1);
                    posJ = r.Next(0, mapSize - 1);
                }
                map[posI, posJ] = -1;
            }
        }

        private static void ShowAllBombs(int iBomb, int jBomb)
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (i == iBomb && j == jBomb)
                        continue;
                    //если это бомба, рисуем её
                    if (map[i, j] == -1)
                    {
                        buttons[i,j].Image = FindNeededImage(3, 2);
                    }
                }
            }
        }

        private static void CountCellBomb()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (map[i,j] == -1) // если здесь бомба
                    {
                        for (int k = i-1; k < i + 2; k++)// пробегаем вокруг неё
                        {
                            for (int l = j-1; l < j+2; l++)
                            {
                                if (!IsInBorder(k, l) || map[k, l] == -1) // если не в границах или бомба
                                    continue;
                                map[k, l] += 1;
                            }
                        }
                    }    
                }
            }
        }

        private static void OpenCells(int i, int j)
        {
            OpenCell(i, j);
            if (map[i, j] > 0)
                return;
            for (int k = i - 1; k < i + 2; k++)// пробегаем вокруг
            {
                for (int l = j - 1; l < j + 2; l++)
                {
                    if (!IsInBorder(k, l)) // если не в границах
                        continue;

                    if (!buttons[k, l].Enabled) // если кнопка выключена - продолжаем
                        continue;

                    if (map[k, l] == 0) // если пустая
                        OpenCells(k, l); // продолжаем рекурсивно открывать ячейки

                    else if (map[k, l] > 0)
                        OpenCell(k, l); // открываем одну ячейку
                }
            }
        }

        private static void OpenCell(int i, int j)
        {
            buttons[i, j].Enabled = false; // выключаем видимость нажатой кнопки

            //открываем определённую картинку для текущей мины
            switch (map[i, j])
            {
                case 1:
                    buttons[i, j].Image = FindNeededImage(1, 0);
                    countOfSteps++;
                    break;
                case 2:
                    buttons[i, j].Image = FindNeededImage(2, 0);
                    countOfSteps++;
                    break;
                case 3:
                    buttons[i, j].Image = FindNeededImage(3, 0);
                    countOfSteps++;
                    break;
                case 4:
                    buttons[i, j].Image = FindNeededImage(4, 0);
                    countOfSteps++;
                    break;
                case 5:
                    buttons[i, j].Image = FindNeededImage(0, 1);
                    countOfSteps++;
                    break;
                case 6:
                    buttons[i, j].Image = FindNeededImage(1, 1);
                    countOfSteps++;
                    break;
                case 7:
                    buttons[i, j].Image = FindNeededImage(2, 1);
                    countOfSteps++;
                    break;
                case 8:
                    buttons[i, j].Image = FindNeededImage(2, 2);
                    countOfSteps++;
                    break;
                case -1:
                    buttons[i, j].Image = FindNeededImage(1, 2);
                    countOfSteps++;
                    break;
                case 0:
                    buttons[i, j].Image = FindNeededImage(0, 0);
                    countOfSteps++;
                    break;
            }

        }

        //Находятся ли координаты в пределах карты
        private static bool IsInBorder(int i, int j)
        {
            if (i< 0 || j < 0 || j > mapSize - 1 || i > mapSize - 1)
                return false;
            return true;
        }

    }
}
