using System.Collections.Generic;
using System.Linq;
using GameObjectScript;
using GameObjectScript.Figure;
using JetBrains.Annotations;
using Model;
using UnityEngine;
using Util;

namespace Controller
{
    public class FiguresController
    {
        public FiguresController(GameController gameController)
        {
            Figures = ComponentsUtil.GetFigures();
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
            if (figureToRemove != null)
            {
                Figures.Remove(figureToRemove);
            }
        }

        public void RemoveFigure(FigureController figureScriptToRemove)
        {
            if (figureScriptToRemove != null)
            {
                Figures.Remove(figureScriptToRemove.gameObject);
            }
        }

        public void AddFigure(FigureController figureScriptToAdd)
        {
            if (figureScriptToAdd != null)
            {
                Figures.Add(figureScriptToAdd.gameObject);
            }
        }

        public GameObject GetKingOfColor(Color color)
        {
            return Figures.First(figure =>
                figure.GetComponent<KingController>() != null &&
                figure.GetComponent<FigureController>().Color == color);
        }
        
        public IEnumerable<FigureController> GetFigureControllersOfColor(Color color)
        {
            return Figures.ConvertAll(figure => figure.GetComponent<FigureController>()).Where(figureController => figureController.Color == color);
        }
        
        public void TryActivateFigure(Coordinate coordinate)
        {
            var optionalFigureController = GetFigureControllerAtPosition(coordinate);
            if (optionalFigureController != null)
            {
                optionalFigureController.Activate();
            }
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