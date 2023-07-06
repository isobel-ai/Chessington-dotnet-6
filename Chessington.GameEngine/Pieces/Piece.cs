using System.Collections.Generic;

namespace Chessington.GameEngine.Pieces
{
    public abstract class Piece
    {
        protected Piece(Player player)
        {
            Player = player;
        }

        public Player Player { get; private set; }

        public abstract IEnumerable<Square> GetAvailableMoves(Board board);

        public void MoveTo(Board board, Square newSquare)
        {
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);
        }
        
        protected void GetLongitudinalMoves(Board board, Square currentSquare, List<Square> moveSquares, int range, int direction)
        {
            for (var i = 1; i <= range; i++)
            {
                if (0 <= currentSquare.Row + i * direction && currentSquare.Row + i * direction < GameSettings.BoardSize)
                {
                    moveSquares.Add(Square.At(currentSquare.Row + i * direction, currentSquare.Col));
                }
            }
        }
        protected void GetDiagonalMoves(Board board, Square currentSquare, List<Square> moveSquares, int range)
        {
            for (var vDir = -1; vDir <= 1; vDir += 2)
            {
                for (var hDir = -1; hDir <= 1; hDir += 2)
                {
                    for (var i = 1; i <= range; i++)
                    {
                        var addRow = currentSquare.Row + vDir * i;
                        var addCol = currentSquare.Col + hDir * i;
                        if (addRow >= 0 && addCol >= 0 && addRow < GameSettings.BoardSize && addCol < GameSettings.BoardSize)
                        {
                            moveSquares.Add(Square.At(addRow, addCol));
                        }   
                    }
                }
            }
        }

        protected void GetLateralMoves(Board board, Square currentSquare, List<Square> moveSquares, int range)
        {
            for (var i = 1; i <= range; i++)
            {
                if (0 <= currentSquare.Col + i && currentSquare.Col + i < GameSettings.BoardSize)
                {
                    moveSquares.Add(Square.At(currentSquare.Row, currentSquare.Col + i));
                }
                if (0 <= currentSquare.Col - i && currentSquare.Col - i < GameSettings.BoardSize)
                {
                    moveSquares.Add(Square.At(currentSquare.Row, currentSquare.Col - i));
                }
            }  
        }
    }
}