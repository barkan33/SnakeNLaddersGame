using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeNlLaddersGame
{
    class SnakeAndLaddersGame
    {
        public int BoardSize { get; private set; }
        public int DiceSides { get; private set; }
        public List<Player> Players { get; private set; }
        public List<Structure> Ladders { get; private set; }
        public List<Structure> Snakes { get; private set; }
        public List<int> GoldenCells { get; private set; }

        public SnakeAndLaddersGame(int boardSize, int diceSides, List<Player> players, List<Structure> ladders, List<Structure> snakes, List<int> goldenCells)
        {
            BoardSize = boardSize;
            DiceSides = diceSides;
            Players = players;
            Ladders = ladders;
            Snakes = snakes;
            GoldenCells = goldenCells;
        }
        public void PlayGame()
        {
            Random random = new Random();
            int currentPlayerIndex = 0;

            IEnumerable<Structure> structures = Ladders.Concat(Snakes);

            while (true)
            {
                Console.WriteLine($"\nIt's {Players[currentPlayerIndex].Name}'s turn!");

                int diceRoll1 = random.Next(1, 7);
                int diceRoll2 = random.Next(1, 7);
                Console.WriteLine($"Dice rolls: {diceRoll1}, {diceRoll2}");

                Players[currentPlayerIndex].Position += diceRoll1 + diceRoll2;


                if (Players[currentPlayerIndex].Position > BoardSize)
                {
                    Players[currentPlayerIndex].Position = BoardSize;
                    break;
                }


                if (GoldenCells.Contains(Players[currentPlayerIndex].Position))
                {
                    int leadingPlayerIndex = Players.IndexOf(Players.OrderBy(p => p.Position).Last());

                    if (Players[leadingPlayerIndex] == Players[currentPlayerIndex])
                    {
                        leadingPlayerIndex = Players.IndexOf(Players.OrderBy(p => p.Position).First());
                    }

                    Console.WriteLine($"You landed on a Golden Cell! Switching places with {Players[leadingPlayerIndex].Name}");
                    (Players[currentPlayerIndex].Position, Players[leadingPlayerIndex].Position) = (Players[leadingPlayerIndex].Position, Players[currentPlayerIndex].Position);
                }


                foreach (Structure structure in structures)
                {
                    if (Players[currentPlayerIndex].Position == structure.Start)
                    {
                        //Ladder or Snake
                        if (structure.Start < structure.End)
                            Console.WriteLine($"Congratulations! Ladder! You climb up from cell {Players[currentPlayerIndex].Position} to cell {structure.End}");

                        else
                            Console.WriteLine($"Oops! Snake! You slide down from cell {Players[currentPlayerIndex].Position} to cell {structure.End}");

                        Players[currentPlayerIndex].Position = structure.End;
                        break;
                    }
                }

                Console.WriteLine($"Current position: {Players[currentPlayerIndex].Position}");

                currentPlayerIndex = (currentPlayerIndex + 1) % Players.Count;
            }
            Console.WriteLine($"\nCongratulations, {Players[currentPlayerIndex].Name}, you won!");
        }
    }
}
