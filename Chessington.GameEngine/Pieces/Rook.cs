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

            GetLateralMoves(board, currentSquare, moveSquares, GameSettings.BoardSize);
            GetLongitudinalMoves(board, currentSquare, moveSquares, GameSettings.BoardSize, 1);
            GetLongitudinalMoves(board, currentSquare, moveSquares, GameSettings.BoardSize, -1);

            return moveSquares;
        }
    }
}