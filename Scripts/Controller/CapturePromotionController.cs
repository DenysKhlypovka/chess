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
            if (pieceScriptToAdd == null) return;
            CapturedPieces.Add(pieceScriptToAdd);
            RearrangeCapturedFiguresByColor(pieceScriptToAdd.Color);
        }

        private void RearrangeCapturedFiguresByColor(Color color)
        {
            var posY = 0;
            CapturedPieces.OrderBy(piece => piece.piece).Where(piece => piece.Color == color).ToList()
                .ForEach(piece =>
                {
                    var rearrangeCoordinate = new Coordinate(posY++, capturedPosXByColor[color]);
                    Util.Util.MovePhysically(piece, rearrangeCoordinate);
                    piece.Coordinate = rearrangeCoordinate;
                });
        }
    }
}