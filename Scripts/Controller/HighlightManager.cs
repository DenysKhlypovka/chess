using System;
using GameObjectScript;
using Model;
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

        public void HighlightCell(MoveType moveType, Coordinate coordinate)
        {
            var cellToChangeColor = boardController.GetCell(coordinate);
            switch (moveType)
            {
                case MoveType.Castling:
                    HighlightCastlingCell(cellToChangeColor);
                    break;
                case MoveType.Move:
                    HighlightCellAvailableToMoveOnto(cellToChangeColor);
                    break;
                case MoveType.Capture:
                    HighlightCellUnderPieceToCapture(cellToChangeColor);
                    break;
                case MoveType.Unavailable:
                    return;
                case MoveType.PotentialCapture:
                    return;
            }
            cellToChangeColor.GetComponent<CellController>().IsActivated = true;
        }

        public void HighlightCellUnderActivePiece(Coordinate coordinate)
        {
            RendererController.SetColorToYellow(boardController.GetCell(coordinate));
        }

        private void HighlightCastlingCell(GameObject cellToChangeColor)
        {
            RendererController.SetColorToBlue(cellToChangeColor);
        }

        private void HighlightCellAvailableToMoveOnto(GameObject cellToChangeColor)
        {
            RendererController.SetColorToGreen(cellToChangeColor);
        }

        private void HighlightCellUnderPieceToCapture(GameObject cellToChangeColor)
        {
            RendererController.SetColorToRed(cellToChangeColor);
        }
    }
}