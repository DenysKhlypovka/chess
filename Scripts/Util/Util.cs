using System;
using System.Collections.Generic;
using System.Linq;
using Controller;
using GameObjectScript;
using UnityEngine;

namespace Util
{
    public class Util : MonoBehaviour
    {
        public static List<GameObject> GetCells()
        {
            return GetGameObjectsOfTag(Tag.cell);
        }

        public static List<GameObject> GetFigures()
        {
            return GetGameObjectsOfTag(Tag.figure);
        }

        public static GameController GetGameController()
        {
            return GetGameObjectOfTag(Tag.controller).GetComponent<GameController>();
        }

        public static BoardController GetBoardController()
        {
            return GetGameObjectOfTag(Tag.board).GetComponent<BoardController>();
        }

        private static List<GameObject> GetGameObjectsOfTag(Tag tag)
        {
            return GameObject.FindGameObjectsWithTag(tag.ToString())
                .OfType<GameObject>()
                .ToList();
        }

        private static GameObject GetGameObjectOfTag(Tag tag)
        {
            return GameObject.FindWithTag(tag.ToString());
        }

        public static void SetCoordinatesOfGameObject(ElementOnGrid objectToSet)
        {
            var physicalPosition = objectToSet.transform.position;
            objectToSet.LocationX = (int) Math.Round(physicalPosition.z); // * -1 + 7;
            objectToSet.LocationY = (int) Math.Round(physicalPosition.x);
        }

        public static bool CellOutOfBounds(int locX, int locY)
        {
            return locX < 0 || locX > 7 || locY < 0 || locY > 7;
        }
    }
}