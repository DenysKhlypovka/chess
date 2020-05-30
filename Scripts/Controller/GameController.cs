using System.Collections.Generic;
using System.Linq;
using GameObjectScript;
using UnityEngine;
using Util;

namespace Controller
{
    public class GameController : MonoBehaviour
    {
        //TODO: Other figures to be able to capture a figure-source of check
        private BoardController boardController;
        private HighlightManager highlightManager;
        private FiguresController figuresController;

        private Color turnColor = Color.white;

        private bool kingInCheck;

        void Start()
        {
            boardController = Util.Util.GetBoardController();
            figuresController = new FiguresController(this);
            highlightManager = new HighlightManager(boardController);
            ChangeFiguresMeshColliderDependingOnTurn();
            SetFiguresAvailableMoves();
        }

        private Color GetTurnColor()
        {
            return turnColor;
        }

        public void ResetBoardAndFigures()
        {
            figuresController.DeactivateFigures();
            boardController.RedrawCells();
        }

        public void TryActivateFigure(Coordinate coordinate)
        {
            var optionalFigureController = figuresController.GetFigureControllerAtPosition(coordinate);
            if (optionalFigureController != null)
            {
                optionalFigureController.Activate();
            }
        }

        private void ChangeFiguresMeshColliderDependingOnTurn()
        {
            figuresController.Figures.ForEach(figure =>
                MeshColliderController.ChangeMeshColliderEnabledProperty(figure,
                    turnColor == figure.GetComponent<FigureController>().Color));
        }

        public bool IsAllowedToMove(FigureController figureController)
        {
            return figureController.Color == GetTurnColor() &&
                   (!kingInCheck || kingInCheck && figureController.GetComponent<KingController>() != null);
        }

        public void HighlightCells(List<MoveProperties> availableMoves, Coordinate coordinate)
        {
            highlightManager.HighlightCellUnderActiveFigure(coordinate);
            availableMoves.ForEach(availableMove =>
                highlightManager.HighlightCell(availableMove.MoveType, availableMove.Coordinate));
        }

        private void NextTurn()
        {
            ResetBoardAndFigures();
            turnColor = turnColor == Color.white ? Color.black : Color.white;
            ChangeFiguresMeshColliderDependingOnTurn();
            SetFiguresAvailableMoves();
            //SetKingAvailableMoves();
        }

        private void SetFiguresAvailableMoves()
        {
            figuresController.Figures.ForEach(figure =>
            {
                var figureController = figure.GetComponent<FigureController>();
                figureController.SetAvailableMoves(figureController.GetPossibleMoveset());
            });
        }

        private void SetKingAvailableMoves()
        {
            kingInCheck = false;
            var kingController = figuresController.GetKingOfColor(turnColor).GetComponent<KingController>();
            var kingAvailableMoves = kingController.GetPossibleMoveset();

            foreach (var enemyFigure in figuresController.Figures
                .Select(figure => figure.GetComponent<FigureController>())
                .Where(figureController => figureController.Color != turnColor))
            {
                foreach (var enemyFigureAvailableMove in enemyFigure.GetPossibleMoveset())
                {
                    if (enemyFigureAvailableMove.Coordinate.Equals(kingController.Coordinate))
                    {
                        kingInCheck = true;
                    }

                    if (kingAvailableMoves.Any(kingAvailableMove =>
                        kingAvailableMove.Coordinate.Equals(enemyFigureAvailableMove.Coordinate)))
                    {
                        //kingController.AddToUnavailableMoves(enemyFigureAvailableMove);
                    }
                }
            }
        }

        public void TakeTurn(Coordinate coordinate)
        {
            var activeFigure =
                figuresController.Figures.First(figure => figure.GetComponent<FigureController>().IsActivated);

            CheckAndTryCastling(activeFigure, coordinate.X);

            var optionalControllerOfFigureAtPosition =
                figuresController.GetFigureControllerAtPosition(coordinate);
            if (optionalControllerOfFigureAtPosition != null)
            {
                DestroyFigure(optionalControllerOfFigureAtPosition.gameObject);
            }

            Move(activeFigure, coordinate);
            NextTurn();
        }

        private void CheckAndTryCastling(GameObject activeFigure, int destinationX)
        {
            var optionalControllerOfRook = activeFigure.GetComponent<RookController>();
            if (optionalControllerOfRook != null &&
                CastlingUtil.GetRookCastlingDestinationX(optionalControllerOfRook) == destinationX &&
                IsCastlingAvailable(optionalControllerOfRook))
            {
                Move(figuresController.GetKingOfColor(optionalControllerOfRook.Color),
                    new Coordinate(CastlingUtil.GetKingCastlingDestinationX(optionalControllerOfRook),
                        optionalControllerOfRook.Coordinate.Y));
            }
        }

        private void DestroyFigure(GameObject figureToDestroy)
        {
            figuresController.RemoveFigure(figureToDestroy.gameObject);
            Destroy(figureToDestroy);
        }

        private void Move(GameObject figure, Coordinate coordinate)
        {
            Util.Util.MovePhysically(figure, coordinate);
            figure.GetComponent<FigureController>().ChangeCoordinates(coordinate);
        }

        public MoveProperties GetMoveProperties(Coordinate coordinate, Color color, bool isPotentialCapture = false)
        {
            if (Util.Util.IsCellOutOfBounds(coordinate))
            {
                return MoveProperties.GetUnavailableMoveProperties();
            }

            var controllerOfFigureAtPosition = figuresController.GetFigureControllerAtPosition(coordinate);
            var moveProperties = new MoveProperties(coordinate, MoveType.Capture);

            if (controllerOfFigureAtPosition != null)
                return controllerOfFigureAtPosition.Color != color
                    ? moveProperties
                    : MoveProperties.GetUnavailableMoveProperties();

            moveProperties.MoveType = isPotentialCapture ? MoveType.PotentialCapture : MoveType.Move;
            return moveProperties;
        }

        public bool CanMoveOnto(Coordinate coordinate)
        {
            return !Util.Util.IsCellOutOfBounds(coordinate) &&
                   figuresController.GetFigureControllerAtPosition(coordinate) == null;
        }

        public bool IsCastlingAvailable(RookController rookController)
        {
            return CastlingUtil.IsCastlingAvailable(rookController.gameObject,
                figuresController.GetKingOfColor(rookController.Color), figuresController);
        }
    }
}