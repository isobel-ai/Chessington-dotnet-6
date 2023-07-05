using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);
            var moveSquares = new List<Square>();

            // Moving SW
            var addRow = currentSquare.Row - 1;
            var addCol = currentSquare.Col - 1;
            while (addRow >= 0 && addCol >= 0)
            {
                moveSquares.Add(Square.At(addRow--, addCol--));
            }
            
            // Moving SE
            addRow = currentSquare.Row - 1;
            addCol = currentSquare.Col + 1;
            while (addRow >= 0 && addCol < GameSettings.BoardSize)
            {
                moveSquares.Add(Square.At(addRow--, addCol++));
            }
            
            // Moving NW
            addRow = currentSquare.Row + 1;
            addCol = currentSquare.Col - 1;
            while (addRow < GameSettings.BoardSize && addCol >= 0)
            {
                moveSquares.Add(Square.At(addRow++, addCol--));
            }
            
            // Moving NE
            addRow = currentSquare.Row + 1;
            addCol = currentSquare.Col + 1;
            while (addRow < GameSettings.BoardSize && addCol < GameSettings.BoardSize)
            {
                moveSquares.Add(Square.At(addRow++, addCol++));
            }
            
            return moveSquares;
        }
    }
}