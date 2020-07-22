using System.Collections.Generic;
using System.Linq;
using GameObjectScript;
using Model;
using UnityEngine;
using Util;

namespace Controller
{
    public class BoardController : MonoBehaviour
    {
        internal List<CellController> CellControllers { get; set; }

        void Start()
        {
            CellControllers = ComponentsUtil.GetCells().ConvertAll(o => o.GetComponent<CellController>());
            foreach (var cell in CellControllers)
            {
                Util.Util.SetCoordinatesOfGameObject(cell.GetComponent<CellController>());
            }
        }

        public GameObject GetCell(Coordinate coordinate)
        {
            return CellControllers.First(cell => cell.Coordinate.Equals(coordinate)).gameObject;
        }

        public void RedrawCells()
        {
            foreach (var cellController in ComponentsUtil.GetCells().Select(cell => cell.GetComponent<CellController>()))
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