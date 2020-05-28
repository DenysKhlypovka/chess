using UnityEngine;

namespace GameObjectScript
{
    public class PawnController : FigureController
    {
        public override void FillMoveset()
        {
            var firstCellForwardOffsetY = 1 * GetColorMultiplier();
            var firstCellForwardY = LocationY + firstCellForwardOffsetY;

            var firstMoveCell = GetMoveProperties(LocationX, firstCellForwardY);
            if (firstMoveCell.MoveType == MoveType.Move)
            {
                moveset.Add(new MoveOffset(0, firstCellForwardOffsetY));
                var secondCellForwardOffsetY = 2 * GetColorMultiplier();
                var secondMoveCell = GetMoveProperties(LocationX, LocationY + secondCellForwardOffsetY);
                if (secondMoveCell.MoveType == MoveType.Move && !IsMoved())
                {
                    moveset.Add(new MoveOffset(0, secondCellForwardOffsetY));
                }
            }

            for (var offsetX = -1; offsetX <= 1; offsetX += 2)
            {
                if (GetMoveProperties(LocationX + offsetX, firstCellForwardY).MoveType == MoveType.Capture)
                {
                    moveset.Add(new MoveOffset(offsetX, firstCellForwardOffsetY));
                }
            }
        }

        private int GetColorMultiplier()
        {
            return Color == Color.black ? 1 : -1;
        }
    }
}