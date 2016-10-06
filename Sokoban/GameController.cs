using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class GameController
    {
        private Game Game;
        private BaseField[,] LoadedBoard;
        private int _levelWidth, _levelHeight;
        private bool _playing;


        internal void SetUpGame()
        {
            Game = new Game();

            //Kan evt mooier door loadboard een array te laten returnen ipv deze eerst op te slaan ??? 
            LoadBoard();
            Console.Clear();
            Game.AddBoard(LoadedBoard);
            _playing = true;
            Game.CreateObjectMover();
            while (_playing)
            {
                Direction direction = ReadInput();
                if (direction != Direction.INVALID)
                {
                    Game.Move(direction);
                    if (CheckGameOver())
                    {
                        _playing = false;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid move");
                }
            }
            Console.WriteLine("Congratulations!! Thank you for playing Sokoban");
            Console.ReadKey();
            
        }

        private bool CheckGameOver()
        {
            bool GameOver = true;
            for (int y = 0; y < LoadedBoard.GetLength(1); y++)
            {
                for (int x = 0; x < LoadedBoard.GetLength(0); x++)
                {
                    if (LoadedBoard[x, y]?.GetType() == typeof(EndField))
                    {
                        if (LoadedBoard[x, y].Object?.GetType() != typeof(Box))
                        {
                            GameOver = false;
                        }
                    }
                }
            }
            return GameOver;
        }

        private Direction ReadInput()
        {
            ConsoleKeyInfo input = Console.ReadKey();
            if(input.Key == ConsoleKey.UpArrow) { return Direction.UP; }
            if (input.Key == ConsoleKey.DownArrow) { return Direction.DOWN; }
            if (input.Key == ConsoleKey.LeftArrow) { return Direction.LEFT; }
            if (input.Key == ConsoleKey.RightArrow) { return Direction.RIGHT; }
            if (input.Key == ConsoleKey.R) { Console.Clear(); SetUpGame(); }
            if (input.Key == ConsoleKey.S) { Environment.Exit(0); }
            return Direction.INVALID;
        }

        private void LoadBoard()
        {
            var chosen = false;
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"levels\doolhof");

            while (!chosen)
            {
                Console.WriteLine("Please input the level you want to select (1 - 4)");

                // Hier wordt input van de user gevraagd welk level hij wil laden
                char selectedLevel;
                if (char.TryParse(Console.ReadLine(), out selectedLevel))
                {
                    path += selectedLevel + ".txt";
                    chosen = true;
                }
                else
                {
                    // Kan later evt weggehaald worden -------------------------------------------------------
                    Console.WriteLine("Not a valid input! ");
                }
            }



            // Hier moeten nog exceptions worden afgevangen!!!! -------------------------------------------------------------

            string[] fileLines = File.ReadAllLines(path);

            _levelHeight = fileLines.Length;
            for (int i = 0; i < _levelHeight; i++)
            {
                if (fileLines[i].Length > _levelWidth)
                {
                    _levelWidth = fileLines[i].Length;
                }
            }

            LoadedBoard = new BaseField[_levelWidth, _levelHeight];

            _levelHeight = 0;
            foreach (var line in fileLines)
            {
                for (int x = 0; x < line.Length; x++)
                {
                    switch (line.ElementAt(x))
                    {
                        case '#':
                            LoadedBoard[x, _levelHeight] = new Wall();
                            break;
                        case 'o':
                            LoadedBoard[x, _levelHeight] = new Field {Object = new Box()};
                            break;
                        case '.':
                            LoadedBoard[x, _levelHeight] = new Field();
                            break;
                        case 'x':
                            LoadedBoard[x, _levelHeight] = new EndField();
                            break;
                        case '@':
                            LoadedBoard[x, _levelHeight] = new Field { Object = Game.GetPlayer() };
                            break;
                        default:
                            LoadedBoard[x, _levelHeight] = new BaseField();
                            break;
                    }
                }
                _levelHeight++;
            }
        }

    }
}
