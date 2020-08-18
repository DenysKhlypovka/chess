using System.Collections;
using GameObjectScript;
using Model;
using UnityEngine;

namespace Controller
{
  public class PieceMoveController : MonoBehaviour
  {
    private const float FIXATE_CAMERA_COROUTINE_STEPS = 30f;

    public void Move(PieceController pieceController, Coordinate destinationCoordinate, bool withAnimation)
    {
      if (withAnimation)
      {
        StartCoroutine(Translate(pieceController, destinationCoordinate));
      }
      else
      {
        pieceController.gameObject.transform.Translate(destinationCoordinate.Y - pieceController.Coordinate.Y, 0,
          destinationCoordinate.X - pieceController.Coordinate.X);
      }
      pieceController.MoveCoordinate(destinationCoordinate);
    }

    private IEnumerator Translate(PieceController pieceController, Coordinate destinationCoordinate)
    {
      float deltaX = (destinationCoordinate.Y - pieceController.Coordinate.Y) / FIXATE_CAMERA_COROUTINE_STEPS;
      float deltaZ = (destinationCoordinate.X - pieceController.Coordinate.X) / FIXATE_CAMERA_COROUTINE_STEPS;
      var deltaVector = new Vector3(deltaX, 0, deltaZ);
      for (var i = 0; i < FIXATE_CAMERA_COROUTINE_STEPS; i++)
      {
        pieceController.gameObject.transform.Translate(deltaVector);
        yield return new WaitForEndOfFrame();
      }
    }
  }
}