using System.Collections.Generic;
using System.Linq;
using Util;

namespace GameObjectScript
{
    public class RookController : FigureController
    {
        public override List<MoveProperties> GetPossibleMoveset()
        {
            var moveset = CheckHorizontalVerticalMoves();
            CheckCastling(moveset);
            return moveset;
        }

        private void CheckCastling(List<MoveProperties> moveset)
        {
            if (!gameController.IsCastlingAvailable(this)) return;

            var castlingMove = moveset.FirstOrDefault(moveToChange =>
                moveToChange.Coordinate.Equals(new Coordinate(CastlingUtil.GetRookCastlingDestinationX(this), Coordinate.Y)));
            if (castlingMove != null)
            {
                castlingMove.MoveType = MoveType.Castling;
            }
        }
    }
}