using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameAnalyzer
{
    public enum Player { white, black }

    /// <summary>
    /// Stores informations about move
    /// </summary>
    struct MoveValue
    {
        public string Move { get; private set; }
        public int Value { get; private set; }
        public Player Player { get; private set; }

        public MoveValue(string move, int value, Player player)
        {
            Move = move;
            Value = value;
            Player = player;
        }

        public static bool operator ==(MoveValue first, MoveValue second)
        {
            if(first.Move == second.Move)
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(MoveValue first, MoveValue second)
        {
            if (first.Move != second.Move)
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MoveValue))
            {
                return false;
            }

            var value = (MoveValue)obj;
            return Move == value.Move &&
                   Value == value.Value;
        }

        public override int GetHashCode()
        {
            var hashCode = 1621229502;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Move);
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            return hashCode;
        }
    }
}
