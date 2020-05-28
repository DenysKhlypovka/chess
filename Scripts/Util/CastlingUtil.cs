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
            return rookController.LocationX == 0 ? 3 : 5;
        }

        public static int GetKingCastlingDestinationX(ElementOnGrid rookController)
        {
            return rookController.LocationX == 0 ? 2 : 6;
        }

        public static bool IsCastlingAvailable(GameObject rook, GameObject king, FiguresController figuresController)
        {
            var rookController = rook.GetComponent<FigureController>();
            var kingController = king.GetComponent<FigureController>();

            if (king.GetComponent<FigureController>().IsMoved() ||
                rook.GetComponent<FigureController>().IsMoved()) return false;

            for (var locX = Math.Min(rookController.LocationX, kingController.LocationX) + 1;
                locX < Math.Max(rookController.LocationX, kingController.LocationX);
                locX++)
            {
                if (figuresController.GetFigureControllerAtPosition(locX, rookController.LocationY) != null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}