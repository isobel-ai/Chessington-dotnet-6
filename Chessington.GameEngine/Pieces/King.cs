using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class King : Piece
    {
        public King(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);
            var moveSquares = new List<Square>();
            
            GetLateralMoves(board, currentSquare, moveSquares, 1);
            GetLongitudinalMoves(board, currentSquare, moveSquares, 1, 1);
            GetLongitudinalMoves(board, currentSquare, moveSquares, 1, -1);
            GetDiagonalMoves(board, currentSquare, moveSquares, 1);

            return moveSquares;
        }
    }
}