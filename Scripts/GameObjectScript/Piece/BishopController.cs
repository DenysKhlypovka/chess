using System.Collections.Generic;
using Model;

namespace GameObjectScript.Piece
{
    public class BishopController : PieceController
    {
        void Awake()
        {
            PieceType = Model.Piece.Bishop;
        }

        public override List<MoveProperties> GetPossibleMoveset()
        {
            return CheckDiagonalMoves();
        }
    }
}