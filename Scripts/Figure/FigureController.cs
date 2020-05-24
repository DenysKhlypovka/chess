using UnityEngine;

namespace Figure
{
    public class FigureController : ElementOnGrid
    {
        private bool moved;
        private bool isActive;

        protected BoardController boardController;
        protected GameController gameController;

        void Start()
        {
            Color = LocationY > 2 ? Color.white : Color.black;
            boardController = Util.GetBoardController();
            gameController = Util.GetGameController();
        }

        protected void OnMouseDown()
        {
            gameController.Reset();
            if (Color != gameController.GetTurnColor()) return;
            isActive = true;
            gameController.HighlightCubeUnderActiveFigure(boardController.GetCube(LocationX, LocationY));
        }

        private void SetMoved()
        {
            moved = true;
        }

        public bool IsMoved()
        {
            return moved;
        }

        public bool IsActive
        {
            get => isActive;
            set => isActive = value;
        }

        public void SetPos(int x, int y)
        {
            LocationX = x;
            LocationY = y;
            SetMoved();
        }

        // returns 'true' when break is needed after the method execution 
        public bool HighlightCubes(int locX, int locY)
        {
            var figureControllerAtPosition = gameController.GetFigureControllerAtPosition(locX, locY);
            var cubeAtPosition = boardController.GetCube(locX, locY);

            if (figureControllerAtPosition == null)
            {
                gameController.HighlightCubeAvailableToMoveOnto(cubeAtPosition);
                return false;
            }

            if (figureControllerAtPosition.Color != Color)
            {
                gameController.HighlightCubeUnderFigureToCapture(cubeAtPosition);
            }

            return true;
        }
    }
}