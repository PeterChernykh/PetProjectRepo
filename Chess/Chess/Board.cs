using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    class Board
    {
         public string Fen { get; private set; }
         Figure[,] Figures;
         public Color MoveColor { get; private set; }

         public int MoveNumber { get; set; }

        public Board(string fen)
        {
            Fen = fen;
            Figures = new Figure[8,8];
            Init();
        }

        void Init()
        {
            string[] parts = Fen.Split();
            if (parts.Length != 6)
            {
                return;
            }

            InitFigures(parts[0]);

            MoveColor = parts[1] == "b" ? Color.black : Color.white;

            MoveNumber = int.Parse(parts[5]);
        }

        void GenerateFen()
        {
            if (MoveColor == Color.black)
            {
                MoveNumber = MoveNumber + 1;
            }

            string figures = FenFigures() + " ";

            string moveColor = MoveColor == Color.white ? "w" + " " : "b" + " ";

            string castle = "- ";

            string enPassant = "- ";

            string draw = "0 ";

            string moveNumber = MoveNumber.ToString();

            string res = figures + moveColor + castle + enPassant + draw + moveNumber;

            Fen = res;
        }

        string FenFigures()
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    sb.Append(Figures[x, y] == Figure.none ? '1' : (char)Figures[x, y]);
                }

                if (y > 0)
                {
                    sb.Append('/');
                }
            }

            return sb.ToString();
        }

        void InitFigures(string data)
        {
            for (int j = 8; j>=2; j--)
            {
                data = data.Replace(j.ToString(), (j - 1).ToString() + "1");
            }

            data = data.Replace("1", ".");

            string[] lines = data.Split('/');

            for ( int y = 7; y>= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    Figures[x, y] = lines[7-y][x] == '.'? Figure.none: (Figure)lines[7 - y][x];
                }
            }
        }

        public Figure GetFigureAt(Square square)
        {
            Figure figureAt = Figure.none;

            if (square.IsOnBoard())
            {
                figureAt = Figures[square.X, square.Y];

            }

            return figureAt;
        }

        void SetFigureAt(Square square, Figure figure)
        {
            if (square.IsOnBoard())
            {
                Figures[square.X, square.Y] = figure;
            }
        }

        public Board Move(FigureMoving fm)
        {
            Board next = new Board(Fen);

            Figure figure = (fm.Promotion == Figure.none) ? fm.Figure : fm.Promotion;

            next.SetFigureAt(fm.From, Figure.none); //remove the figure from the previous square
            next.SetFigureAt(fm.To, figure);//set new figure on the correspoding square

            next.MoveColor = MoveColor.FlipColor();

            next.GenerateFen();

            return next;

        }

        public IEnumerable<FigureOnSquare> YieldFigures()
        {
            foreach (Square square in Square.YieldSquares())
            {
                if (GetFigureAt(square).GetColor() == MoveColor)
                {
                    yield return new FigureOnSquare(GetFigureAt(square), square);
                }
            }
        }
    }
}
