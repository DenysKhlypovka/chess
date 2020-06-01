using System.Collections.Generic;
using System.Linq;
using GameObjectScript;
using GameObjectScript.Figure;
using JetBrains.Annotations;
using Model;
using UnityEngine;

namespace Controller
{
    public class FiguresController
    {
        public FiguresController(GameController gameController)
        {
            Figures = Util.Util.GetFigures();
            foreach (var figureController in Figures.Select(piece =>
                piece.GetComponent<FigureController>()))
            {
                Util.Util.SetCoordinatesOfGameObject(figureController);
                SetColorOfFigure(figureController);
                figureController.Init(gameController);
            }
        }

        internal List<GameObject> Figures { get; }

        [CanBeNull]
        public FigureController GetFigureControllerAtPosition(Coordinate coordinate)
        {
            return Figures
                .Select(figure => figure.GetComponent<FigureController>())
                .FirstOrDefault(figureController => figureController.Coordinate.Equals(coordinate));
        }

        public void RemoveFigure(GameObject figureToRemove)
        {
            Figures.Remove(figureToRemove);
        }

        public GameObject GetKingOfColor(Color color)
        {
            return Figures.First(figure =>
                figure.GetComponent<KingController>() != null &&
                figure.GetComponent<FigureController>().Color == color);
        }

        public void DeactivateFigures()
        {
            foreach (var figure in Figures.Select(figure => figure.GetComponent<FigureController>())
                .Where(figureController => figureController.IsActivated))
            {
                figure.IsActivated = false;
            }
        }

        private void SetColorOfFigure(FigureController figureController)
        {
            //TODO: magic number 2
            figureController.Color = figureController.Coordinate.Y > 2 ? Color.white : Color.black;
        }
    }
}