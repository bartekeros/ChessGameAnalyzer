using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameAnalyzer
{
    class Program
    {
        const int MaxEngineLines = 10;

        static void Main(string[] args)
        {
            string fileName = XmlFileInfo.GetFileName();
            Console.WriteLine();
            int engineLines = XmlFileInfo.GetEngineLinesCount(MaxEngineLines);
            Console.WriteLine();

            EvaluatedGameAnalyzer analyzer = new EvaluatedGameAnalyzer(fileName, engineLines);
            analyzer.AnalyzeEngineLinePercentage();
            
            Console.Clear();
            Display.WriteMovesPercentage(analyzer);
            Console.ReadKey();
        }

        

      
    }
}
