using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    class FigureMoving
    {
        public Figure Figure { get; set; }
        public Square From { get; set; }
        public Square To { get; set; }
        public Figure Promotion { get; set; }

        public FigureMoving(Square to, FigureOnSquare figureOnSquare, Figure promotion = Figure.none)
        {
            To = to;
            From = figureOnSquare.Square;
            Figure = figureOnSquare.Figure;
            Promotion = promotion;

        }

        public FigureMoving(string move)
        {
            Figure = (ChessGame.Figure)move[0];//смотрим на этот символ, как на фигуру
            From = new Square(move.Substring(1, 2));
            To = new Square(move.Substring(3, 2));
            Promotion = move.Length == 6 ? (ChessGame.Figure)move[5] : Figure.none;

        }

        public int DeltaX { get { return To.X - From.X; } }
        public int DeltaY { get { return To.Y - From.Y; } }

        public int AbsDeltaX { get { return Math.Abs(DeltaX); } }//возвращает абсолютное число / модуль числа 
        public int AbsDeltaY { get { return Math.Abs(DeltaY); } }

        public int SignX { get { return Math.Sign(DeltaX); } } //возвращает знак
        public int SignY { get { return Math.Sign(DeltaY); } }
    }
}
