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
            return moveSquares;
        }
    }
}