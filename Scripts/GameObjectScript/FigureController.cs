using System.Collections.Generic;
using Controller;
using Model;
using Util;

namespace GameObjectScript
{
    public abstract class FigureController : ElementOnGrid
    {
        private List<MoveProperties> availableMoves;

        private bool moved;

        public void Init(GameController gameController)
        {
            this.gameController = gameController;
            availableMoves = new List<MoveProperties>();
        }

        void OnMouseDown()
        {
            Activate();
        }

        public abstract List<MoveProperties> GetPossibleMoveset();

        public void ClearAvailableMoves()
        {
            availableMoves.Clear();
        }

        public void SetAvailableMoves(List<MoveProperties> moveset)
        {
            availableMoves = new List<MoveProperties>(moveset);
        }

        public bool IsMoved()
        {
            return moved;
        }

        public void ChangeCoordinates(Coordinate coordinate)
        {
            Coordinate = coordinate;
            moved = true;
        }

        protected List<MoveProperties> FilterInitialMoveset(List<MoveProperties> inputMoveset)
        {
            return inputMoveset.ConvertAll(moveProperties =>
                gameController.GetMoveProperties(moveProperties.Coordinate, Color));
        }

        public void Activate()
        {
            if (IsActivated) return;
            gameController.ResetBoardAndFigures();

            if (!gameController.IsAllowedToMove(this)) return;
            IsActivated = true;
            gameController.HighlightCells(availableMoves, Coordinate);
        }

        protected List<MoveProperties> CheckDiagonalMoves()
        {
            return CheckCyclicMoves(FigureMovesetParameters.GetDiagonalMovesetParametersList(Coordinate));
        }

        protected List<MoveProperties> CheckHorizontalVerticalMoves()
        {
            return CheckCyclicMoves(FigureMovesetParameters.GetHorizontalVerticalMovesetParametersList(Coordinate));
        }

        private List<MoveProperties> CheckCyclicMoves(List<MovesetParameters> movesetParametersList)
        {
            var moveset = new List<MoveProperties>();
            movesetParametersList.ForEach(movesetParameters =>
            {
                for (var coordinate = new Coordinate(movesetParameters.InitialX, movesetParameters.InitialY);
                    movesetParameters.ConditionX(coordinate.X) && movesetParameters.ConditionY(coordinate.Y);
                    coordinate.X += movesetParameters.IteratorDiffX, coordinate.Y += movesetParameters.IteratorDiffY)
                {
                    MoveProperties.AddToMoveset(moveset,
                        gameController.GetMoveProperties(new Coordinate(coordinate), Color));
                    if (!gameController.CanMoveOnto(coordinate))
                    {
                        break;
                    }
                }
            });
            return moveset;
        }
    }
}