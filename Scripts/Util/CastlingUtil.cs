using System;
using Controller;
using GameObjectScript;
using Model;
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

        public static bool IsCastlingAvailable(PieceController rook, PieceController king, PiecesController piecesController)
        {
            if (king.IsMoved() || rook.IsMoved()) return false;

            for (var coordinate = new Coordinate(Math.Min(rook.Coordinate.X, king.Coordinate.X) + 1, rook.Coordinate.Y);
                coordinate.X < Math.Max(rook.Coordinate.X, king.Coordinate.X);
                coordinate.X++)
            {
                if (piecesController.GetPieceControllerAtPosition(coordinate) != null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}