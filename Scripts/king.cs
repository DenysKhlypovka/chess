using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class king : MonoBehaviour {


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


		for (int i = pieceScript.getPosX() - 1; i < pieceScript.getPosX() + 2; i++)
			for (int j = pieceScript.getPosY() - 1; j < pieceScript.getPosY() + 2; j++) {
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

	}   
}