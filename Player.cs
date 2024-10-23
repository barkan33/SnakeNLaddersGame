using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SnakeNlLaddersGame
{
    internal class Player
    {
        public string Name { get; private set; }
        public int Position { get; set; }

        public Player(string name)
        {
            Name = name;
            Position = 1;
        }
    }
}
