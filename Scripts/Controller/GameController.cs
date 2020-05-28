using System.Linq;
using GameObjectScript;
using JetBrains.Annotations;
using UnityEngine;
using Util;

namespace Controller
{
    public class GameController : MonoBehaviour
    {
        //TODO: Other figures to be able to capture a figure-source of check
        //TODO: Check from pawns
        //TODO: Improve check (moveset checking without king)
        private BoardController boardController;
        private HighlightManager highlightManager;
        private FiguresController figuresController;

        private Color turnColor = Color.white;

        private bool kingInCheck;

        void Start()
        {
            boardController = Util.Util.GetBoardController();
            figuresController = new FiguresController();
            figuresController.Init(this, new HighlightManager(boardController));

            foreach (var cell in boardController.GetCellControllers())
            {
                Util.Util.SetCoordinatesOfGameObject(cell.GetComponent<CellController>());
            }

            ChangeFiguresMeshColliderDependingOnTurn();
        }

        private void NextTurn()
        {
            Reset();
            turnColor = turnColor == Color.white ? Color.black : Color.white;
            ChangeFiguresMeshColliderDependingOnTurn();
            SetKingAvailableMoves();
        }

        private void SetKingAvailableMoves()
        {
            
            kingInCheck = false;
            var kingController = figuresController.GetKingOfColor(turnColor).GetComponent<KingController>();
            var kingAvailableMoves = kingController.GetAvailableMoves();

            foreach (var enemyFigure in figuresController.Figures
                .Select(figure => figure.GetComponent<FigureController>())
                .Where(figureController => figureController.Color != turnColor))
            {
                foreach (var enemyFigureAvailableMove in enemyFigure.GetAvailableMoves())
                {
                    if (enemyFigureAvailableMove.LocX == kingController.LocationX &&
                        enemyFigureAvailableMove.LocY == kingController.LocationY)
                    {
                        kingInCheck = true;
                    }
                    if (kingAvailableMoves.Any(kingAvailableMove => kingAvailableMove.LocX == enemyFigureAvailableMove.LocX && kingAvailableMove.LocY == enemyFigureAvailableMove.LocY))
                    {
                        kingController.AddToUnavailableMoves(enemyFigureAvailableMove);
                    }
                }
            }
        }

        private void ChangeFiguresMeshColliderDependingOnTurn()
        {
            foreach (var figure in figuresController.Figures)
            {
                if (turnColor != figure.GetComponent<FigureController>().Color)
                {
                    MeshColliderController.DisableMeshCollider(figure);
                }
                else
                {
                    MeshColliderController.EnableMeshCollider(figure);
                }
            }
        }

        private Color GetTurnColor()
        {
            return turnColor;
        }

        public bool IsAllowedToMove(FigureController figureController)
        {
            return figureController.Color == GetTurnColor() &&
                    (!kingInCheck || kingInCheck && figureController.GetComponent<KingController>() != null);
        }

        [CanBeNull]
        public FigureController GetFigureControllerAtPosition(int x, int y)
        {
            return figuresController.GetFigureControllerAtPosition(x, y);
        }

        public void Reset()
        {
            figuresController.DeactivateFigures();
            boardController.RedrawCells();
        }

        public void TakeTurn(int destinationX, int destinationY)
        {
            var activeFigure =
                figuresController.Figures.First(figure => figure.GetComponent<FigureController>().IsActive);

            var optionalControllerOfRook = activeFigure.GetComponent<RookController>();
            if (optionalControllerOfRook != null &&
                CastlingUtil.GetRookCastlingDestinationX(optionalControllerOfRook) == destinationX &&
                IsCastlingAvailable(optionalControllerOfRook))
            {
                Move(figuresController.GetKingOfColor(optionalControllerOfRook.Color),
                    CastlingUtil.GetKingCastlingDestinationX(optionalControllerOfRook),
                    optionalControllerOfRook.LocationY);
            }

            var optionalControllerOfFigureAtPosition =
                figuresController.GetFigureControllerAtPosition(destinationX, destinationY);
            if (optionalControllerOfFigureAtPosition != null)
            {
                DestroyFigure(optionalControllerOfFigureAtPosition.gameObject);
            }

            Move(activeFigure, destinationX, destinationY);
            NextTurn();
        }

        private void DestroyFigure(GameObject figureToDestroy)
        {
            figuresController.RemoveFigure(figureToDestroy.gameObject);
            Destroy(figureToDestroy);
        }

        private void Move(GameObject figure, int destinationX, int destinationY)
        {
            var figureController = figure.GetComponent<FigureController>();

            figure.transform.Translate(destinationY - figureController.LocationY, 0,
                destinationX - figureController.LocationX);
            figureController.Move(destinationX, destinationY);
        }

        public bool IsCastlingAvailable(RookController rookController)
        {
            return CastlingUtil.IsCastlingAvailable(rookController.gameObject,
                figuresController.GetKingOfColor(rookController.Color), figuresController);
        }
    }
}