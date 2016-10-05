using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class ObjectMover
    {
        private Board board;
        private Player player;
        private int playerX;
        private int playerY;
        public ObjectMover(Board board, Player player)
        {
            this.board = board;
            this.player = player;
            SearchPlayer(board.LoadedBoard);
            
        }

        public void SearchPlayer(BaseField[,] board)
        {
            for(int y = 0; y < board.GetLength(1); y++)
            {
                for(int x = 0; x < board.GetLength(0); x++)
                {
                    if(board[x,y].Object?.GetType() == typeof(Player))
                    {
                        playerX = x;
                        playerY = y;
                    }
                }
            }



        }

        internal void TryMove(Direction direction)
        {
            if (!ValidMove(direction)) return;
            switch (direction)
            {
                case Direction.UP:
                    board.LoadedBoard[playerX, playerY - 1].Object = player;
                    board.LoadedBoard[playerX, playerY].Object = null;
                    playerY--;
                    break;

                case Direction.DOWN:
                    board.LoadedBoard[playerX, playerY + 1].Object = player;
                    board.LoadedBoard[playerX, playerY].Object = null;
                    playerY++;
                    break;

                case Direction.LEFT:
                    board.LoadedBoard[playerX - 1, playerY].Object = player;
                    board.LoadedBoard[playerX, playerY].Object = null;
                    playerX--;
                    break;

                case Direction.RIGHT:
                    board.LoadedBoard[playerX + 1, playerY].Object = player;
                    board.LoadedBoard[playerX, playerY].Object = null;
                    playerX++;
                    break;
            }
            
        }

        private bool ValidMove(Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    if (board.LoadedBoard[playerX, playerY - 1].GetType() == typeof(Wall)) return false;
                    if (board.LoadedBoard[playerX, playerY - 1].Object?.GetType() == typeof(Box))
                    {
                        if (board.LoadedBoard[playerX, playerY - 2].GetType() == typeof(Wall) || board.LoadedBoard[playerX, playerY - 2].Object?.GetType() == typeof(Box)) return false;
                        else { MoveBox(direction); }
                       
                    }
                    break;

                case Direction.DOWN:
                    if (board.LoadedBoard[playerX, playerY + 1].GetType() == typeof(Wall)) return false;
                    if (board.LoadedBoard[playerX, playerY + 1].Object?.GetType() == typeof(Box))
                    {
                        if (board.LoadedBoard[playerX, playerY + 2].GetType() == typeof(Wall) || board.LoadedBoard[playerX, playerY + 2].Object?.GetType() == typeof(Box)) return false;
                        else { MoveBox(direction); }
                    }
                    break;

                case Direction.LEFT:
                    if (board.LoadedBoard[playerX - 1, playerY].GetType() == typeof(Wall)) return false;
                    if (board.LoadedBoard[playerX - 1, playerY].Object?.GetType() == typeof(Box))
                    {
                        if (board.LoadedBoard[playerX - 2, playerY].GetType() == typeof(Wall) || board.LoadedBoard[playerX - 2, playerY].Object?.GetType() == typeof(Box)) return false;
                        else { MoveBox(direction); }
                    }
                    break;

                case Direction.RIGHT:
                    if (board.LoadedBoard[playerX + 1, playerY].GetType() == typeof(Wall)) return false;
                    if (board.LoadedBoard[playerX + 1, playerY].Object?.GetType() == typeof(Box))
                    {
                        if (board.LoadedBoard[playerX + 2, playerY].GetType() == typeof(Wall) || board.LoadedBoard[playerX + 2, playerY].Object?.GetType() == typeof(Box)) return false;
                        else { MoveBox(direction); }
                    }
                    break;
            }
            return true;
        }

        private void MoveBox(Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    board.LoadedBoard[playerX, playerY - 2].Object = board.LoadedBoard[playerX, playerY - 1].Object;
                    break;

                case Direction.DOWN:
                    board.LoadedBoard[playerX, playerY + 2].Object = board.LoadedBoard[playerX, playerY + 1].Object;
                    break;

                case Direction.LEFT:
                    board.LoadedBoard[playerX - 2, playerY].Object = board.LoadedBoard[playerX - 1, playerY].Object;
                    break;

                case Direction.RIGHT:
                    board.LoadedBoard[playerX + 2, playerY].Object = board.LoadedBoard[playerX + 1, playerY].Object;
                    break;
            }
            
        }
    }
}
