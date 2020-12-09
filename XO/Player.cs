using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO
{
    public class Player
    {
        public Player(string name, char symbol)
        {
            name = PlayerName;
            PlayerSymbol = symbol;
        }

        public string PlayerName { get; }
        public char PlayerSymbol { get; }
    }
}
