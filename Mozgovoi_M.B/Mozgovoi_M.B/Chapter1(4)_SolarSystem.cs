using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Mozgovoi_M.B_
{
    class Chapter1_SolarSystem
    {
        private const int screenWidth = 60;
        private const int screenHeight = 30;
        bool isRuning = false;
        Random random = new Random();

        int earthX, earthY; // координаты Земли
        double earthA; // текущий угол отклонения Земли
        int moonX, moonY; // координаты Луны
        double moonA; // т. угол отклонения Луны

        const int sunR = 1; // радиус Солнца
        const int earthR = 1; // радиус Земли
        const int moonR = 1; // радиус Луны                              
        const int earthD = 5; // расстояние от солнца до Земли
        const int moonD = 2; // радиус от Земли до Луны
        double earthV = 0.02; // угловая скорость Земли
        double moonV = 0.1; // угловая скорость Луны

        public void Start()
        {
            Console.WriteLine("Press enter to start: ");
            earthA = 0; moonA = 0; // инициализация
            earthX = 0; earthY = 0;
            moonX = 0; moonY = 0;
            
            while (true)
            {
                if (Console.KeyAvailable) // если нажата клавиша
                {
                    var key = Console.ReadKey().Key; // читаем её
                    if (key == ConsoleKey.Enter)
                    {
                        isRuning = true;
                        Console.Clear();
                    }
                    if (key == ConsoleKey.Escape)
                    {
                        isRuning = false;
                        Environment.Exit(0);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(screenWidth / 2 - sunR, screenHeight / 2 - sunR);
                Console.WriteLine("☼");

                if (isRuning)
                {
                    if(earthX > 0 && moonX > 0)
                    {
                        Console.SetCursorPosition(earthX - earthR, earthY - earthR); // стираем шар
                        Console.Write(" ");
                        Console.SetCursorPosition(moonX - moonR, moonY - moonR); // стираем шар
                        Console.Write(" ");
                    }

                    //пересчитываем коорды Земли и Луны
                    earthX = (int)Math.Round(screenWidth / 2 + earthD * Math.Cos(earthA)); ;
                    earthY = (int)Math.Round(screenHeight / 2 + earthD * Math.Sin(earthA)); ;
                    moonX = (int)Math.Round(earthX + moonD * Math.Cos(moonA));
                    moonY = (int)Math.Round(earthY + moonD * Math.Sin(moonA));

                    earthA = earthA + earthV; // изменяем текущие углы отклонения
                    moonA = moonA + moonV; // Земли и Луны

                    if (earthA > 2 * Math.PI) // корректируем углы если требуется
                        earthA = earthA - 2 * Math.PI;

                    if(moonA > 2 * Math.PI) 
                        moonA = moonA - 2* Math.PI;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(earthX - earthR, earthY - earthR); // рисуем шар
                    Console.Write("O");

                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.SetCursorPosition(moonX - moonR, moonY - moonR); // рисуем шар
                    Console.Write("☻");

                    Thread.Sleep(30); // пауза
                }
            }

        }
    }
}
