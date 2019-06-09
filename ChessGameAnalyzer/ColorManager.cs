using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameAnalyzer
{
    /// <summary>
    /// Manages colors
    /// </summary>
    class ColorManager
    {
        public void ColorText(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public void ColorBackground(ConsoleColor color)
        {
            Console.BackgroundColor = color;
        }

        public void ResetColor()
        {
            Console.ResetColor();
        }
    }
}
