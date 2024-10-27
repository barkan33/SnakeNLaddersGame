using System.Linq;

namespace SnakeNlLaddersGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int DiceSides = 6;
            const int NumOfGoldenCells = 2;
            int boardSize = (int)Math.Pow(10, 2);

            Tuple<int, int> playerRange = new Tuple<int, int>(2, 2);
            Tuple<int, int> ladderRange = new Tuple<int, int>(1, 5);
            Tuple<int, int> snakeRange = new Tuple<int, int>(1, 5);


            Console.WriteLine($"Welcome to the Snake and Ladders Game!");
            Console.WriteLine($"The game will be played on a board of size {boardSize}.");

            int numberOfPlayers = false ? IntInput(playerRange.Item1, playerRange.Item2, "Player") : 2;
            List<Player> players = GetPlayers(numberOfPlayers);

            int numberOfLadders = IntInput(ladderRange.Item1, ladderRange.Item2, "Ladders");
            int numberOfSnakes = IntInput(snakeRange.Item1, snakeRange.Item2, "Snakes");

            (List<Structure> snakes, List<Structure> ladders) = GenerateSnakesAndLadders(boardSize, numberOfLadders, numberOfSnakes);

            List<int> goldenCells = GenerateGoldenCells(boardSize, NumOfGoldenCells, ladders, snakes);


            Console.WriteLine("Ladders: ");
            foreach (Structure item in ladders)
                Console.WriteLine(item);
            Console.WriteLine("\nSnakes: ");
            foreach (Structure item in snakes)
                Console.WriteLine(item);


            SnakeAndLaddersGame game = new SnakeAndLaddersGame(boardSize, DiceSides, players, ladders, snakes, goldenCells);
            game.PlayGame();
        }

        public static int IntInput(int min, int max, string item)
        {
            int numberOfItems;
            Console.Write($"Enter the number of {item} ({min}-{max}): ");
            while (!int.TryParse(Console.ReadLine(), out numberOfItems) || (numberOfItems < min || numberOfItems > max))
            {
                Console.WriteLine($"Invalid number of {item}. Please enter a number between {min} and {max}.");
                Console.Write($"Enter the number of {item} ({min}-{max}): ");
            }

            return numberOfItems;
        }

        public static List<Player> GetPlayers(int numberOfPlayers)
        {

            List<Player> players = new List<Player>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.Write($"Enter the name of player {i + 1}: ");
                string name;
                while (true)
                {
                    name = Console.ReadLine();
                    if (name.Trim().Length > 0)
                    {

                        break;
                    }
                    Console.WriteLine("Name cannot be empty. Please enter a valid name.");
                    Console.Write($"Enter the name of player {i + 1}: ");

                }
                players.Add(new Player(name));
            }
            return players;
        }
        public static Tuple<List<Structure>, List<Structure>> GenerateSnakesAndLadders(int boardSize, int numberOfLadders, int numberOfSnakes)
        {
            int width = (int)Math.Sqrt(boardSize);

            List<Structure> ladders = new List<Structure>();
            List<Structure> snakes = new List<Structure>();

            Random random = new Random();

            while (ladders.Count < numberOfLadders)
            {
                int bottom = random.Next(2, boardSize - width + 1);

                int row = (bottom - 1) / width + 1;

                int top = random.Next(row * width + 1, boardSize);

                if (bottom != top && !ladders.Any(ladder => ladder.Start == top || ladder.Start == bottom || ladder.End == top || ladder.End == bottom))
                {
                    ladders.Add(new Structure(bottom, top));
                }
            }

            while (snakes.Count < numberOfSnakes)
            {
                int bottom = random.Next(2, boardSize - width + 1);

                int row = (bottom - 1) / width + 1;

                int top = random.Next(row * width + 1, boardSize);

                if (bottom != top &&
                    !snakes.Any(ladder => ladder.Start == top || ladder.Start == bottom || ladder.End == top || ladder.End == bottom) &&
                    !ladders.Any(ladder => ladder.Start == top || ladder.Start == bottom || ladder.End == top || ladder.End == bottom))
                {
                    snakes.Add(new Structure(top, bottom));
                }
            }

            return new Tuple<List<Structure>, List<Structure>>(snakes, ladders);
        }
        public static List<int> GenerateGoldenCells(int boardSize, int numberOfGoldenCells, List<Structure> ladders, List<Structure> snakes)
        {
            List<int> goldenCells = new List<int>();
            Random random = new Random();

            while (goldenCells.Count < numberOfGoldenCells)
            {
                int cell = random.Next(10, boardSize);
                //Is cell exists
                if (!goldenCells.Contains(cell) &&
                    !ladders.Any(ladder => ladder.Start == cell || ladder.End == cell) &&
                    !snakes.Any(snake => snake.Start == cell || snake.End == cell))
                {
                    goldenCells.Add(cell);
                }
            }
            return goldenCells;
        }

    }
}
