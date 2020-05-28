using System.Collections.Generic;
using Controller;
using UnityEngine;

namespace GameObjectScript
{
    public abstract class FigureController : ElementOnGrid
    {
        protected List<MoveProperties> availableMoves;
        protected List<MoveOffset> moveset;

        private bool moved;

        protected HighlightManager highlightManager;
        protected GameController gameController;

        public void Init(GameController gameController, HighlightManager highlightManager)
        {
            //TODO: magic number 2
            Color = LocationY > 2 ? Color.white : Color.black;
            this.highlightManager = highlightManager;
            this.gameController = gameController;
            availableMoves = new List<MoveProperties>();
            moveset = new List<MoveOffset>();
        }

        private void Activate()
        {
            if (IsActive) return;
            gameController.Reset();

            if (!gameController.IsAllowedToMove(this)) return;
            IsActive = true;
            highlightManager.HighlightCellUnderActiveFigure(LocationX, LocationY);
            GetAvailableMoves().ForEach(availableMove => highlightManager.HighlightCell(availableMove.MoveType, availableMove.LocX, availableMove.LocY));
        }

        public virtual void ClearAvailableMovesList()
        {
            availableMoves.Clear();
        }

        public void ClearMoveset()
        {
            moveset.Clear();
        }

        public bool IsMoved()
        {
            return moved;
        }

        public bool IsActive { get; set; }

        public void Move(int x, int y)
        {
            LocationX = x;
            LocationY = y;
            moved = true;
        }

        protected virtual void CheckAvailableMoves()
        {
            moveset.ForEach(moveLocationOffset =>
            {
                var moveProperties = GetMoveProperties(LocationX + moveLocationOffset.OffsetX,
                    LocationY + moveLocationOffset.OffsetY);
                if (moveProperties.MoveType != MoveType.Unavailable)
                {
                    availableMoves.Add(moveProperties);
                }
            });
        }

        protected MoveProperties GetMoveProperties(int locX, int locY)
        {
            if (Util.Util.CellOutOfBounds(locX, locY))
            {
                return MoveProperties.GetUnavailableMoveProperties();
            }

            var controllerOfFigureAtPosition = gameController.GetFigureControllerAtPosition(locX, locY);
            var figurePosition = new MoveProperties(locX, locY, controllerOfFigureAtPosition, MoveType.Capture);
            if (controllerOfFigureAtPosition == null)
            {
                figurePosition.MoveType = MoveType.Move;
                return figurePosition;
            }

            return controllerOfFigureAtPosition.Color != Color
                ? figurePosition
                : MoveProperties.GetUnavailableMoveProperties();
        }

        public void CheckDiagonalMoves()
        {
            for (int locX = LocationX + 1, locY = LocationY + 1; locX < 8 && locY < 8; locX++, locY++)
            {
                if (!IsAvailableToMoveOnto(locX, locY))
                {
                    break;
                }
            }

            for (int locX = LocationX + 1, locY = LocationY - 1; locX < 8 && locY > -1; locX++, locY--)
            {
                if (!IsAvailableToMoveOnto(locX, locY))
                {
                    break;
                }
            }

            for (int locX = LocationX - 1, locY = LocationY + 1; locX > -1 && locY < 8; locX--, locY++)
            {
                if (!IsAvailableToMoveOnto(locX, locY))
                {
                    break;
                }
            }

            for (int locX = LocationX - 1, locY = LocationY - 1; locX > -1 && locY > -1; locX--, locY--)
            {
                if (!IsAvailableToMoveOnto(locX, locY))
                {
                    break;
                }
            }
        }

        public void CheckHorizontalVerticalMoves()
        {
            for (var i = LocationX + 1; i < 8; i++)
            {
                if (!IsAvailableToMoveOnto(i, LocationY))
                {
                    break;
                }
            }

            for (var i = LocationX - 1; i > -1; i--)
            {
                if (!IsAvailableToMoveOnto(i, LocationY))
                {
                    break;
                }
            }

            for (var j = LocationY + 1; j < 8; j++)
            {
                if (!IsAvailableToMoveOnto(LocationX, j))
                {
                    break;
                }
            }

            for (var j = LocationY - 1; j > -1; j--)
            {
                if (!IsAvailableToMoveOnto(LocationX, j))
                {
                    break;
                }
            }
        }

        private bool IsAvailableToMoveOnto(int locX, int locY)
        {
            var moveType = GetMoveProperties(locX, locY).MoveType;
            if (moveType == MoveType.Unavailable) return false;
            moveset.Add(new MoveOffset(locX - LocationX, locY - LocationY));
            return moveType == MoveType.Move;
        }

        void OnMouseDown()
        {
            Activate();
        }

        public List<MoveProperties> GetAvailableMoves()
        {
            FillMoveset();
            CheckAvailableMoves();
            return availableMoves;
        }

        public abstract void FillMoveset();
    }
}