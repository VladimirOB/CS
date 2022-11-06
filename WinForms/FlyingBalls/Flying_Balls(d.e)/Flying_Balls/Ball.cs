using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flying_Balls
{
    /// <summary>
    /// Класс, хранящий информацию о шарике
    /// </summary>
    class Ball
    {
        /// <summary>
        /// Точная текущая координата X шарика
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Точная текущая координата Y шарика
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Пошаговое смещение шарика по X
        /// </summary>
        public float DeltaX { get; set; }

        /// <summary>
        /// Пошаговое смещение шарика по Y
        /// </summary>
        public float DeltaY { get; set; }

        public Ball(float x, float y, float deltaX, float deltaY)
        {
            this.X = x;
            this.Y = y;
            this.DeltaX = deltaX;
            this.DeltaY = deltaY;
        }
    }
}
