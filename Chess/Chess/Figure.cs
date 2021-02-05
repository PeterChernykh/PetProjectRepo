using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    enum Figure
    {
        none,
        WhiteKing = 'K',
        WhiteQueen = 'Q',
        WhiteRook = 'R',
        WhiteBishop = 'B',
        WhiteKnight = 'N',
        WhitePawn = 'P',

        BlackKing = 'k',
        BlackQueen = 'q',
        BlackRook = 'r',
        BlackBishop = 'b',
        BlackKnight = 'n',
        BlackPawn = 'p'

    }

    static class FigureMethods
    {
        public static Color GetColor(this Figure figure)
        {
            if(figure == Figure.none)
            {
                return Color.none;
            }

            Color color = (char.IsUpper((char)figure)) ? Color.white : Color.black;

            return color;
        }
    }
}
