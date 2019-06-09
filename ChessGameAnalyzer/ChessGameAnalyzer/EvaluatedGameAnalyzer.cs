using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace ChessGameAnalyzer
{
    /// <summary>
    /// Interprets and makes operations on Xml file
    /// </summary>
    class EvaluatedGameAnalyzer : XmlManager
    {
        private readonly string fileName;

        public List<string> GameNotation { get; private set; }
        public Dictionary<int,KeyValuePair<int,MoveValue>> EvaluatedMoves { get; private set; }
        public MoveValue EvaluatingMove { get; private set; }
        public string[,] WhitePlayedEngineLinePercentage { get; private set; }
        public string[,] BlackPlayedEngineLinePercentage { get; private set; }
        public Dictionary<int, GameData> GameDatabase { get; private set; }
        private Player player;
        private XmlReader reader;
        private GameData gameData;
        private int gameCounter = 1;
        //Arguments ready for usage in arrays (decrement by 1)
        private int evaluatedLines;
        public int GameNumber { get; private set; }
        private int currentGameNumber = -1;

        public EvaluatedGameAnalyzer(string fileName, int evaluatedLines)
        {
            this.fileName = fileName;
            CountGamesNumber();

            this.evaluatedLines = evaluatedLines;
            GameNotation = new List<string>();
            EvaluatedMoves = new Dictionary<int, KeyValuePair<int,MoveValue>>();
            WhitePlayedEngineLinePercentage = new string[GameNumber, this.evaluatedLines];
            BlackPlayedEngineLinePercentage = new string[GameNumber, this.evaluatedLines];
            GameDatabase = new Dictionary<int, GameData>();
            gameData = new GameData();
        }

        public void AnalyzeEngineLinePercentage()
        {
            reader = LoadFile(fileName);
            int counter = 1;
            int subCounter = 1;

            while(reader.Read())
            {
                if(reader.NodeType == XmlNodeType.Element)
                {
                    if(reader.Name == "move")
                    {
                        GetPlayerColor();
                    }

                    else if(reader.Name == "evaluation")
                    {
                        EvaluatingMove = GetEvaluationMove();
                    }

                    else if(reader.Name == "played")
                    {
                        GameNotation?.Add(GetPlayedMove());
                        subCounter = 1;
                    }

                    else if(reader.Name == "tag")
                    {
                        FillGameData();
                    }
                }

                else if (reader.Name == "game" && reader.NodeType == XmlNodeType.EndElement)
                {
                    currentGameNumber++;
                    for (int i = 0; i < evaluatedLines; i++)
                    {
                        WhitePlayedEngineLinePercentage[currentGameNumber, i] = CalculatePercentage(i + 1, Player.white).ToString("P1");
                        BlackPlayedEngineLinePercentage[currentGameNumber, i] = CalculatePercentage(i + 1, Player.black).ToString("P1");
                    }
                }

                if (EvaluatingMove != new MoveValue())
                {
                    EvaluatedMoves.Add(counter, new KeyValuePair<int, MoveValue>(subCounter, EvaluatingMove));
                    counter++;
                    subCounter++;
                }
                EvaluatingMove = new MoveValue();
            }
        }

        private void FillGameData()
        {
            if (reader.GetAttribute(0) == "White")
            {
                gameData.WhitePlayer = reader.GetAttribute("value");
            }
            else if (reader.GetAttribute(0) == "Black")
            {
                gameData.BlackPlayer = reader.GetAttribute("value");
            }
            else if (reader.GetAttribute(0) == "Result")
            {
                gameData.Result = reader.GetAttribute(1);
                GameDatabase.Add(gameCounter, gameData);
                gameCounter++;
                gameData = new GameData();
            }
        }

        private double CalculatePercentage(int lineNumber, Player player)
        {
            int sum = 0;
            int whiteMovesCount = 0;
            int BlackMovesCount = 0;
            int moveCount = 0;

            for (int i = 1; i < EvaluatedMoves.Keys.Count; i++)
            {
                moveCount = whiteMovesCount + BlackMovesCount;
                if (EvaluatedMoves[i].Value.Player == player)
                {
                    if (EvaluatedMoves[i].Value.Move == GameNotation[moveCount] && EvaluatedMoves[i].Key == lineNumber)
                    {
                        sum += 1;
                    }
                    if (EvaluatedMoves[i+1].Key == 1)
                    {
                        whiteMovesCount++;
                    }
                }
                else if(EvaluatedMoves[i + 1].Key == 1)
                {
                    BlackMovesCount++;
                }
            }
            switch (player)
            {
                case Player.white:
                    return (double)sum / whiteMovesCount;
                case Player.black:
                    return (double)sum / BlackMovesCount;
                default:
                    break;
            }
            return 0;
        }

        private void GetPlayerColor()
        {
            string color = reader.GetAttribute("player").ToString();
            switch (color)
            {
                case "white":
                    player = Player.white;
                    break;
                case "black":
                    player = Player.black;
                    break;
                default:
                    break;
            }
        }

        private MoveValue GetEvaluationMove()
        {
            string move = reader.GetAttribute("move").ToString();
            int value = GetValueAttributeSafe();
            return new MoveValue(move, value, player);
        }

        private string GetPlayedMove()
        {
            return reader.ReadInnerXml().ToString();
        }

        private int GetValueAttributeSafe()
        {
            string temporaryValue = reader.GetAttribute("value");
            int.TryParse(temporaryValue, out int result);
            if(result == 0)
            {
                if(temporaryValue.Contains("mate"))
                {
                    return 1000 * Math.Sign(int.Parse(Regex.Match(temporaryValue, @"\d+").Value));
                }
                return int.Parse(Regex.Match(temporaryValue, @"\d+").Value);
            }
            return result;
        }

        private void Reset()
        {
            GameNotation = new List<string>();
            EvaluatedMoves = new Dictionary<int, KeyValuePair<int, MoveValue>>();
            EvaluatingMove = new MoveValue();
            WhitePlayedEngineLinePercentage = new string[GameNumber, evaluatedLines];
            BlackPlayedEngineLinePercentage = new string[GameNumber, evaluatedLines];
            player = Player.white;
        }


        private void CountGamesNumber()
        {
            reader = LoadFile(fileName);

            while (reader.Read())
            {
                if(reader.NodeType == XmlNodeType.Element && reader.Name == "game")
                {
                    GameNumber++;
                }
            }
        }

        public void AnalyzePrecision()
        {

        }

    }
}
