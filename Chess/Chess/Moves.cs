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
                    return (fm.SignX == 0||fm.SignY==0)&&
                        CanMoveStraight();

                case Figure.WhiteBishop:
                case Figure.BlackBishop:
                    return (fm.SignX!= 0 && fm.SignY!= 0)&&
                        CanMoveStraight();

                case Figure.WhiteKnight:
                case Figure.BlackKnight:
                    return CanKnightMove();

                case Figure.WhitePawn:
                case Figure.BlackPawn:
                    return CanPawnMove();

                default: return false;
            }
        }

        bool CanPawnMove()
        {
            if (fm.From.Y<1||fm.From.Y>6)
            {
                return false;
            }

            int stepY = fm.Figure.GetColor() == Color.white ? 1 : -1;

            return CanPawnGo(stepY) ||
                CanPawnJump(stepY) ||
                CanPawnEat(stepY);

        }

        private bool CanPawnEat(int stepY)
        {
            if (board.GetFigureAt(fm.To) != Figure.none)
                if (fm.AbsDeltaX == 1)
                    if (fm.DeltaY == stepY)
                    {
                        return true;
                    }
            return false;
        }

        private bool CanPawnJump(int stepY)
        {
            if (board.GetFigureAt(fm.To) == Figure.none)
                if (fm.DeltaX == 0)
                    if(fm.DeltaY==2 * stepY)
                        if(fm.From.Y == 1||fm.From.Y==6)
                            if(board.GetFigureAt(new Square(fm.From.X, fm.From.Y + stepY)) == Figure.none)
                            {
                                return true;
                            }
            return false;
        }

        private bool CanPawnGo(int stepY)
        {
            if (board.GetFigureAt(fm.To) == Figure.none)
                if (fm.DeltaX == 0)
                    if (fm.DeltaY == stepY)
                    {
                        return true;
                    }
            return false;
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
                if(from == fm.To)
                {
                    return true;
                }
            }
            while (from.IsOnBoard() && board.GetFigureAt(from) == Figure.none);

            return false;
        }
    }
}
