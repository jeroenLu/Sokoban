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


        internal void SetUpGame()
        {
            Game = new Game();
            Game.StartGame();
            LoadBoard();
            Console.ReadKey();
            for (int i = 0; i < _levelHeight; i++)
            {
                for (int x = 0; x < _levelWidth; x++)
                {
                    Console.Write(LoadedBoard[x, i]);
                }
                Console.WriteLine();
            }
            Console.ReadKey();

        }

        private void LoadBoard()
        {
            var chosen = false;
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"levels\doolhof");

            while (!chosen)
            {
                Console.WriteLine("Please input the level you want to select");

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
            _levelWidth = fileLines[0].Length;

            Console.WriteLine("This is height " + _levelHeight);
            Console.WriteLine("This is width " + _levelWidth);

            LoadedBoard = new BaseField[_levelWidth, _levelHeight];

            _levelHeight = 0;
            foreach (var line in fileLines)
            {
                for (int x = 0; x < _levelWidth; x++)
                {
                    switch (line.ElementAt(x))
                    {
                        case '#':
                            LoadedBoard[x, _levelHeight] = new Wall();
                            break;
                        case 'o':
                            LoadedBoard[x, _levelHeight] = new Field(new Box());
                            break;
                        case '.':
                            LoadedBoard[x, _levelHeight] = new Field();
                            break;
                        case 'x':
                            LoadedBoard[x, _levelHeight] = new EndField();
                            break;
                        case '@':
                            LoadedBoard[x, _levelHeight] = new Field(new Player());
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
