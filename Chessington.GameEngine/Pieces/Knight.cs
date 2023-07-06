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
                if (IsOnBoard(Square.At(newRow, newCol)))
                {
                    if (IsEmpty(board, Square.At(newRow, newCol)) || SquaresHaveOpposingPlayers(board, currentSquare, Square.At(newRow, newCol)))
                    {
                        moveSquares.Add(Square.At(newRow,newCol));
                    }
                    (newRow, newCol) = (newCol, newRow);
                    if (IsEmpty(board, Square.At(newRow, newCol)) || SquaresHaveOpposingPlayers(board, currentSquare, Square.At(newRow, newCol)))
                    {
                        moveSquares.Add(Square.At(newRow,newCol));
                    }
                }
            }

            return moveSquares;
        }
    }
}