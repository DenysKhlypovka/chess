using UnityEngine;

namespace Figure
{
    public class FigureController : ElementOnGrid
    {
        private bool moved;

        protected HighlightManager highlightManager;
        protected GameController gameController;

        public void Init(GameController gameController, HighlightManager highlightManager)
        {
            Color = LocationY > 2 ? Color.white : Color.black;
            this.highlightManager = highlightManager;
            this.gameController = gameController;
        }

        protected void Activate()
        {
            gameController.Reset();
            
            if (Color != gameController.GetTurnColor()) return;
            IsActive = true;
            highlightManager.HighlightCellUnderActiveFigure(LocationX, LocationY);
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

        // returns 'true' cell is blocked by other figure 
        protected bool HighlightCells(int locX, int locY)
        {
            if (highlightManager.CheckAndHighlightCellAvailableToMoveOnto(locX, locY))
            {
                return false;
            }

            highlightManager.CheckAndHighlightCellUnderFigureToCapture(locX, locY, Color);
            return true;
        }
    }
}