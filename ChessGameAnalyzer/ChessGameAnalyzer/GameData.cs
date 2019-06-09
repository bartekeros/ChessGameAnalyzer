using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameAnalyzer
{
    /// <summary>
    /// Stores basic game data
    /// </summary>
    struct GameData
    {
        public string WhitePlayer { get; set; }
        public string BlackPlayer{ get; set; }
        public string Result { get; set; }

        public GameData(string whitePlayer, string blackPlayer, string result)
        {
            this.WhitePlayer = whitePlayer;
            this.BlackPlayer = blackPlayer;
            this.Result = result;
        }

        public GameData(GameData gameData)
        {
            this.WhitePlayer = gameData.WhitePlayer;
            this.BlackPlayer = gameData.BlackPlayer;
            this.Result = gameData.Result;
        }
    }
}