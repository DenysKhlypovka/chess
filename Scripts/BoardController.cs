using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    private List<CubeController> cubeControllers;
    protected GameController gameController;

    void Start()
    {
        cubeControllers = Util.GetCubes().ConvertAll(o => o.GetComponent<CubeController>());
        gameController = Util.GetGameController();
    }

    public List<CubeController> GetCubeControllers()
    {
        return cubeControllers;
    }

    public GameObject GetCube(int x, int y)
    {
        return cubeControllers.First(cube => cube.LocationX == x && cube.LocationY == y).gameObject;
    }
    
    public void RedrawCubes()
    {
        foreach (var cubeController in Util.GetCubes().Select(cube => cube.GetComponent<CubeController>()))
        {
            cubeController.Dectivate();
            gameController.RedrawCube(cubeController);
        }
    }
}