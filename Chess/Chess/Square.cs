using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    struct Square
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public static Square none = new Square(-1, -1);

        public Square(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Square(string position)
        {
            if(position.Length==2&&
                position[0]>= 'a'&&
                position[0]<= 'h'&&
                position[1]>= '1'&&
                position[1]<= '8')
            {
                X = position[0] - 'a';
                Y = position[1] - '1';
            }
            else
            {
                this = none;
            }
        }

        public bool IsOnBoard() => X >= 0 && X <= 8 && Y >= 0 && Y <= 8;

        public static bool operator == (Square a, Square b) => a.X == b.X && a.Y == b.Y;

        public static bool operator != (Square a, Square b) => a.X != b.X && a.Y != b.Y;

        public static IEnumerable<Square> YieldSquares()
        {
            for(int x = 0; x<8; x++)
            {
                for (int y = 0; y<8; y++)
                {
                    yield return new Square(x , y);
                }
            }
        }
    }
}
