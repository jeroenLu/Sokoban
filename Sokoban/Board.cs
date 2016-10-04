using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class Board
    {
        private LinkedList<BaseField> GameBoard;
        private int width;
        private int height;
        private Dictionary<int, BaseField> FieldType;
        public Board()
        {
            GameBoard = new LinkedList<BaseField>();
            CreateBoard();
        }

       

        private void CreateBoard()
        {
            this.width = 8;
            this.height = 8;


        }


    }

    
}
