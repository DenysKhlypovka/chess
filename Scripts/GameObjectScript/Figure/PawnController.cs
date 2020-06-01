using System.Collections.Generic;
using Model;
using UnityEngine;

namespace GameObjectScript.Figure
{
    public class PawnController : FigureController
    {
        public override List<MoveProperties> GetPossibleMoveset()
        {
            var moveset = new List<MoveProperties>();
            if (AddMoveToMoveset(moveset, 1) == MoveType.Move && !IsMoved())
            {
                AddMoveToMoveset(moveset, 2);
            }

            AddCaptureToMoveset(moveset, -1);
            AddCaptureToMoveset(moveset, 1);
            return moveset;
        }

        private MoveType AddMoveToMoveset(List<MoveProperties> moveset, int coordinateYMultiplier)
        {
            var cellDestinationY = Coordinate.Y + GetColorMultiplier() * coordinateYMultiplier;
            var moveProperties =
                gameController.GetMoveProperties(new Coordinate(Coordinate.X, cellDestinationY), Color);

            if (moveProperties.MoveType == MoveType.Move)
            {
                MoveProperties.AddToMoveset(moveset, moveProperties);
            }

            return moveProperties.MoveType;
        }

        private void AddCaptureToMoveset(List<MoveProperties> moveset, int offsetX)
        {
            MoveProperties.AddToMoveset(moveset,
                gameController.GetMoveProperties(
                    new Coordinate(Coordinate.X + offsetX, Coordinate.Y + GetColorMultiplier()), Color, true));
        }

        private int GetColorMultiplier()
        {
            return Color == Color.black ? 1 : -1;
        }
    }
}