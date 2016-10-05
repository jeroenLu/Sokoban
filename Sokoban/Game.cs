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
        private bool playing;

        public Game()
        {
            
            ObjectMover = new ObjectMover(Board);
            Player = new Player();
            playing = true;
        }


        internal void StartGame()
        {
            PrintIntro();

            Console.ReadKey();
            Console.Clear();
            
            Console.ReadKey();
        }

        public void AddBoard(BaseField[,] board)
        {
            Board = new Board(board);
            // tijdelijk hier show aan roepen, later verplaatsen --------------------------------------------
            Board.ShowBoard();
            Console.ReadKey();
        }

        public Player GetPlayer() { return Player; }

        private void PrintIntro()
        {
            Console.WriteLine("Welcome to SOKOBAN");
        }
    }
}
