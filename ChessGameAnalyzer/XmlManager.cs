using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ChessGameAnalyzer
{
    /// <summary>
    /// Manages basic operations on the xml file 
    /// </summary>
    class XmlManager
    {
        public XmlReader LoadFile(string fileName)
        {
            try
            {
                XmlReader reader = XmlReader.Create(fileName);
                return reader;
            }
            catch
            {
                Environment.Exit(1);
            }
            return null;
        }

        public void SaveFile(string fileName)
        {

        }
    }
}
