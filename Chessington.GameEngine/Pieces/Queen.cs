using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Queen : Piece
    {
        public Queen(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);
            var moveSquares = new List<Square>();

            for (var i = 0; i < GameSettings.BoardSize; i++)
                moveSquares.Add(Square.At(currentSquare.Row, i));

            for (var i = 0; i < GameSettings.BoardSize; i++)
                moveSquares.Add(Square.At(i, currentSquare.Col));

            // Get rid of our starting location.
            moveSquares.RemoveAll(s => s == currentSquare);
            
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