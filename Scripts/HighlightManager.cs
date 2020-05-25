using Figure;
using UnityEngine;

public class HighlightManager
{
    private readonly GameController gameController;
    private readonly BoardController boardController;

    public HighlightManager(GameController gameController, BoardController boardController)
    {
        this.gameController = gameController;
        this.boardController = boardController;
    }

    public bool CheckAndHighlightCellAvailableToMoveOnto(int locX, int locY)
    {
        if (CellOutOfBounds(locX, locY)) return true;
        if (gameController.GetFigureControllerAtPosition(locX, locY) != null) return false;
        HighlightCellAvailableToMoveOnto(boardController.GetCell(locX, locY));
        return true;
    }

    public void CheckAndHighlightCellUnderFigureToCapture(int locX, int locY, Color activeFigureColor)
    {
        if (CellOutOfBounds(locX, locY)) return;
        var figureControllerAtPosition = gameController.GetFigureControllerAtPosition(locX, locY);
        if (figureControllerAtPosition != null && figureControllerAtPosition.Color != activeFigureColor)
        {
            HighlightCellUnderFigureToCapture(boardController.GetCell(locX, locY));
        }
    }

    public void HighlightCellUnderActiveFigure(int locationX, int locationY)
    {
        RendererController.SetColorToYellow(boardController.GetCell(locationX, locationY));
    }

    public void HighlightCastlingCell(RookController rookController)
    {
        var cellToChangeColor =
            boardController.GetCell(CastlingUtil.GetCastlingX(rookController), rookController.LocationY);
        RendererController.SetColorToBlue(cellToChangeColor);
    }

    private void HighlightCellAvailableToMoveOnto(GameObject cellToChangeColor)
    {
        RendererController.SetColorToGreen(cellToChangeColor);
        cellToChangeColor.GetComponent<CellController>().Activate();
    }

    private void HighlightCellUnderFigureToCapture(GameObject cellToChangeColor)
    {
        RendererController.SetColorToRed(cellToChangeColor);
        cellToChangeColor.GetComponent<CellController>().Activate();
    }
    
    
    private bool CellOutOfBounds(int locX, int locY)
    {
        return locX < 0 || locX > 7 || locY < 0 || locY > 7;
    }
}