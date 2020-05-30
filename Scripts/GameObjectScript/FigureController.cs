using System;
using System.Collections.Generic;
using Controller;

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
            return CheckCyclicMoves(GetDiagonalMovesetParametersList());
        }

        protected List<MoveProperties> CheckHorizontalVerticalMoves()
        {
            return CheckCyclicMoves(GetHorizontalVerticalMovesetParametersList());
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
                    if (!gameController.CanMoveOnto(coordinate))
                    {
                        break;
                    }

                    MoveProperties.AddToMoveset(moveset, gameController.GetMoveProperties(new Coordinate(coordinate), Color));
                }
            });
            return moveset;
        }

        private List<MovesetParameters> GetDiagonalMovesetParametersList()
        {
            return new List<MovesetParameters>
            {
                new MovesetParameters(Coordinate.X + 1, Coordinate.Y + 1, x => x < 8, y => y < 8, 1, 1),
                new MovesetParameters(Coordinate.X + 1, Coordinate.Y - 1, x => x < 8, y => y > -1, 1, -1),
                new MovesetParameters(Coordinate.X - 1, Coordinate.Y + 1, x => x > -1, y => y < 8, -1, 1),
                new MovesetParameters(Coordinate.X - 1, Coordinate.Y - 1, x => x > -1, y => y > -1, -1, -1)
            };
        }

        private List<MovesetParameters> GetHorizontalVerticalMovesetParametersList()
        {
            return new List<MovesetParameters>
            {
                new MovesetParameters(Coordinate.X + 1, Coordinate.Y, x => x < 8, y => true, 1, 0),
                new MovesetParameters(Coordinate.X - 1, Coordinate.Y, x => x > -1, y => true, -1, 0),
                new MovesetParameters(Coordinate.X, Coordinate.Y + 1, x => true, y => y < 8, 0, 1),
                new MovesetParameters(Coordinate.X, Coordinate.Y - 1, x => true, y => y > -1, 0, -1)
            };
        }

        private readonly struct MovesetParameters
        {
            public int InitialX { get; }
            public int InitialY { get; }
            public Func<int, bool> ConditionX { get; }
            public Func<int, bool> ConditionY { get; }
            public int IteratorDiffX { get; }
            public int IteratorDiffY { get; }

            public MovesetParameters(int initialX, int initialY, Func<int, bool> conditionX, Func<int, bool> conditionY,
                int iteratorDiffX, int iteratorDiffY)
            {
                InitialX = initialX;
                InitialY = initialY;
                ConditionX = conditionX;
                ConditionY = conditionY;
                IteratorDiffX = iteratorDiffX;
                IteratorDiffY = iteratorDiffY;
            }
        }
    }
}