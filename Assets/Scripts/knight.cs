using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knight : MonoBehaviour {

	private GameObject board;
	private GameObject cube;
	private GameObject gameControllerG;


	// Use this for initialization
	void Start () {


		board = GameObject.FindWithTag ("board");

		gameControllerG = GameObject.FindWithTag ("controller");
	}

	// Update is called once per frame
	void Update () {


	}



	void OnMouseDown(){
		gameController gameScript = gameControllerG.GetComponent<gameController> ();
		boardController boardScript = board.GetComponent<boardController> ();

		pieceController pieceScript = gameObject.GetComponent<pieceController> ();

		if ((pieceScript.color) && (!pieceScript.getTurn()) || (!pieceScript.color) && (pieceScript.getTurn())) {
			return;
		}


		gameScript.clear ();

		gameScript.setActivePiece (gameObject);

		cube = boardScript.getCube (pieceScript.getPosX(), pieceScript.getPosY());
		cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);

		int X = pieceScript.getPosX ();
		int Y = pieceScript.getPosY ();
		for (int i = X - 2; i < X + 3; i++)
			for (int j = Y - 2; j < Y + 3; j++) {
				if (((i - X)*(j - Y) == 2) || ((i - X)*(j - Y) == -2))
				if ((i > -1) && (i < 8) && (j > -1) && (j < 8)) {
					if (gameScript.getArrayComponent (i, j) == - 1 * pieceScript.multip) {
						continue;
					}
					if (gameScript.getArrayComponent (i, j) == 0) {
						cube = boardScript.getCube (i, j);
						cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.yellow);
						cube.GetComponent<cubeController> ().Activate ();
					}
					if (gameScript.getArrayComponent (i, j) == pieceScript.multip) {
						cube = boardScript.getCube (i, j);
						cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
						cube.GetComponent<cubeController> ().Activate ();
					}

				}
			}

	//	for (int i = pieceScript.getPosX() - 2; i < pieceScript.getPosX() + 3; i++)
	//		for (int j = pieceScript.getPosY() - 2; j < pieceScript.getPosY() + 3; j++)
	//		{
	//			if ((i * j == 2) || (i * j == -2))
	//			if (i > -1 && i < 8 && j > -1 && j < 8) {
	//				if (gameScript.getArrayComponent (i, j) == 0) {
	//					cube = board.GetComponent<boardController> ().getCube (i, j);
	//					cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.yellow);
	//					cube.GetComponent<cubeController> ().Activate ();
	//				}
	//				if (gameScript.getArrayComponent (i, j) == pieceScript.multip) {
	//					cube = board.GetComponent<boardController> ().getCube (i, j);
	//					cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
	//					cube.GetComponent<cubeController> ().Activate ();
	//				}
	//			}
	//	}




		// this object was clicked - do something
		//GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
	}   
}
