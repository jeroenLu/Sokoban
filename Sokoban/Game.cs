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
            Board = new Board();
            ObjectMover = new ObjectMover(Board);
            Player = new Player();
            playing = true;
        }


        internal void StartGame()
        {
            PrintIntro();
            Console.ReadKey();
            Console.Clear();
            
        }

        private void PrintIntro()
        {
            Console.WriteLine("Welcome to SOKOBAN");
        }
    }
}
