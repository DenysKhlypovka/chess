using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class queen : MonoBehaviour {

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

		if ((pieceScript.color) && (!pieceScript.getTurn ()) || (!pieceScript.color) && (pieceScript.getTurn ())) {
			return;
		}


		gameScript.clear ();

		gameScript.setActivePiece (gameObject);

		cube = boardScript.getCube (pieceScript.getPosX (), pieceScript.getPosY ());
		cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.green);


		for (int i = pieceScript.getPosX () + 1; i < 8; i++) {
			int j = pieceScript.getPosY ();
			if ((i > 7) || (gameScript.getArrayComponent (i, j) == -pieceScript.multip))
				break;

			if (gameScript.getArrayComponent (i, j) == 0) {
				cube = board.GetComponent<boardController> ().getCube (i, j);
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.yellow);
				cube.GetComponent<cubeController> ().Activate ();
			}
			if (gameScript.getArrayComponent (i, j) == pieceScript.multip) {
				cube = board.GetComponent<boardController> ().getCube (i, j);
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				cube.GetComponent<cubeController> ().Activate ();
				break;
			}
		}

		for (int i = pieceScript.getPosX () - 1; i > -1; i--) {
			int j = pieceScript.getPosY ();

			if ((i < 0) || (gameScript.getArrayComponent (i, j) == -pieceScript.multip))
				break;

			if (gameScript.getArrayComponent (i, j) == 0) {
				cube = board.GetComponent<boardController> ().getCube (i, j);
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.yellow);
				cube.GetComponent<cubeController> ().Activate ();
			}
			if (gameScript.getArrayComponent (i, j) == pieceScript.multip) {
				cube = board.GetComponent<boardController> ().getCube (i, j);
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				cube.GetComponent<cubeController> ().Activate ();
				break;
			}
		}

		for (int j = pieceScript.getPosY () + 1; j < 8; j++) {
			int i = pieceScript.getPosX ();

			if ((j > 7) || (gameScript.getArrayComponent (i, j) == -pieceScript.multip))
				break;

			if (gameScript.getArrayComponent (i, j) == 0) {
				cube = board.GetComponent<boardController> ().getCube (i, j);
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.yellow);
				cube.GetComponent<cubeController> ().Activate ();
			}
			if (gameScript.getArrayComponent (i, j) == pieceScript.multip) {
				cube = board.GetComponent<boardController> ().getCube (i, j);
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				cube.GetComponent<cubeController> ().Activate ();
				break;
			}
		}

		for (int j = pieceScript.getPosY () - 1; j > -1; j--) {
			int i = pieceScript.getPosX ();

			if ((j < 0) || (gameScript.getArrayComponent (i, j) == -pieceScript.multip))
				break;

			if (gameScript.getArrayComponent (i, j) == 0) {
				cube = board.GetComponent<boardController> ().getCube (i, j);
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.yellow);
				cube.GetComponent<cubeController> ().Activate ();
			}
			if (gameScript.getArrayComponent (i, j) == pieceScript.multip) {
				cube = board.GetComponent<boardController> ().getCube (i, j);
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				cube.GetComponent<cubeController> ().Activate ();
				break;
			}
		}

		int sum = pieceScript.getPosX () + pieceScript.getPosY ();




		for (int i = pieceScript.getPosX () + 1; i < 8; i++) {
			if ((sum - i > 7) || (i > sum) || (i > 7))
				break;

			if (gameScript.getArrayComponent (i, sum - i) == -1 * pieceScript.multip) {
				break;
			}
			if (gameScript.getArrayComponent (i, sum - i) == 0) {
				cube = board.GetComponent<boardController> ().getCube (i, sum - i);
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.yellow);
				cube.GetComponent<cubeController> ().Activate ();
			}
			if (gameScript.getArrayComponent (i, sum - i) == pieceScript.multip) {
				cube = board.GetComponent<boardController> ().getCube (i, sum - i);
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				cube.GetComponent<cubeController> ().Activate ();
				break;
			}
		}



		sum = pieceScript.getPosX () + pieceScript.getPosY ();
		//asas
		for (int i = pieceScript.getPosX () - 1; i > - 1; i--) {
			if ((sum - i > 7) || (i > sum) || (i < 0))
				break;


			if (gameScript.getArrayComponent (i, sum - i) == -1 * pieceScript.multip) {
				break;
			}

			if (gameScript.getArrayComponent (i, sum - i) == 0) {
				cube = board.GetComponent<boardController> ().getCube (i, sum - i);
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.yellow);
				cube.GetComponent<cubeController> ().Activate ();
			}
			if (gameScript.getArrayComponent (i, sum - i) == pieceScript.multip) {
				cube = board.GetComponent<boardController> ().getCube (i, sum - i);
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				cube.GetComponent<cubeController> ().Activate ();
				break;
			}
		}


		int I = pieceScript.getPosX ();


		for (int j = pieceScript.getPosY () - 1; ; j--) {
			I--;

			if ((I < 0) || (j < 0)) {

				break;
			}


			if (gameScript.getArrayComponent (I, j) == -1 * pieceScript.multip) {
				break;
			}

			if (gameScript.getArrayComponent (I, j) == 0) {
				cube = board.GetComponent<boardController> ().getCube (I, j);
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.yellow);
				cube.GetComponent<cubeController> ().Activate ();
			}
			if (gameScript.getArrayComponent (I, j) == pieceScript.multip) {
				cube = board.GetComponent<boardController> ().getCube (I, j);
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				cube.GetComponent<cubeController> ().Activate ();
				break;
			}
		}

		I = pieceScript.getPosX ();



		for (int j = pieceScript.getPosY () + 1; ; j++) {
			I++;
			if ((I > 7) || (j > 7))
				break;

			if (gameScript.getArrayComponent (I, j) == -1 * pieceScript.multip) {
				break;
			}

			if (gameScript.getArrayComponent (I, j) == 0) {
				cube = board.GetComponent<boardController> ().getCube (I, j);
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.yellow);
				cube.GetComponent<cubeController> ().Activate ();
			}
			if (gameScript.getArrayComponent (I, j) == pieceScript.multip) {
				cube = board.GetComponent<boardController> ().getCube (I, j);
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				cube.GetComponent<cubeController> ().Activate ();
				break;
			}
		}	
	}
}
