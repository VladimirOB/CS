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
        public const int cellSize = 50; // размер клетки

        public static int[,] map = new int[mapSize, mapSize];
        public static Button[,] buttons = new Button[mapSize, mapSize];

        public static Image spriteSet;

        private static Form1 form;

        static bool AIFirstStep = false;



        public static void Init(Form1 current)
        {
            AIFirstStep = false;
            form = current;
            spriteSet = new Bitmap("tiles.png");
            ConfigureMapSize(current);
            InitMap();
            InitButtons(current);
        }

        private static void ConfigureMapSize(Form1 current)
        {
            current.Width = mapSize * cellSize + 20;
            current.Height = (mapSize+1) * cellSize;
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
        private static void InitButtons(Form1 current)
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
            if (map[iButton, jButton] == 0)
            {
                map[iButton, jButton] = 1;
                pressedButton.Image = FindNeededImage(1, 0);
                if (!AIFirstStep)
                    AIFirst(iButton, jButton); // первый ход AI
                else
                AI(iButton, jButton);
            }
            CheckWin();
        }

        //AI (artificial intelligence) — это искусственный интеллект.
        static void AI(int i, int j)
        {
            bool flag = AICheckDanger(i, j);

            if (!flag)
            {
                if(!AICheckWinPos())
                AIStep(i, j);
            }
                
        }

        static bool AICheckWinPos()
        {
            int resX = 0, resY = 0;
            //вертикаль
            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize - 4; x++)
                {
                    int cntO = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (map[x + i, y] != 0)
                        {
                            if (map[x + i, y] == 2)
                            {
                                cntO++;
                                resX = x + i;
                                resY = y;
                            }

                        }
                    }
                    if (cntO == 4)
                    {
                        if (resX + 1 < mapSize)
                        {
                            if (map[resX + 1, resY] == 0)
                            {
                                map[resX + 1, resY] = 2;
                                buttons[resX + 1, resY].Image = FindNeededImage(2, 0);
                                return true;
                            }
                        }
                    }
                }
            }
            
            //горизонт
            for (int x = 0; x < mapSize; x++)
            {
                for (int y = 0; y < mapSize - 4; y++)
                {
                    int cntO = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (map[x, y + 1] != 0)
                        {
                            if (map[x, y + i] == 2)
                            {
                                cntO++;
                                resX = x;
                                resY = y + i;
                            }

                        }
                    }
                    if (cntO == 4)
                    {
                        if (resY + 1 < mapSize)
                        {
                            if (map[resX, resY + 1] == 0)
                            {
                                map[resX, resY + 1] = 2;
                                buttons[resX, resY + 1].Image = FindNeededImage(2, 0);
                                return true;
                            }
                        }
                    }
                }
            }
            
            return false;
        }

        static void AIFirst(int iButton, int jButton)
        {
            Random rand = new Random();
            switch (rand.Next(0, 5))
            {
                case 0:
                    if (iButton - 1 < mapSize && iButton - 1 > 0 && jButton - 1 < mapSize && jButton - 1 > 0)
                    {
                        map[iButton - 1, jButton - 1] = 2;
                        buttons[iButton - 1, jButton - 1].Image = FindNeededImage(2, 0);
                    }
                    else
                        AIFirst(iButton, jButton);
                    break;
                case 1:
                    if (iButton < mapSize && iButton > 0 && jButton - 1 < mapSize && jButton - 1 > 0)
                    {
                        map[iButton, jButton - 1] = 2;
                        buttons[iButton, jButton - 1].Image = FindNeededImage(2, 0);
                    }
                    else
                        AIFirst(iButton, jButton);
                    break;
                case 2:
                    if (iButton - 1 < mapSize && iButton - 1 > 0 && jButton + 1 < mapSize && jButton + 1 > 0)
                    {
                        map[iButton - 1, jButton + 1] = 2;
                        buttons[iButton - 1, jButton + 1].Image = FindNeededImage(2, 0);
                    }
                    else
                        AIFirst(iButton, jButton);
                    break;
                case 3:
                    if (iButton + 1 < mapSize && iButton + 1 > 0 && jButton < mapSize && jButton > 0)
                    {
                        map[iButton + 1, jButton] = 2;
                        buttons[iButton + 1, jButton].Image = FindNeededImage(2, 0);
                    }
                    else
                        AIFirst(iButton, jButton);
                    break;
                case 4:
                    if (iButton + 1 < mapSize && iButton + 1 > 0 && jButton + 1 < mapSize && jButton + 1 > 0)
                    {
                        map[iButton + 1, jButton + 1] = 2;
                        buttons[iButton + 1, jButton + 1].Image = FindNeededImage(2, 0);
                    }
                    else
                        AIFirst(iButton, jButton);
                    break;
            }
            AIFirstStep = true;
        }

        static bool AICheckDanger(int hPos, int wPos)
        {
            int resX = 0, resY = 0;

            if (AICheckDanger2x2(hPos, wPos)) // **_** вот такие проверки
                return true;

            //вертикаль
            if (hPos + 2 < mapSize && hPos - 1 > 0)
            {
                for (int j = hPos - 2; j < hPos + 1; j++)
                {
                    int cntX = 0;

                    for (int k = 0; k < 3; k++)
                    {
                        if (map[j + k, wPos] == 1)
                        {
                            cntX++;
                            resX = j + k;
                            resY = wPos;
                        }

                    }
                    if (cntX == 3)
                    {
                        if (resX + 1 < mapSize)
                        {
                            if (map[resX + 1, resY] != 1)
                            {
                                map[resX + 1, resY] = 2;
                                buttons[resX + 1, resY].Image = FindNeededImage(2, 0);
                                return true;
                            }
                            else
                            {
                                if (resX - 3 > 0)
                                {
                                    if (map[resX-3, resY] != 1)
                                    {
                                        map[resX - 3, resY] = 2;
                                        buttons[resX - 3, resY].Image = FindNeededImage(2, 0);
                                        return true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (map[resX-3, resY] != 1)
                            {
                                map[resX - 3, resY] = 2;
                                buttons[resX - 3, resY].Image = FindNeededImage(2, 0);
                                return true;
                            }

                        }
                    }
                }
            }

            //горизонталь
            if (wPos + 2 < mapSize && wPos - 1 > 0)
            {
                for (int j = wPos - 2; j < wPos + 1; j++)
                {
                    int cntX = 0;

                    for (int k = 0; k < 3; k++)
                    {
                        if (map[hPos, j + k] == 1)
                        {
                            cntX++;
                            resX = hPos;
                            resY = j + k;
                        }

                    }
                    if (cntX == 3)
                    {
                        if (resY + 1 < mapSize)
                        {
                            if (map[resX, resY+1] != 1)
                            {
                                map[resX, resY+1] = 2;
                                buttons[resX, resY+1].Image = FindNeededImage(2, 0);
                                return true;
                            }
                            else
                            {
                                if (resY - 3 > 0)
                                {
                                    if (map[resX, resY - 3] != 1)
                                    {
                                        map[resX, resY - 3] = 2;
                                        buttons[resX, resY - 3].Image = FindNeededImage(2, 0);
                                        return true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (map[resX, resY - 3 ] != 1)
                            {
                                map[resX, resY - 3] = 2;
                                buttons[resX, resY - 3].Image = FindNeededImage(2, 0);
                                return true;
                            }

                        }
                    }
                }
            }

            //диагональ направо
            if (hPos + 2 < mapSize && hPos - 1 > 0 && wPos + 2 < mapSize && wPos - 1 > 0)
            {
                if (map[hPos - 1, wPos - 1] == 1 || map[hPos + 1, wPos + 1] == 1)
                {
                    if (map[hPos + 2, wPos + 2] == 1)
                    {
                        if (map[hPos - 1, wPos - 1] != 1)
                        {
                            map[hPos - 1, wPos - 1] = 2;
                            buttons[hPos - 1, wPos - 1].Image = FindNeededImage(2, 0);
                            return true;
                        }
                    }
                    else if (map[hPos - 2, wPos - 2] == 1)
                    {
                        if (map[hPos +1, wPos +1] != 1)
                        {
                            map[hPos + 1, wPos + 1] = 2;
                            buttons[hPos + 1, wPos + 1].Image = FindNeededImage(2, 0);
                            return true;
                        }
                    }
                }
            }

            //диагональ налево
            if (hPos + 2 < mapSize && hPos - 1 > 0 && wPos + 2 < mapSize && wPos - 1 > 0)
            {
                if (map[hPos+1, wPos -1] == 1 || map[hPos - 1, wPos + 1] == 1)
                {
                    if (map[hPos+2,wPos-2] ==1)
                    {
                        if (map[hPos-1, wPos+1] != 1)
                        {
                            map[hPos - 1, wPos + 1] = 2;
                            buttons[hPos - 1, wPos + 1].Image = FindNeededImage(2, 0);
                            return true;
                        }
                    }
                    else if (map[hPos-2,wPos+2] == 1)
                    {
                        if (map[hPos + 1, wPos -1] != 1)
                        {
                            map[hPos + 1, wPos - 1] = 2;
                            buttons[hPos + 1, wPos - 1].Image = FindNeededImage(2, 0);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        static bool AICheckDanger2x2(int x, int y)
        {
            int cnt = 0;
            int hPos = x, wPos = y;
            Stack<int> stack = new Stack<int>();
            Stack<int> stack2 = new Stack<int>();

            stack.Push(hPos-2);
            //вертикаль
            while (cnt <= 3)
            {
                hPos = stack.Pop();
                if (hPos + 2 < mapSize && hPos - 2 > 0)
                {
                    if (map[hPos-2, wPos] == 1 && map[hPos-1, wPos] == 1 && map[hPos+1, wPos] == 1 && map[hPos+2, wPos] == 1)
                    {
                        if (map[hPos, wPos] != 1)
                            map[hPos, wPos] = 2;
                        buttons[hPos, wPos].Image = FindNeededImage(2, 0);
                        return true;
                    }
                }
                stack.Push(++hPos);
                cnt++;
            }
            cnt = 0;
            stack.Clear();
            hPos = x; wPos = y;
            stack.Push(wPos - 2);
            //горизонт
            while (cnt <= 3)
            {
                wPos = stack.Pop();
                if (wPos + 2 < mapSize && wPos - 2 > 0)
                {
                    if (map[hPos, wPos - 2] == 1 && map[hPos, wPos - 1] == 1 && map[hPos, wPos + 1] == 1 && map[hPos, wPos + 2] == 1)
                    {
                        if (map[hPos, wPos] != 1)
                            map[hPos, wPos] = 2;
                        buttons[hPos, wPos].Image = FindNeededImage(2, 0);
                        return true;
                    }
                }
                stack.Push(++wPos);
                cnt++;
            }
            cnt = 0;
            stack.Clear();
            hPos = x; wPos = y;
            stack.Push(wPos - 2);
            stack2.Push(hPos - 2);
            //диагональ направо
            while (cnt <= 3)
            {
                wPos = stack.Pop();
                hPos = stack2.Pop();
                if (wPos + 2 < mapSize && wPos - 2 > 0 && hPos + 2 < mapSize && hPos - 2 > 0)
                {
                    if (map[hPos-2, wPos - 2] == 1 && map[hPos-1, wPos - 1] == 1 && map[hPos+1, wPos + 1] == 1 && map[hPos+2, wPos + 2] == 1)
                    {
                        if (map[hPos, wPos] != 1)
                            map[hPos, wPos] = 2;
                        buttons[hPos, wPos].Image = FindNeededImage(2, 0);
                        return true;
                    }
                }
                stack.Push(++wPos);
                stack2.Push(++hPos);
                cnt++;
            }

            cnt = 0;
            stack.Clear();
            stack2.Clear();
            hPos = x; wPos = y;
            stack.Push(wPos + 2);
            stack2.Push(hPos - 2);
            //диагональ налево
            while (cnt <= 3)
            {
                wPos = stack.Pop();
                hPos = stack2.Pop();
                if (wPos + 2 < mapSize && wPos - 2 > 0 && hPos + 2 < mapSize && hPos - 2 > 0)
                {
                    if (map[hPos - 2, wPos + 2] == 1 && map[hPos - 1, wPos + 1] == 1 && map[hPos + 1, wPos - 1] == 1 && map[hPos + 2, wPos - 2] == 1)
                    {
                        if (map[hPos, wPos] != 1)
                            map[hPos, wPos] = 2;
                        buttons[hPos, wPos].Image = FindNeededImage(2, 0);
                        return true;
                    }
                }
                stack.Push(--wPos);
                stack2.Push(++hPos);
                cnt++;
            }

            return false;
        }

        static void AIStep(int hPos, int wPos)
        {
            int resX = 0, resY = 0;
            //вертикаль
            if (hPos + 2 < mapSize && hPos - 1 > 0)
            {
                for (int y = 0; y < mapSize; y++)
                {
                    for (int x = 0; x < mapSize - 4; x++)
                    {
                        int cntO = 0;
                        for (int i = 0; i < 5; i++) 
                        {
                            if (map[x + i, y] != 0)
                            {
                                if (map[x + i, y] == 2)
                                {
                                    cntO++;
                                    resX = x + i;
                                    resY = y;
                                }
                                    
                            }
                        }
                        if (cntO >= 1)
                        {
                            if(resX + 1 < mapSize)
                            {
                                if (map[resX +1, resY] == 0)
                                {
                                    map[resX + 1, resY] = 2;
                                    buttons[resX + 1, resY].Image = FindNeededImage(2, 0);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            //горизонт
            if (wPos + 2 < mapSize && wPos - 1 > 0)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    for (int y = 0; y < mapSize - 4; y++)
                    {
                        int cntO = 0;
                        for (int i = 0; i < 5; i++)
                        {
                            if (map[x, y + 1 ] != 0)
                            {
                                if (map[x, y + i] == 2)
                                {
                                    cntO++;
                                    resX = x;
                                    resY = y + i;
                                }

                            }
                        }
                        if (cntO >= 1)
                        {
                            if (resY + 1 < mapSize)
                            {
                                if (map[resX, resY+1] == 0)
                                {
                                    map[resX, resY+1] = 2;
                                    buttons[resX, resY+1].Image = FindNeededImage(2, 0);
                                    return;

                                }
                            }
                        }
                    }
                }
            }

        }

        
        static void CheckWin()
        {
            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize-4; x++) // тут минус 4, чтоб не было выхода за границы массива
                {
                    int cntX = 0, cntO = 0;
                    //вертикаль
                    for (int i = 0; i < 5; i++) // тут добираем те 4 что отняли.
                    {
                        if (map[x+i, y] != 0)
                        {
                            if (map[x + i, y] == 1)
                            {
                                cntX++;
                            }
                            else
                                cntO++;
                        }
                    }
                    if(cntX == 5)
                    {
                        MessageBox.Show("Вы победили!");
                        form.Restart();
                    }
                    if (cntO == 5)
                    {
                        MessageBox.Show("Вы проиграли!");
                        form.Restart();
                    }
                }
            }

            for (int x = 0; x < mapSize; x++)
            {
                for (int y = 0; y < mapSize - 4; y++)
                {
                    int cntX = 0, cntO = 0;
                    //горизонталь
                    for (int i = 0; i < 5; i++)
                    {
                        if (map[x, y+i] != 0)
                        {
                            if (map[x, y+i] == 1)
                            {
                                cntX++;
                            }
                            else
                                cntO++;
                        }
                    }
                    if (cntX == 5)
                    {
                        MessageBox.Show("Вы победили!");
                    }
                    if (cntO == 5)
                    {
                        MessageBox.Show("Вы проиграли!");
                    }
                }
            }

            for (int x = 0; x < mapSize-4; x++)
            {
                for (int y = 0; y < mapSize - 4; y++)
                {
                    int cntX = 0, cntO = 0;
                    //диагональ сверху вниз
                    for (int i = 0; i < 5; i++)
                    {
                        if (map[x+i, y + i] != 0)
                        {
                            if (map[x+i, y + i] == 1)
                            {
                                cntX++;
                            }
                            else
                                cntO++;
                        }
                    }
                    if (cntX == 5)
                    {
                        MessageBox.Show("Вы победили!");
                    }
                    if (cntO == 5)
                    {
                        MessageBox.Show("Вы проиграли!");
                    }
                }
            }

            for (int y = 4; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize - 4; x++)
                {
                    int cntX = 0, cntO = 0;
                    //диагональ снизу вверх
                    for (int i = 0; i < 5; i++)
                    {
                        if (map[x + i, y - i] != 0)
                        {
                            if (map[x + i, y - i] == 1)
                            {
                                cntX++;
                            }
                            else
                                cntO++;
                        }
                    }
                    if (cntX == 5)
                    {
                        MessageBox.Show("Вы победили!");
                    }
                    if (cntO == 5)
                    {
                        MessageBox.Show("Вы проиграли!");
                    }
                }
            }
        }

        public static Image FindNeededImage(int xPos, int yPos)
        {
            Image image = new Bitmap(cellSize, cellSize);
            Graphics g = Graphics.FromImage(image);
            //вырезать конкретную картинку из атласа
            g.DrawImage(spriteSet, new Rectangle( new Point(0, 0), new Size(cellSize, cellSize)), 0 + 32 * xPos, 0 + 32 * yPos, 33, 33, GraphicsUnit.Pixel);

            return image;
        }
    }
}
