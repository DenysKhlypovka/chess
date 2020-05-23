using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Util : MonoBehaviour
{
    public static List<GameObject> GetCubes()
    {
        return GetGameObjectsOfTag(Tag.cube);
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
}