using System.Collections.Generic;
using System.Linq;
using Controller;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Util
{
    public class ComponentsUtil
    {
        public static List<GameObject> GetCells()
        {
            return GetGameObjectsOfTag(Tag.cell);
        }

        public static List<GameObject> GetPieces()
        {
            return GetGameObjectsOfTag(Tag.piece);
        }

        public static GameController GetGameController()
        {
            return GetGameObjectOfTag(Tag.controller).GetComponent<GameController>();
        }

        public static BoardController GetBoardController()
        {
            return GetGameObjectOfTag(Tag.board).GetComponent<BoardController>();
        }

        public static CameraController GetCameraController()
        {
            return Object.FindObjectOfType<CameraController>();
        }

        public static ButtonsController GetButtonsController()
        {
            return Object.FindObjectOfType<ButtonsController>();
        }

        public static Text GetLayoutText()
        {
            return GetGameObjectOfTag(Tag.layout).GetComponent<Text>();
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
    }
}