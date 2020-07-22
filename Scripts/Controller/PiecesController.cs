using System.Collections.Generic;
using System.Linq;
using GameObjectScript;
using JetBrains.Annotations;
using Model;
using UnityEngine;
using Util;

namespace Controller
{
    public class PiecesController
    {
        internal List<PieceController> Pieces { get; }
        public PiecesController(GameController gameController)
        {
            Pieces = ComponentsUtil.GetPieces().ConvertAll(o => o.GetComponent<PieceController>());
            Pieces.ForEach(pieceController =>
            {
                Util.Util.SetCoordinatesOfGameObject(pieceController);
                SetColorOfPiece(pieceController);
                pieceController.Init(gameController);
            });
        }

        [CanBeNull]
        public PieceController GetPieceControllerAtPosition(Coordinate coordinate)
        {
            return Pieces.FirstOrDefault(pieceController => pieceController.Coordinate.Equals(coordinate));
        }

        public void RemovePiece(PieceController pieceScriptToRemove)
        {
            if (pieceScriptToRemove != null)
            {
                Pieces.Remove(pieceScriptToRemove);
            }
        }

        public void AddPiece(PieceController pieceScriptToAdd)
        {
            if (pieceScriptToAdd != null)
            {
                Pieces.Add(pieceScriptToAdd);
            }
        }

        public PieceController GetKingOfColor(Color color)
        {
            return Pieces.First(pieceController =>
                pieceController.PieceType == Piece.King &&
                pieceController.Color == color);
        }
        
        public List<PieceController> GetPieceControllersOfColor(Color color)
        {
            return Pieces.Where(pieceController => pieceController.Color == color).ToList();
        }
        
        public void TryActivatePiece(Coordinate coordinate)
        {
            var optionalPieceController = GetPieceControllerAtPosition(coordinate);
            if (optionalPieceController != null)
            {
                optionalPieceController.Activate();
            }
        }

        public void DeactivatePieces()
        {
            foreach (var piece in Pieces.Where(pieceController => pieceController.IsActivated))
            {
                piece.IsActivated = false;
            }
        }

        private void SetColorOfPiece(PieceController pieceController)
        {
            //TODO: magic number 2
            pieceController.Color = pieceController.Coordinate.Y > 2 ? Color.white : Color.black;
        }
    }
}