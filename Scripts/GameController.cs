using System;
using System.Collections.Generic;
using System.Linq;
using Figure;
using JetBrains.Annotations;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //TODO: first turn meshCollider
    //TODO: rook castling issue
    private List<GameObject> figures;

    private BoardController boardController;

    private Color turnColor = Color.white;

    void Start()
    {
        boardController = Util.GetBoardController();
        figures = Util.GetFigures();

        foreach (var cube in boardController.GetCubeControllers())
        {
            SetCoordinatesOfGameObject(cube.GetComponent<CubeController>());
        }

        foreach (var piece in figures)
        {
            FigureController figureController = piece.GetComponent<FigureController>();
            SetCoordinatesOfGameObject(figureController);
        }
    }

    public void NextTurn()
    {
        turnColor = turnColor == Color.white ? Color.black : Color.white;
        foreach (var figure in figures)
        {
            if (turnColor != figure.GetComponent<FigureController>().Color)
            {
                MeshColliderController.DisableMeshCollider(figure);
            }
            else
            {
                MeshColliderController.EnableMeshCollider(figure);
            }
        }
    }

    public Color GetTurnColor()
    {
        return turnColor;
    }

    [CanBeNull]
    public FigureController GetFigureControllerAtPosition(int x, int y)
    {
        return figures
            .Select(figure => figure.GetComponent<FigureController>())
            .FirstOrDefault(figureController => figureController.LocationX == x && figureController.LocationY == y);
    }

    public void Reset()
    {
        DeactivateFigures();
        boardController.RedrawCubes();
    }

    private void DeactivateFigures()
    {
        foreach (var figure in figures.Select(figure => figure.GetComponent<FigureController>()).Where(figureController => figureController.IsActive))
        {
            figure.IsActive = false;
        }
    }

    public void Move(int destinationX, int destinationY)
    {
        GameObject activeFigure = figures.First(figure => figure.GetComponent<FigureController>().IsActive);
        FigureController activeFigureController = activeFigure.GetComponent<FigureController>();;
        FigureController controllerOfFigureAtPosition = GetFigureControllerAtPosition(destinationX, destinationY);
        
        if (controllerOfFigureAtPosition != null)
        {
            GameObject figureAtPosition = controllerOfFigureAtPosition.gameObject;
            figures.Remove(figureAtPosition);
            Destroy(figureAtPosition);
        }

        activeFigure.transform.Translate(destinationY - activeFigureController.LocationY, 0, destinationX - activeFigureController.LocationX);

        activeFigureController.SetPos(destinationX, destinationY);
        
        Reset();
        NextTurn();
    }

    public void Castling(GameObject rook)
    {
        
        //Move(x, y);
    }

    public bool IsCastlingAvailable(GameObject rook)
    {
        var rookController = rook.GetComponent<FigureController>();
        var king = figures.First(figure =>
            figure.layer != 8 && figure.GetComponent<FigureController>().Color ==
            rookController.Color);
        var kingController = king.GetComponent<FigureController>();
        
        if (!king.GetComponent<FigureController>().IsMoved() && !rook.GetComponent<FigureController>().IsMoved())
        {
            for (int locX = Math.Min(rookController.LocationX, kingController.LocationX) + 1;
                locX < Math.Max(rookController.LocationX, kingController.LocationX);
                locX++)
            {
                if (boardController.GetCube(locX, rookController.LocationY) != null)
                {
                    return false;
                }
            }

            return true;
        }
        return false;
    }

    private void SetCoordinatesOfGameObject(ElementOnGrid objectToSet)
    {
        var physicalPosition = objectToSet.transform.position;
        objectToSet.LocationX = (int) Math.Round(physicalPosition.z);// * -1 + 7;
        objectToSet.LocationY = (int) Math.Round(physicalPosition.x);
    }

    public void HighlightCubeAvailableToMoveOnto(GameObject cubeToChangeColor)
    {
        RendererController.ChangeColor(cubeToChangeColor, Color.yellow);
        cubeToChangeColor.GetComponent<CubeController>().Activate();
    }

    public void HighlightCastlingCube(GameObject cubeToChangeColor)
    {
        RendererController.ChangeColor(cubeToChangeColor, Color.blue);
    }

    public void HighlightCubeUnderActiveFigure(GameObject cubeToChangeColor)
    {
        RendererController.ChangeColor(cubeToChangeColor, Color.green);
    }

    public void HighlightCubeUnderFigureToCapture(GameObject cubeToChangeColor)
    {
        RendererController.ChangeColor(cubeToChangeColor, Color.red);
        cubeToChangeColor.GetComponent<CubeController>().Activate();
    }

    public void RedrawCube(CubeController cubeController)
    {
        RendererController.ChangeColor(cubeController.gameObject, cubeController.Color);
    }


//	void Check(){		
//		int x, y;
//		int multip;
//
//		if (!turn) {
//			x = blackKing.GetComponent<pieceController> ().boardPositionX;
//			y = blackKing.GetComponent<pieceController> ().boardPositionY;
//			multip = 1;
//		} else {
//			x = whiteKing.GetComponent<pieceController> ().boardPositionX;
//			y = whiteKing.GetComponent<pieceController> ().boardPositionY;
//			multip = -1;
//		}
//
//		if (figures [x - 1, y + 1 * multip] == multip)
//		if (pieces [x - 1, y + 1 * multip].GetComponent<pawn> ())
//			check = true;
//
//		if (figures [x - 1, y + 1 * multip] == multip)
//		if (pieces [x - 1, y + 1 * multip].GetComponent<pawn> ())
//			check = true;
//	
//
//		for (int i = x - 2; i < x + 3; i++)
//		for (int j = y - 2; j < y + 3; j++) {
//				if ((i > -1) && (i < 8) && (j > -1) && (j < 8))
//				if (((i - x) * (j - y) == 2) || ((i - x) * (j - y) == -2))
//				if (pieces [i, j])
//				if (pieces [i, j].GetComponent<knight> ())
//					check = true;
//			}
//	}

//	public bool CheckTest(){	
//		return check;
//	}
}