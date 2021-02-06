using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public class Chess
    {
        private string fen;
        Board board;
        List<FigureMoving> allPossibleMoves;
        Moves moves;

        public string GetFen()
        {
            return fen;
        }

        public Chess(string _fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 0")
        {
            fen = _fen;
            board = new Board(fen);
            moves = new Moves(board);
        }

        Chess (Board board)
        {
            this.board = board;
            this.fen = board.Fen;
            moves = new Moves(board);
        }

        public Chess Move(string move)//Example: move = Ke2e4 (K = king, e2 = old move, e4 = new move)
        {
            FigureMoving fm = new FigureMoving(move);//fm = from, to, figure, promotion

            if (!moves.CanMove(fm))
            {
                return this;
            }

            if (board.IsCheckAfterMove(fm))
            {
                return this; //the same board
            }

            Board nextBoard = board.Move(fm); //отвечает за ход, содержит всю инфу о доске, массив фигур, фен от которого отталкивается при пост новой доски, после хода

            Chess nextChess = new Chess(nextBoard);//запускает игру

            return nextChess;
        }

        public char GetFigureAt(int x, int y)
        {
            Square square = new Square(x , y);

            Figure f = board.GetFigureAt(square);

            var resFigure = f == Figure.none ? '.' : (char)f;

            return resFigure;
        }

        void FindAllMoves()
        {
            allPossibleMoves = new List<FigureMoving>();

            foreach (FigureOnSquare fs in board.YieldFigures())
            {
                foreach (Square to in Square.YieldSquares())
                {
                    FigureMoving fm = new FigureMoving(to, fs);
                    if (moves.CanMove(fm))
                    {
                        if (!board.IsCheckAfterMove(fm))
                        {
                            allPossibleMoves.Add(fm);
                        }
                    }
                }
            }
        }

        public List<string> GetAllMoves()
        {
            FindAllMoves();
            List<string> list = new List<string>();

            foreach (FigureMoving fm in allPossibleMoves)
            {
                list.Add(fm.ToString());
            }

            return list;
        }

        public bool IsCheck() => board.IsCheck();
    }
}
