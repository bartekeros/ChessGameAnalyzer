using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameAnalyzer
{
    /// <summary>
    /// Displays data in the console
    /// </summary>
    static class Display
    {
        public static void WriteMovesPercentage(EvaluatedGameAnalyzer analyzer)
        {
            ColorManager colorManager = new ColorManager();

            for (int i = 0; i < analyzer.GameNumber; i++)
            {
                Console.WriteLine("Game: {0}\n", i + 1);
                Console.WriteLine(analyzer.GameDatabase[i + 1].WhitePlayer + " - " + analyzer.GameDatabase[i + 1].BlackPlayer);
                Console.WriteLine(analyzer.GameDatabase[i + 1].Result);
                for (int j = 0; j < analyzer.WhitePlayedEngineLinePercentage.GetLength(1); j++)
                {
                    colorManager.ColorText(ConsoleColor.Black);
                    colorManager.ColorBackground(ConsoleColor.Gray);
                    Console.WriteLine(j + 1 + " line: " + analyzer.WhitePlayedEngineLinePercentage[i, j]);
                    colorManager.ResetColor();
                }
                Console.WriteLine();
                for (int j = 0; j < analyzer.BlackPlayedEngineLinePercentage.GetLength(1); j++)
                {
                    Console.WriteLine(j + 1 + " line: " + analyzer.BlackPlayedEngineLinePercentage[i, j]);
                    colorManager.ResetColor();
                }
                colorManager.ColorText(ConsoleColor.DarkGreen);
                Console.WriteLine("\n------------------------------------------------\n");
                colorManager.ResetColor();
            }
        }

        public static void WriteText(string text)
        {
            Console.Write(text);
        }
    }
}
