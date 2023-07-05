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

            switch (Player)
            {
                case Player.White:
                    moveSquares.Add(Square.At(currentSquare.Row - 1, currentSquare.Col));
                    if (!board.MovedPawns.Contains(this))
                    {
                        moveSquares.Add(Square.At(currentSquare.Row - 2, currentSquare.Col));
                    }
                    break;
                default: // Player.Black
                    moveSquares.Add(Square.At(currentSquare.Row + 1, currentSquare.Col));
                    if (!board.MovedPawns.Contains(this))
                    {
                        moveSquares.Add(Square.At(currentSquare.Row + 2, currentSquare.Col));
                    }
                    break;
            }
            
            return moveSquares;
        }
    }
}