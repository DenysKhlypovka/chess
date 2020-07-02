using System;
using System.Collections.Generic;
using System.Linq;
using Controller;
using GameObjectScript;
using Model;
using UnityEngine;

namespace Util
{
    public class Util : MonoBehaviour
    {
        public static Color GetOppositeColor(Color color)
        {
            return color == Color.white ? Color.black : Color.white;
        }

        public static void SetCoordinatesOfGameObject(ElementOnGrid objectToSet)
        {
            var physicalPosition = objectToSet.transform.position;
            objectToSet.Coordinate = new Coordinate((int) Math.Round(physicalPosition.z),
                (int) Math.Round(physicalPosition.x));
        }

        public static bool IsCellOutOfBounds(Coordinate coordinate)
        {
            return coordinate.X < 0 || coordinate.X > 7 || coordinate.Y < 0 || coordinate.Y > 7;
        }

        public static void MovePhysically(PieceController pieceController, Coordinate destinationCoordinate)
        {
            pieceController.gameObject.transform.Translate(destinationCoordinate.Y - pieceController.Coordinate.Y, 0,
                destinationCoordinate.X - pieceController.Coordinate.X);
        }
    }
}