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

        protected bool IsOnBoard(Square square)
        {
            return (0 <= square.Row && square.Row < GameSettings.BoardSize && 0 <= square.Col &&
                    square.Col < GameSettings.BoardSize);
        }

        private bool IsEmpty(Board board, Square square)
        {
            return board.GetPiece(Square.At(square.Row, square.Col)) == null;
        }
        
        protected void GetLongitudinalMoves(Board board, Square currentSquare, List<Square> moveSquares, int range, int direction)
        {
            for (var i = 1; i <= range; i++)
            {
                if (IsOnBoard(Square.At(currentSquare.Row + i * direction, currentSquare.Col)))
                {
                    if (IsEmpty(board, Square.At(currentSquare.Row + i * direction, currentSquare.Col)))
                    {
                        moveSquares.Add(Square.At(currentSquare.Row + i * direction, currentSquare.Col));
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        
        protected void GetLateralMoves(Board board, Square currentSquare, List<Square> moveSquares, int range)
        {
            for (var direction = -1; direction <= 1; direction += 2)
            {
                for (var i = 1; i <= range; i++)
                {
                    if (IsOnBoard(Square.At(currentSquare.Row, currentSquare.Col + i * direction)))
                    {
                        if (IsEmpty(board, Square.At(currentSquare.Row, currentSquare.Col + i * direction)))
                        {
                            moveSquares.Add(Square.At(currentSquare.Row, currentSquare.Col + i * direction));
                        }
                        else
                        {
                            break;
                        }
                    }
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
                        if (IsOnBoard(Square.At(addRow, addCol)))
                        {
                            if (IsEmpty(board, Square.At(addRow, addCol)))
                            {
                                moveSquares.Add(Square.At(addRow, addCol));
                            }
                            else
                            {
                                break;
                            }
                        }   
                    }
                }
            }
        }
    }
}