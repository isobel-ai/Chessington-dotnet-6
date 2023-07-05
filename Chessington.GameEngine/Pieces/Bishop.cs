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

            for (var vDir = -1; vDir <= 1; vDir += 2)
            {
                for (var hDir = -1; hDir <= 1; hDir += 2)
                {
                    var addRow = currentSquare.Row + vDir;
                    var addCol = currentSquare.Col + hDir;
                    while (addRow >= 0 && addCol >= 0 && addRow < GameSettings.BoardSize && addCol < GameSettings.BoardSize)
                    {
                        moveSquares.Add(Square.At(addRow, addCol));
                        addRow += vDir;
                        addCol += hDir;
                    }
                }
            }

            return moveSquares;
        }
    }
}