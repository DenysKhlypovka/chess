using Util;

namespace GameObjectScript
{
    public class RookController : FigureController
    {
        public override void FillMoveset()
        {
            CheckHorizontalVerticalMoves();
            CheckCastling();
        }

        private void CheckCastling()
        {
            if (!gameController.IsCastlingAvailable(this)) return;

            var castlingDestinationX = CastlingUtil.GetRookCastlingDestinationX(this);
            var castlingDestinationOffsetX = castlingDestinationX - LocationX;
            moveset.RemoveAll(moveset =>
                moveset.OffsetX == castlingDestinationOffsetX && moveset.OffsetY == 0);
            availableMoves.Add(new MoveProperties(castlingDestinationX, LocationY, null, MoveType.Castling));
        }
    }
}