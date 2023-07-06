using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player) 
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);
            var moveSquares = new List<Square>();
            var direction = Player == Player.White ? -1 : 1;
            
            GetLongitudinalMoves(board, currentSquare, moveSquares, 1, direction);
            if (!board.MovedPawns.Contains(this))
            {
                GetLongitudinalMoves(board, currentSquare, moveSquares, 2, direction);
            }

            var diagonal1 = Square.At(currentSquare.Row + 1 * direction, currentSquare.Col + 1 * direction);
            var diagonal2 = Square.At(currentSquare.Row + 1 * direction, currentSquare.Col - 1 * direction);

            if (IsOnBoard(diagonal1) && !IsEmpty(board, diagonal1) && SquaresHaveOpposingPlayers(board, currentSquare, diagonal1))
            {
                moveSquares.Add(diagonal1);
            }
            if (IsOnBoard(diagonal2) && !IsEmpty(board, diagonal2) && SquaresHaveOpposingPlayers(board, currentSquare, diagonal2))
            {
                moveSquares.Add(diagonal2);
            }
            
            return moveSquares;
        }
    }
}