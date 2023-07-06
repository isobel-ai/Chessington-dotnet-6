using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Knight : Piece
    {
        public Knight(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);
            var moveSquares = new List<Square>();

            int[] dx = { 1, 1, -1, -1 };
            int[] dy = { 1, -1, 1, -1 };

            for (var i = 0; i < 4; i++)
            {
                var newRow = currentSquare.Row + dx[i];
                var newCol = currentSquare.Col + 2 * dy[i];
                if (newRow >= 0 && newRow < GameSettings.BoardSize && newCol >= 0 && newCol < GameSettings.BoardSize)
                {
                    moveSquares.Add(Square.At(newRow,newCol));
                    (newRow, newCol) = (newCol, newRow);
                    moveSquares.Add(Square.At(newRow,newCol));
                }
            }

            return moveSquares;
        }
    }
}