using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    enum Color
    {
        none,

        black = 'B',

        white = 'W'
    }

    static class ColorMethod
    {
        public static Color FlipColor(this Color color)
        {
            Color clr =
               (color == Color.black) ? Color.white :
               (color == Color.white) ? Color.black :
               Color.none;

            return clr;
        }
    }

}
