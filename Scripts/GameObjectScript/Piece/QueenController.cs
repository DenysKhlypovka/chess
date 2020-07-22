using System.Collections.Generic;
using System.Linq;
using Model;

namespace GameObjectScript.Piece
{
    public class QueenController : PieceController
    {
        void Awake()
        {
            PieceType = Model.Piece.Queen;
        }
        public override List<MoveProperties> GetPossibleMoveset()
        {
            return CheckHorizontalVerticalMoves().Concat(CheckDiagonalMoves()).ToList();
        }
    }
}