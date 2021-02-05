using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    class FigureOnSquare
    {
        public Figure Figure {get;set;} //TODO: сделать свойство полем и сделать его приватным. Сделать методы для лвозврата.
        public Square Square { get; set; }

        public FigureOnSquare(Figure figure, Square square)
        {
            Figure = figure;
            Square = square;
        }
    }
}
