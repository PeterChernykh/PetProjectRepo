using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessGame;

namespace ChessDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Chess chess = new Chess("rnbqkbnr/1p1111p1/8/8/8/8/1P1111P1/RNBQKBNR w KQkq - 0 0");

            while (true)
            {
                Console.WriteLine(chess.GetFen());
                foreach (string moves in chess.GetAllMoves())
                {
                    Console.WriteLine(moves +"\t");
                }
                Console.WriteLine();
                Console.WriteLine("> ");
                Console.WriteLine(ChessToAscii(chess));
                string move = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(move))
                {
                    chess = chess.Move(move);
                }
            }
        }

        static string ChessToAscii (Chess chess)
        {
            string text = "  +-----------------+\n";
            for (int y = 7; y >= 0; y--)
            {
                text += y + 1;
                text += " | ";
                for (int x =0; x<=7; x++)
                {
                    text += chess.GetFigureAt(x, y) + " ";
                }

                text += "|\n";

            }

            text += "  +-----------------+ \n";
            return text;

        }
    }
}
