using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class GameController
    {
        Game Game;
        BaseField[,] LoadedBoard;


        internal void SetUpGame()
        {
            Game = new Game();
            Game.StartGame();
            LoadBoard();
            Console.ReadKey();
            for (int i = 0; i < 6; i++)
            {
                for(int x = 0; x < 9; x++)
                {
                    Console.Write(LoadedBoard[x, i]);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
            
        }

        private void LoadBoard()
        {

            System.IO.StreamReader file = new System.IO.StreamReader("C:\\Users\\Jessew\\Documents\\Informatica\\Hoofdfase1\\Modelleren 3\\Sokoban\\Sokoban\\levels\\doolhof1.txt");
            int height = System.IO.File.ReadAllLines(@"C:\\Users\\Jessew\\Documents\\Informatica\\Hoofdfase1\\Modelleren 3\\Sokoban\\Sokoban\\levels\\doolhof1.txt").Count();
            int width = file.ReadLine().Length;
            string line;
            
            Console.WriteLine("This is height " + height);
            Console.WriteLine("This is width " + width);

            LoadedBoard = new BaseField[width, height];
            file = new System.IO.StreamReader("C:\\Users\\Jessew\\Documents\\Informatica\\Hoofdfase1\\Modelleren 3\\Sokoban\\Sokoban\\levels\\doolhof1.txt");
            height = 0;
            while((line = file.ReadLine()) != null)
            {

                for (int x = 0; x < width; x++)
                {
                    switch (line.ElementAt(x))
                    {
                        case '#':
                            LoadedBoard[x, height] = new Wall();
                            break;
                        case 'o':
                            LoadedBoard[x, height] = new Field(new Box());
                            break;
                        case '.':
                            LoadedBoard[x, height] = new Field();
                            break;
                        case 'x':
                            LoadedBoard[x, height] = new EndField();
                            break;
                        case '@':
                            LoadedBoard[x, height] = new Field(new Player());
                            break;
                        default:
                            LoadedBoard[x, height] = new BaseField();
                            break;

                    }
                }
                height++;
            }
                
            
        }
    }
}
