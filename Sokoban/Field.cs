using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    class Field : BaseField
    {
        private Box box;
        private Player player;

        public Field()
        {
        }

        public Field(Player player)
        {
            this.player = player;
        }

        public Field(Box box)
        {
            this.box = box;
        }
    }
}
