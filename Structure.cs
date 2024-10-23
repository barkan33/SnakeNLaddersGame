using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeNlLaddersGame
{
    class Structure
    {
        public int Start { get; private set; }
        public int End { get; private set; }

        public Structure(int start, int end)
        {
            Start = start;
            End = end;
        }
        public override string ToString()
        {
            return $"{Start} -> {End}";
        }
    }

}
