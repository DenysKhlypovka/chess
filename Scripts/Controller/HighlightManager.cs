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
        private readonly PiecesController piecesController;

        private const float OUTLINE_WIDTH = 7.5f;

        public HighlightManager(BoardController boardController, PiecesController piecesController)
        {
            this.boardController = boardController;
            this.piecesController = piecesController;
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
                    OutlinePieceToCapture(coordinate);
                    break;
                case MoveType.Unavailable:
                    return;
                case MoveType.PotentialCapture:
                    return;
            }
            cellToChangeColor.GetComponent<CellController>().IsActivated = true;
        }

        private void HighlightCastlingCell(CellController cellToChangeColor)
        {
            RendererController.SetColorToBlue(cellToChangeColor.gameObject, cellToChangeColor.Color == Color.black);
        }

        private void HighlightCellAvailableToMoveOnto(CellController cellToChangeColor)
        {
            RendererController.SetColorToGreen(cellToChangeColor.gameObject, cellToChangeColor.Color == Color.black);
        }

        public void InitOutlines()
        {
            piecesController.Pieces.ForEach(piece => ChangeOutline(piece.GetComponent<Outline>(), Outline.Mode.OutlineHidden, Color.clear, OUTLINE_WIDTH));
        }

        public void RemoveAllOutlines()
        {
            piecesController.Pieces.ForEach(piece => ChangeOutline(piece.GetComponent<Outline>(), Outline.Mode.OutlineHidden, Color.clear));
        }

        public void OutlineActivePiece(Coordinate coordinate)
        {
            OutlineActivePiece(piecesController.GetPieceControllerAtPosition(coordinate));
        }

        public void OutlineActivePiece(PieceController piece)
        {
            ChangeOutline(GetOutlineOfPiece(piece), Outline.Mode.OutlineAll, Color.green);
        }

        private void OutlinePieceToCapture(Coordinate coordinate)
        {
            ChangeOutline(GetOutlineOfPiece(piecesController.GetPieceControllerAtPosition(coordinate)), Outline.Mode.OutlineAll, Color.red);
        }

        private Outline GetOutlineOfPiece(PieceController pieceController)
        {
            return pieceController.gameObject.GetComponent<Outline>();
        }

        private void ChangeOutline(Outline outline, Outline.Mode outlineMode, Color color)
        {
            outline.OutlineMode = outlineMode;
            outline.OutlineColor = color;
        }

        private void ChangeOutline(Outline outline, Outline.Mode outlineMode, Color color, float width)
        {
            outline.OutlineMode = outlineMode;
            outline.OutlineColor = color;
            outline.OutlineWidth = width;
        }
    }
}