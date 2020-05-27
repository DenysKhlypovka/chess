using System.Collections.Generic;
using System.Linq;
using GameObjectScript;
using JetBrains.Annotations;
using UnityEngine;

namespace Controller
{
    public class FiguresController
    {
        public FiguresController()
        {
            Figures = Util.Util.GetFigures();
        }

        public void Init(GameController gameController, HighlightManager highlightManager)
        {
            foreach (var figureController in Figures.Select(piece =>
                piece.GetComponent<FigureController>()))
            {
                Util.Util.SetCoordinatesOfGameObject(figureController);
                figureController.Init(gameController, highlightManager);
            }
        }

        internal List<GameObject> Figures { get; }

        [CanBeNull]
        public FigureController GetFigureControllerAtPosition(int x, int y)
        {
            return Figures
                .Select(figure => figure.GetComponent<FigureController>())
                .FirstOrDefault(figureController => figureController.LocationX == x && figureController.LocationY == y);
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
                .Where(figureController => figureController.IsActive))
            {
                figure.IsActive = false;
                figure.ClearAvailableMovesList();
                figure.ClearMoveset();
            }
        }
    }
}