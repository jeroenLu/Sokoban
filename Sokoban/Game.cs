﻿using System;
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


        internal void StartGame()
        {
            ObjectMover = new ObjectMover(Board, Player);
        }

        public void AddBoard(BaseField[,] board)
        {
            Board = new Board(board);
            // tijdelijk hier show aan roepen, later verplaatsen --------------------------------------------
            Board.ShowBoard();
        }

        public Player GetPlayer() { return Player; }

        private void PrintIntro()
        {
            Console.WriteLine("Welcome to SOKOBAN");
        }

        public void Move(Direction direction)
        {
            Console.Clear();
            ObjectMover.TryMove(direction);
            Console.WriteLine(direction);
            Board.ShowBoard();
        }
    }
}
