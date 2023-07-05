using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Rook : Piece
    {
        public Rook(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);
            var moveSquares = new List<Square>();

            for (var i = 0; i < GameSettings.BoardSize; i++)
            {
                moveSquares.Add(Square.At(currentSquare.Row, i));
                moveSquares.Add(Square.At(i, currentSquare.Col));
            }
            
            // Get rid of our starting location.
            moveSquares.RemoveAll(s => s == currentSquare);

            return moveSquares;
        }
    }
}