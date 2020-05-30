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
            objectToSet.Coordinate = new Coordinate((int) Math.Round(physicalPosition.z),
                (int) Math.Round(physicalPosition.x));
        }

        public static bool IsCellOutOfBounds(Coordinate coordinate)
        {
            return coordinate.X < 0 || coordinate.X > 7 || coordinate.Y < 0 || coordinate.Y > 7;
        }

        public static void MovePhysically(GameObject figure, Coordinate destinationCoordinate)
        {
            var figureController = figure.GetComponent<FigureController>();
            figure.transform.Translate(destinationCoordinate.Y - figureController.Coordinate.Y, 0,
                destinationCoordinate.X - figureController.Coordinate.X);
        }
    }
}