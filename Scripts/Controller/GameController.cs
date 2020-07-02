using System.Collections.Generic;
using System.Linq;
using GameObjectScript;
using GameObjectScript.Piece;
using Model;
using UnityEngine;
using Util;

namespace Controller
{
    public class GameController : MonoBehaviour
    {
        private BoardController boardController;
        private HighlightManager highlightManager;
        private PiecesController piecesController;
        private CapturePromotionController capturePromotionController;
        private LayoutTextManager layoutTextManager;

        private Color turnColor = Color.white;

        void Start()
        {
            boardController = ComponentsUtil.GetBoardController();
            piecesController = new PiecesController(this);
            highlightManager = new HighlightManager(boardController);
            layoutTextManager = new LayoutTextManager();
            capturePromotionController = new CapturePromotionController();
            ChangePiecesMeshColliderDependingOnTurn();
            SetPiecesAvailableMoves();
        }

        public void ResetBoardAndPieces()
        {
            piecesController.DeactivatePieces();
            boardController.RedrawCells();
        }

        public void TryActivatePiece(Coordinate coordinate)
        {
            piecesController.TryActivatePiece(coordinate);
        }

        private void ChangePiecesMeshColliderDependingOnTurn()
        {
            piecesController.Pieces.ForEach(pieceController =>
                MeshColliderController.ChangeMeshColliderEnabledProperty(pieceController.gameObject,
                    turnColor == pieceController.Color));
        }

        public bool IsAllowedToMove(PieceController pieceController)
        {
            return pieceController.Color == turnColor;
        }

        public void HighlightCells(List<MoveProperties> availableMoves, Coordinate coordinate)
        {
            highlightManager.HighlightCellUnderActivePiece(coordinate);
            availableMoves.ForEach(availableMove =>
                highlightManager.HighlightCell(availableMove.MoveType, availableMove.Coordinate));
        }

        private void CapturePiece(PieceController pieceToCapture)
        {
            piecesController.RemovePiece(pieceToCapture);
            capturePromotionController.AddToCaptured(pieceToCapture);
        }

        private void Move(PieceController piece, Coordinate coordinate)
        {
            Util.Util.MovePhysically(piece, coordinate);
            piece.Move(coordinate);
        }

        public bool CanMoveOnto(Coordinate coordinate)
        {
            return !Util.Util.IsCellOutOfBounds(coordinate) &&
                   piecesController.GetPieceControllerAtPosition(coordinate) == null;
        }

        public bool IsCastlingAvailable(RookController rookController)
        {
            return CastlingUtil.IsCastlingAvailable(rookController,
                piecesController.GetKingOfColor(rookController.Color), piecesController);
        }


        private void NextTurn()
        {
            ResetBoardAndPieces();
            turnColor = Util.Util.GetOppositeColor(turnColor);
            ChangePiecesMeshColliderDependingOnTurn();
            SetPiecesAvailableMoves();
            if (piecesController.GetPieceControllersOfColor(turnColor)
                .All(pieceController => pieceController.NoAvailableMoves()))
            {
                layoutTextManager.DisplayCheckMateText(turnColor);
            }
            else if (VerifyCheck())
            {
                layoutTextManager.DisplayCheckText(turnColor);
            }
            else
            {
                layoutTextManager.DisplayNothing();
            }
        }

        private void SetPiecesAvailableMoves()
        {
            foreach (var pieceController in piecesController.GetPieceControllersOfColor(turnColor))
            {
                var availableMoves = new List<MoveProperties>();
                pieceController.GetPossibleMoveset().ForEach(possibleMove =>
                {
                    var enemyPieceAtCoordinate =
                        piecesController.GetPieceControllerAtPosition(possibleMove.Coordinate);
                    var formerCoordinate = pieceController.Coordinate;
                    pieceController.Coordinate = possibleMove.Coordinate;

                    if (!VerifyCheckIgnoringPiece(enemyPieceAtCoordinate))
                    {
                        availableMoves.Add(possibleMove);
                    }

                    pieceController.Coordinate = formerCoordinate;
                });
                pieceController.SetAvailableMoves(availableMoves);
            }
        }

        private bool VerifyCheckIgnoringPiece(PieceController pieceToIgnore)
        {
            piecesController.RemovePiece(pieceToIgnore);
            var isCheck = VerifyCheck();
            piecesController.AddPiece(pieceToIgnore);
            return isCheck;
        }

        private bool VerifyCheck()
        {
            var kingController = piecesController.GetKingOfColor(turnColor);

            return piecesController
                .GetPieceControllersOfColor(Util.Util.GetOppositeColor(turnColor)).Any(enemyPiece => enemyPiece
                    .GetPossibleMoveset().Any(enemyPieceAvailableMove =>
                        enemyPieceAvailableMove.Coordinate.Equals(kingController.Coordinate)));
        }

        public void TakeTurn(Coordinate coordinate)
        {
            var activePiece =
                piecesController.Pieces.First(piece => piece.IsActivated);

            CheckAndTryCastling(activePiece, coordinate.X);

            var optionalControllerOfPieceAtPosition =
                piecesController.GetPieceControllerAtPosition(coordinate);
            if (optionalControllerOfPieceAtPosition != null)
            {
                CapturePiece(optionalControllerOfPieceAtPosition);
            }

            Move(activePiece, coordinate);
            NextTurn();
        }

        private void CheckAndTryCastling(PieceController activePiece, int destinationX)
        {
            var optionalControllerOfRook = activePiece.gameObject.GetComponent<RookController>();
            if (optionalControllerOfRook != null &&
                CastlingUtil.GetRookCastlingDestinationX(optionalControllerOfRook) == destinationX &&
                IsCastlingAvailable(optionalControllerOfRook))
            {
                Move(piecesController.GetKingOfColor(optionalControllerOfRook.Color),
                    new Coordinate(CastlingUtil.GetKingCastlingDestinationX(optionalControllerOfRook),
                        optionalControllerOfRook.Coordinate.Y));
            }
        }

        public MoveProperties GetMoveProperties(Coordinate coordinate, Color color, bool isPotentialCapture = false)
        {
            if (Util.Util.IsCellOutOfBounds(coordinate))
            {
                return MoveProperties.GetUnavailableMoveProperties();
            }

            var controllerOfPieceAtPosition = piecesController.GetPieceControllerAtPosition(coordinate);
            var moveProperties = new MoveProperties(coordinate, MoveType.Capture);

            if (controllerOfPieceAtPosition != null)
                return controllerOfPieceAtPosition.Color != color
                    ? moveProperties
                    : MoveProperties.GetUnavailableMoveProperties();

            moveProperties.MoveType = isPotentialCapture ? MoveType.PotentialCapture : MoveType.Move;
            return moveProperties;
        }
    }
}