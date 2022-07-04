using System;
using System.Collections.Generic;
using System.Linq;
using Chessington.GameEngine.Pieces;

namespace Chessington.GameEngine
{
    public class Board
    {
        private readonly Piece[,] _board;
        public Player CurrentPlayer { get; private set; }
        public IList<Piece> CapturedPieces { get; private set; }

        public Board()
            : this(Player.White)
        {
        }

        public Board(Player currentPlayer, Piece[,] boardState = null)
        {
            _board = boardState ?? new Piece[GameSettings.BoardSize, GameSettings.BoardSize];
            CurrentPlayer = currentPlayer;
            CapturedPieces = new List<Piece>();
        }

        public void AddPiece(Square square, Piece pawn)
        {
            _board[square.Row, square.Col] = pawn;
        }

        public bool SquareIsAvailable(Square square)
        {
            return HasSquare(square)
                   && GetPiece(square) == null;
        }

        public bool SquareIsOccupied(Square square)
        {
            return HasSquare(square)
                   && GetPiece(square) != null;
        }

        public bool HasSquare(Square square)
        {
            return square.Col >= 0
                   && square.Row >= 0
                   && square.Col < GameSettings.BoardSize
                   && square.Row < GameSettings.BoardSize;
        }

        public Piece GetPiece(Square square)
        {
            return _board[square.Row, square.Col];
        }

        public Square FindPiece(Piece piece)
        {
            for (var row = 0; row < GameSettings.BoardSize; row++)
            for (var col = 0; col < GameSettings.BoardSize; col++)
                if (_board[row, col] == piece)
                    return Square.At(row, col);

            throw new ArgumentException("The supplied piece is not on the board.", "piece");
        }

        public void MovePiece(Square from, Square to)
        {
            var movingPiece = _board[from.Row, from.Col];
            if (movingPiece == null)
            {
                return;
            }

            if (movingPiece.Player != CurrentPlayer)
            {
                throw new ArgumentException("The supplied piece does not belong to the current player.");
            }

            //If the space we're moving to is occupied, we need to mark it as captured.
            if (_board[to.Row, to.Col] != null)
            {
                OnPieceCaptured(_board[to.Row, to.Col]);
            }

            //Move the piece and set the 'from' square to be empty.
            _board[to.Row, to.Col] = _board[from.Row, from.Col];
            _board[from.Row, from.Col] = null;

            CurrentPlayer = movingPiece.Player == Player.White ? Player.Black : Player.White;
            OnCurrentPlayerChanged(CurrentPlayer);
        }

        public bool MoveIsIntoCheck(Square from, Square to)
        {
            var piece = _board[from.Row, from.Col];
            if (piece == null)
            {
                return false;
            }

            var player = piece.Player;

            var testBoard = new Board(CurrentPlayer, (Piece[,])_board.Clone());
            testBoard.MovePiece(from, to);
            return testBoard.IsPlayerInCheck(player);
        }

        public bool IsPlayerInCheck(Player player)
        {
            var playerKings = new List<Piece>();
            var opposingPieces = new List<Piece>();

            foreach (var piece in _board)
            {
                if (piece == null)
                {
                    continue;
                }

                if (piece.Player != player)
                {
                    opposingPieces.Add(piece);
                }
                else if (piece is King)
                {
                    playerKings.Add(piece);
                }
            }

            var kingLocations = playerKings.Select(FindPiece);

            return opposingPieces.Any(p => p.GetAvailableMoves(this).Any(kingLocations.Contains));
        }

        public bool IsStalemate()
        {
            return !IsPlayerInCheck(CurrentPlayer) && PlayerHasNoValidMoves(CurrentPlayer);
        }

        public bool IsCheckmate()
        {
            return IsPlayerInCheck(CurrentPlayer) && PlayerHasNoValidMoves(CurrentPlayer);
        }

        public bool PlayerHasNoValidMoves(Player player)
        {
            foreach (var piece in _board)
            {
                if (piece == null || piece.Player != player)
                {
                    continue;
                }

                if (piece.GetNonCheckMoves(this).Any())
                {
                    return false;
                }
            }

            return true;
        }

        public delegate void PieceCapturedEventHandler(Piece piece);

        public event PieceCapturedEventHandler PieceCaptured;

        protected virtual void OnPieceCaptured(Piece piece)
        {
            var handler = PieceCaptured;
            handler?.Invoke(piece);
        }

        public delegate void CurrentPlayerChangedEventHandler(Player player);

        public event CurrentPlayerChangedEventHandler CurrentPlayerChanged;

        protected virtual void OnCurrentPlayerChanged(Player player)
        {
            var handler = CurrentPlayerChanged;
            handler?.Invoke(player);
        }
    }
}