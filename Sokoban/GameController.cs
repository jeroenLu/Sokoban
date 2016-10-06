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
        private bool _solved;


        internal void SetUpGame()
        {
            while (true)
            {
                Game = new Game();
                LoadBoard();
                Console.Clear();
                Game.AddBoard(LoadedBoard);
                Game.CreateObjectMover();
                
                while (!_solved)
                {
                    Direction direction = ReadInput();
                    if (direction != Direction.INVALID)
                    {
                        Game.Move(direction);
                        CheckGameOver();
                    }
                    else
                    {
                        Console.WriteLine("Invalid move");
                    }
                }
                Console.WriteLine("Gefelicteerd, level opgelost!!! Druk op een toets voor menu of s om te stoppen");
                Console.ReadKey();
                Console.Clear();
                _solved = false;
            }
        }

        private void CheckGameOver()
        {
            _solved = true;
            for (var y = 0; y < LoadedBoard.GetLength(1); y++)
            {
                for (var x = 0; x < LoadedBoard.GetLength(0); x++)
                {
                    if (LoadedBoard[x, y]?.GetType() != typeof(EndField)) continue;
                    if (LoadedBoard[x, y].Object?.GetType() != typeof(Box))
                    {
                        _solved = false;
                    }
                }
            }
        }

        private Direction ReadInput()
        {
            ConsoleKeyInfo input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.UpArrow:    return Direction.UP;
                case ConsoleKey.DownArrow:  return Direction.DOWN;
                case ConsoleKey.LeftArrow:  return Direction.LEFT;
                case ConsoleKey.RightArrow: return Direction.RIGHT;
                case ConsoleKey.R:          Console.Clear(); SetUpGame(); break;
            }
            if (input.Key == ConsoleKey.S)
            {
                Environment.Exit(0);
            }
            return Direction.INVALID;
        }

        private void LoadBoard()
        {
            var validatedLevel = false; 
            while (!validatedLevel)
            {
                Console.WriteLine("Selecteer het level dat je wil spelen en druk op Enter (1 - 4)");
                string path = ((Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"levels\doolhof")) + Console.ReadLine())+ ".txt";
                
                try
                {
                    string[] fileLines = File.ReadAllLines(path);
                    _levelHeight = fileLines.Length;

                    for (var i = 0; i < _levelHeight; i++)
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
                        for (var x = 0; x < line.Length; x++)
                        {
                            switch (line.ElementAt(x))
                            {
                                case '#':
                                    LoadedBoard[x, _levelHeight] = new Wall();
                                    break;
                                case 'o':
                                    LoadedBoard[x, _levelHeight] = new Field { Object = new Box() };
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
                    validatedLevel = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("De opgegeven file kan niet worden geleden, probeer opnieuw");

                }
            }
        }
    }
}
