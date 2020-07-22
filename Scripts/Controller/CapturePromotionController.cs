using System.Collections.Generic;
using System.Linq;
using GameObjectScript;
using Model;
using UnityEngine;

namespace Controller
{
  public class CapturePromotionController
  {
    private List<PieceController> CapturedPieces { get; }
    private readonly Dictionary<Color, int> capturedPosXByColor;

    private PiecesController piecesController;

    public CapturePromotionController()
    {
      CapturedPieces = new List<PieceController>();
      capturedPosXByColor = new Dictionary<Color, int>
      {
        {Color.white, -1},
        {Color.black, 8}
      };
    }

    public void AddToCaptured(PieceController pieceScriptToAdd)
    {
      CapturedPieces.Add(pieceScriptToAdd);
      RearrangeCapturedFiguresByColor(pieceScriptToAdd.Color);
    }

    public void RemoveFromCaptured(PieceController pieceScriptToAdd)
    {
      CapturedPieces.Remove(pieceScriptToAdd);
      RearrangeCapturedFiguresByColor(pieceScriptToAdd.Color);
    }

    private void RearrangeCapturedFiguresByColor(Color color)
    {
      var posY = 0;
      var posYPawns = 0;
      var offsetX = Util.Util.GetFacingDirectionOffset(color);
      CapturedPieces.OrderBy(piece => piece.PieceType).Where(piece => piece.Color == color).ToList()
        .ForEach(piece =>
        {
          var rearrangeCoordinate = piece.PieceType == Piece.Pawn
            ? new Coordinate(posYPawns++, capturedPosXByColor[color])
            : new Coordinate(posY++, capturedPosXByColor[color] + offsetX);

          piece.Move(rearrangeCoordinate);
        });
    }
  }
}