using System.Collections.Generic;
using UnityEngine;

namespace GameObjectScript
{
    public class PawnController : FigureController
    {
        public override List<MoveProperties> GetPossibleMoveset()
        {
            var moveset = new List<MoveProperties>();
            if (AddMoveToMoveset(moveset, true))
            {
                AddMoveToMoveset(moveset, false);
            }

            AddCapturesToMoveset(moveset);
            return moveset;
        }

        private bool AddMoveToMoveset(List<MoveProperties> moveset, bool isFirstCellForward)
        {
            var cellDestinationY = Coordinate.Y + GetColorMultiplier() * (isFirstCellForward ? 1 : 2);
            var moveProperties = gameController.GetMoveProperties(new Coordinate(Coordinate.X, cellDestinationY), Color);
            var isAvailableToMoveOntoCell = moveProperties.MoveType == MoveType.Move;

            if (isAvailableToMoveOntoCell && isFirstCellForward || !IsMoved())
            {
                MoveProperties.AddToMoveset(moveset, moveProperties);
            }

            return isAvailableToMoveOntoCell;
        }

        private void AddCapturesToMoveset(List<MoveProperties> moveset)
        {
            for (var offsetX = -1; offsetX <= 1; offsetX += 2)
            {
                MoveProperties.AddToMoveset(moveset,
                    gameController.GetMoveProperties(new Coordinate(Coordinate.X + offsetX, Coordinate.Y + GetColorMultiplier()), Color, true));
            }
        }

        private int GetColorMultiplier()
        {
            return Color == Color.black ? 1 : -1;
        }
    }
}