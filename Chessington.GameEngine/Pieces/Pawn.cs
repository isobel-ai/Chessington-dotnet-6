﻿using System.Collections.Generic;
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
            
            moveSquares.Add(Square.At(currentSquare.Row + direction * 1, currentSquare.Col));
            if (!board.MovedPawns.Contains(this))
            {
                moveSquares.Add(Square.At(currentSquare.Row + direction * 2, currentSquare.Col));
            }
            return moveSquares;
        }
    }
}