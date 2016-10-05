using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class Game
    {
        Board Board;
        ObjectMover ObjectMover;
        Player Player;

        public Game()
        {
            Player = new Player();
            PrintIntro();
        }


        internal void CreateObjectMover()
        {
            ObjectMover = new ObjectMover(Board, Player);
        }

        public void AddBoard(BaseField[,] board)
        {
            Board = new Board(board);
            Board.ShowBoard();
        }

        public Player GetPlayer() { return Player; }

        private void PrintIntro()
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("| Welkom bij Sokoban                              |");
            Console.WriteLine("|                                                 |");
            Console.WriteLine("| betekenis van de symbolen  | doel van het spel  |");
            Console.WriteLine("|                            |                    |");
            Console.WriteLine("| # : muur                   | duw met de truck   |");
            Console.WriteLine("| . : vloer                  | de krat(ten)       |");
            Console.WriteLine("| o : krat                   | naar de bestemming |");
            Console.WriteLine("| 0 : krat op bestemming     |                    |");
            Console.WriteLine("| x : bestemming             | s = stop           |");
            Console.WriteLine("| @ : truck                  | r = reset          |");
            Console.WriteLine("|_________________________________________________|");
        }

        public void Move(Direction direction)
        {
            Console.Clear();
            ObjectMover.TryMove(direction);
            Board.ShowBoard();
        }
    }
}
