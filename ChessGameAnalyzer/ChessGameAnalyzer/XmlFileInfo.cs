using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameAnalyzer
{
    /// <summary>
    /// Gets the necessary data from the user
    /// </summary>
    static class XmlFileInfo
    {
        public static string GetFileName()
        {
            string fileName;
            while (true)
            {
                Display.WriteText("Enter name of the file: ");
                fileName = Console.ReadLine();
                if (!fileName.Contains("xml"))
                {
                    fileName += ".xml";
                }
                if (File.Exists(fileName))
                {
                    break;
                }
            }
            return fileName;
        }

        public static int GetEngineLinesCount(int maxEngineLines)
        {
            int engineLines;
            while (true)
            {
                Display.WriteText("Enter lines count you want see: ");
                int.TryParse(Console.ReadLine(), out engineLines);
                if (engineLines > 0)
                {
                    if (engineLines > maxEngineLines)
                    {
                        engineLines = maxEngineLines;
                    }
                    break;
                }
            }
            return engineLines;
        }
    }
}
