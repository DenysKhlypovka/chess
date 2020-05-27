﻿using System.Collections.Generic;
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
        }

        public List<CellController> GetCellControllers()
        {
            return cellControllers;
        }

        public GameObject GetCell(int x, int y)
        {
            return cellControllers.First(cell => cell.LocationX == x && cell.LocationY == y).gameObject;
        }

        public void RedrawCells()
        {
            foreach (var cellController in Util.Util.GetCells().Select(cell => cell.GetComponent<CellController>()))
            {
                cellController.Deactivate();
                RedrawCell(cellController);
            }
        }

        private void RedrawCell(CellController cellController)
        {
            RendererController.ChangeColor(cellController.gameObject, cellController.Color);
        }
    }
}