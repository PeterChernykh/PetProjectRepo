using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    class Moves
    {
        FigureMoving fm;

        Board board;

        public Moves(Board board)
        {
            this.board = board;
        }

        public bool CanMove(FigureMoving fm)
        {
            this.fm = fm;

            bool result = CanMoveFrom() &&
                CanMoveTo() &&
                CanFigureMove();

            return result;
        }

        bool CanMoveFrom()
        {
            bool isCanMove = fm.From.IsOnBoard() &&
                fm.Figure.GetColor() == board.MoveColor;

            return isCanMove;
        }

        bool CanMoveTo()
        {
            bool isCanMove = false;
            if (fm.To.IsOnBoard() &&
                fm.From != fm.To &&
            board.GetFigureAt(fm.To).GetColor() != board.MoveColor)
            {
                isCanMove = true;
            }

            return isCanMove;
        }

        bool CanFigureMove()
        {
            switch (fm.Figure)
            {
                case Figure.WhiteKing:
                case Figure.BlackKing:
                    return CanKingMove();

                case Figure.WhiteQueen:
                case Figure.BlackQueen:
                    return CanMoveStraight();

                case Figure.WhiteRook:
                case Figure.BlackRook:
                    return false;
                case Figure.WhiteBishop:
                case Figure.BlackBishop:
                    return false;

                case Figure.WhiteKnight:
                case Figure.BlackKnight:
                    return CanKnightMove();

                case Figure.WhitePawn:
                case Figure.BlackPawn:
                    return false;

                default: return false;
            }
        }

        bool CanKingMove()
        {
            bool canKingMove = 
                fm.AbsDeltaX <= 1 && fm.AbsDeltaY <= 1 ? true: false;

            return canKingMove;
        }

        bool CanKnightMove()
        {
            bool canKnightMove = 
                fm.AbsDeltaX == 1 && fm.AbsDeltaY == 2 ? true:
                fm.AbsDeltaX == 2 && fm.AbsDeltaY == 1 ? true:
                false;

            return canKnightMove;
        }

        bool CanMoveStraight()
        {
            Square from = fm.From;
            do
            {
                from = new Square(from.X + fm.SignX, from.Y + fm.SignY);
                return true;
            }
            while (from.IsOnBoard() && board.GetFigureAt(from) == Figure.none);

            return false;
        }






    }
}
