using GameObjectScript;
using Util;

namespace Controller
{
    public class HighlightManager
    {
        //TODO: refactor methods
        private readonly BoardController boardController;

        public HighlightManager(BoardController boardController)
        {
            this.boardController = boardController;
        }

        public void HighlightCellUnderActiveFigure(int locationX, int locationY)
        {
            RendererController.SetColorToYellow(boardController.GetCell(locationX, locationY));
        }

        public void HighlightCastlingCell(RookController rookController)
        {
            var cellToChangeColor =
                boardController.GetCell(CastlingUtil.GetRookCastlingDestinationX(rookController),
                    rookController.LocationY);
            RendererController.SetColorToBlue(cellToChangeColor);
        }

        public void HighlightCellAvailableToMoveOnto(int locationX, int locationY)
        {
            var cellToChangeColor = boardController.GetCell(locationX, locationY);
            RendererController.SetColorToGreen(cellToChangeColor);
            cellToChangeColor.GetComponent<CellController>().Activate();
        }

        public void HighlightCellUnderFigureToCapture(int locationX, int locationY)
        {
            var cellToChangeColor = boardController.GetCell(locationX, locationY);
            RendererController.SetColorToRed(cellToChangeColor);
            cellToChangeColor.GetComponent<CellController>().Activate();
        }
    }
}