using System;
using Controller;
using GameObjectScript;
using UnityEngine;

namespace Util
{
    public static class CastlingUtil
    {
        public static int GetRookCastlingDestinationX(ElementOnGrid rookController)
        {
            return rookController.Coordinate.X == 0 ? 3 : 5;
        }

        public static int GetKingCastlingDestinationX(ElementOnGrid rookController)
        {
            return rookController.Coordinate.X == 0 ? 2 : 6;
        }

        public static bool IsCastlingAvailable(GameObject rook, GameObject king, FiguresController figuresController)
        {
            var rookController = rook.GetComponent<FigureController>();
            var kingController = king.GetComponent<FigureController>();

            if (king.GetComponent<FigureController>().IsMoved() ||
                rook.GetComponent<FigureController>().IsMoved()) return false;

            for (var coordinate = new Coordinate(Math.Min(rookController.Coordinate.X, kingController.Coordinate.X) + 1, rookController.Coordinate.Y);
                coordinate.X < Math.Max(rookController.Coordinate.X, kingController.Coordinate.X);
                coordinate.X++)
            {
                if (figuresController.GetFigureControllerAtPosition(coordinate) != null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}