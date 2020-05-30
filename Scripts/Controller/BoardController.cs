using System.Collections.Generic;
using System.Linq;
using GameObjectScript;
using UnityEngine;

namespace Controller
{
    public class BoardController : MonoBehaviour
    {
        private List<CellController> cellControllers;

        void Start()
        {
            cellControllers = Util.Util.GetCells().ConvertAll(o => o.GetComponent<CellController>());
            foreach (var cell in cellControllers)
            {
                Util.Util.SetCoordinatesOfGameObject(cell.GetComponent<CellController>());
            }
        }

        public GameObject GetCell(Coordinate coordinate)
        {
            return cellControllers.First(cell => cell.Coordinate.Equals(coordinate)).gameObject;
        }

        public void RedrawCells()
        {
            foreach (var cellController in Util.Util.GetCells().Select(cell => cell.GetComponent<CellController>()))
            {
                cellController.IsActivated = false;
                RedrawCell(cellController);
            }
        }

        private void RedrawCell(CellController cellController)
        {
            RendererController.ChangeColor(cellController.gameObject, cellController.Color);
        }
    }
}