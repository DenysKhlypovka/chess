using System;
using GameObjectScript;
using UnityEngine;

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

        public void HighlightCell(MoveType moveType, int locationX, int locationY)
        {
            var cellToChangeColor = boardController.GetCell(locationX, locationY);
            switch (moveType)
            {
                case MoveType.Castling:
                    HighlightCastlingCell(cellToChangeColor);
                    break;
                case MoveType.Move:
                    HighlightCellAvailableToMoveOnto(cellToChangeColor);
                    break;
                case MoveType.Capture:
                    HighlightCellUnderFigureToCapture(cellToChangeColor);
                    break;
                case MoveType.Unavailable:
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            cellToChangeColor.GetComponent<CellController>().Activate();
        }

        public void HighlightCellUnderActiveFigure(int locationX, int locationY)
        {
            RendererController.SetColorToYellow(boardController.GetCell(locationX, locationY));
        }

        private void HighlightCastlingCell(GameObject cellToChangeColor)
        {
            RendererController.SetColorToBlue(cellToChangeColor);
        }

        private void HighlightCellAvailableToMoveOnto(GameObject cellToChangeColor)
        {
            RendererController.SetColorToGreen(cellToChangeColor);
        }

        private void HighlightCellUnderFigureToCapture(GameObject cellToChangeColor)
        {
            RendererController.SetColorToRed(cellToChangeColor);
        }
    }
}