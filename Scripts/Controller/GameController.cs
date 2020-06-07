using System.Collections.Generic;
using System.Linq;
using GameObjectScript;
using GameObjectScript.Figure;
using Model;
using UnityEngine;
using Util;

namespace Controller
{
    public class GameController : MonoBehaviour
    {
        private BoardController boardController;
        private HighlightManager highlightManager;
        private FiguresController figuresController;
        private LayoutTextManager layoutTextManager;

        private Color turnColor = Color.white;

        void Start()
        {
            boardController = ComponentsUtil.GetBoardController();
            figuresController = new FiguresController(this);
            highlightManager = new HighlightManager(boardController);
            layoutTextManager = new LayoutTextManager();
            ChangeFiguresMeshColliderDependingOnTurn();
            SetFiguresAvailableMoves();
        }

        public void ResetBoardAndFigures()
        {
            figuresController.DeactivateFigures();
            boardController.RedrawCells();
        }

        public void TryActivateFigure(Coordinate coordinate)
        {
            figuresController.TryActivateFigure(coordinate);
        }

        private void ChangeFiguresMeshColliderDependingOnTurn()
        {
            figuresController.Figures.ForEach(figure =>
                MeshColliderController.ChangeMeshColliderEnabledProperty(figure,
                    turnColor == figure.GetComponent<FigureController>().Color));
        }

        public bool IsAllowedToMove(FigureController figureController)
        {
            return figureController.Color == turnColor;
        }

        public void HighlightCells(List<MoveProperties> availableMoves, Coordinate coordinate)
        {
            highlightManager.HighlightCellUnderActiveFigure(coordinate);
            availableMoves.ForEach(availableMove =>
                highlightManager.HighlightCell(availableMove.MoveType, availableMove.Coordinate));
        }

        private void DestroyFigure(GameObject figureToDestroy)
        {
            figuresController.RemoveFigure(figureToDestroy);
            Destroy(figureToDestroy);
        }

        private void Move(GameObject figure, Coordinate coordinate)
        {
            Util.Util.MovePhysically(figure, coordinate);
            figure.GetComponent<FigureController>().Move(coordinate);
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


        private void NextTurn()
        {
            ResetBoardAndFigures();
            turnColor = Util.Util.GetOppositeColor(turnColor);
            ChangeFiguresMeshColliderDependingOnTurn();
            SetFiguresAvailableMoves();
            if (figuresController.GetFigureControllersOfColor(turnColor)
                .All(figureController => figureController.NoAvailableMoves()))
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

        private void SetFiguresAvailableMoves()
        {
            foreach (var figureController in figuresController.GetFigureControllersOfColor(turnColor))
            {
                var availableMoves = new List<MoveProperties>();
                figureController.GetPossibleMoveset().ForEach(possibleMove =>
                {
                    var enemyFigureAtCoordinate =
                        figuresController.GetFigureControllerAtPosition(possibleMove.Coordinate);
                    var formerCoordinate = figureController.Coordinate;
                    figureController.Coordinate = possibleMove.Coordinate;

                    if (!VerifyCheckIgnoringFigure(enemyFigureAtCoordinate))
                    {
                        availableMoves.Add(possibleMove);
                    }

                    figureController.Coordinate = formerCoordinate;
                });
                figureController.SetAvailableMoves(availableMoves);
            }
        }

        private bool VerifyCheckIgnoringFigure(FigureController figureToIgnore)
        {
            figuresController.RemoveFigure(figureToIgnore);
            var isCheck = VerifyCheck();
            figuresController.AddFigure(figureToIgnore);
            return isCheck;
        }

        private bool VerifyCheck()
        {
            var kingController = figuresController.GetKingOfColor(turnColor).GetComponent<KingController>();

            return figuresController
                .GetFigureControllersOfColor(Util.Util.GetOppositeColor(turnColor)).Any(enemyFigure => enemyFigure
                    .GetPossibleMoveset().Any(enemyFigureAvailableMove =>
                        enemyFigureAvailableMove.Coordinate.Equals(kingController.Coordinate)));
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
    }
}