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
            Debug.Log("zdarowa from " + gameObject.GetComponent<FigureController>().name);
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
    }
}