using System;
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

    public static int GetFacingDirectionOffset(Color color)
    {
      return color == Color.black ? 1 : -1;
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

    public static float CalculateAngle(float angle)
    {
      return Math.Abs(angle) > 180 ? 360 + angle : angle;
    }
  }
}