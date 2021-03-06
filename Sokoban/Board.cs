﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class Board
    {
        private long width;
        private int height;
        public BaseField[,] LoadedBoard { get; set; }


        public Board(BaseField[,] board)
        {
            LoadedBoard = board;
        }

        public void ShowBoard()
        {
            width = LoadedBoard.GetLength(0);
            height = LoadedBoard.GetLength(1);

            for (var i = 0; i < height; i++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (LoadedBoard[x, i] == null)
                    {
                        Console.Write(" ");
                        continue;
                    }
                    if (LoadedBoard[x, i].GetType() == typeof(Wall))
                    {
                        Console.Write("#");
                        continue;
                    }
                    if (LoadedBoard[x, i].GetType() == typeof(Field))
                    {
                        if(LoadedBoard[x,i].Object?.GetType() == typeof(Player))
                        {
                            Console.Write("@");
                            continue;
                        }
                        if (LoadedBoard[x, i].Object?.GetType() == typeof(Box))
                        {
                            Console.Write("o");
                            continue;
                        }
                        Console.Write(".");
                        continue;
                    }
                    if (LoadedBoard[x, i].GetType() == typeof(EndField))
                    {
                        if (LoadedBoard[x, i].Object?.GetType() == typeof(Player))
                        {
                            Console.Write("@");
                            continue;
                        }
                        else if (LoadedBoard[x, i].Object?.GetType() == typeof(Box))
                        {
                            Console.Write("0");
                            continue;
                        }
                        Console.Write("x");
                        continue;
                    }
                    if (LoadedBoard[x, i].GetType() != typeof(BaseField)) continue;
                    Console.Write(" ");
                    continue;
                }
                Console.WriteLine();
            }
        }
    }
}
