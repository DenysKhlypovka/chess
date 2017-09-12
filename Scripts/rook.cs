using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rook : MonoBehaviour {

	private GameObject board;
	private GameObject cube;
	private GameObject gameControllerG;

	private int castlingX;
	private int castlingY;

	private pieceController pieceScript;
	private gameController gameScript;
	private boardController boardScript;

	// Use this for initialization
	void Start () {


		board = GameObject.FindWithTag ("board");

		gameControllerG = GameObject.FindWithTag ("controller");

		gameScript = gameControllerG.GetComponent<gameController> ();

		boardScript = board.GetComponent<boardController> ();

		pieceScript = gameObject.GetComponent<pieceController> ();

		if (pieceScript.boardPositionX == 0)
			castlingX = 3;
		else
			castlingX = 5;
		castlingY = pieceScript.boardPositionY;
	}

	// Update is called once per frame
	void Update () {


	}

	void OnMouseDown(){
		

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

			cube = board.GetComponent<boardController> ().getCube (i, j);
			if (gameScript.getArrayComponent (i, j) == 0) {
				
				if (castling(i, j))
					cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.blue);
				else
					cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.yellow);
				cube.GetComponent<cubeController> ().Activate ();

			}
			if (gameScript.getArrayComponent (i, j) == pieceScript.multip) {
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				cube.GetComponent<cubeController> ().Activate ();
				break;
			}
		}

		for (int i = pieceScript.getPosX () - 1; i > -1; i--) {
			int j = pieceScript.getPosY ();

			if ((i < 0) || (gameScript.getArrayComponent (i, j) == -pieceScript.multip))
				break;

			cube = board.GetComponent<boardController> ().getCube (i, j);
			if (gameScript.getArrayComponent (i, j) == 0) {
				if (castling(i, j))
					cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.blue);
				else
					cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.yellow);
				cube.GetComponent<cubeController> ().Activate ();

			}
			if (gameScript.getArrayComponent (i, j) == pieceScript.multip) {
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				cube.GetComponent<cubeController> ().Activate ();
				break;
			}
		}

		for (int j = pieceScript.getPosY () + 1; j < 8; j++) {
			int i = pieceScript.getPosX ();

			if ((j > 7) || (gameScript.getArrayComponent (i, j) == -pieceScript.multip))
				break;

			cube = board.GetComponent<boardController> ().getCube (i, j);
			if (gameScript.getArrayComponent (i, j) == 0) {
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.yellow);
				cube.GetComponent<cubeController> ().Activate ();
			}
			if (gameScript.getArrayComponent (i, j) == pieceScript.multip) {
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				cube.GetComponent<cubeController> ().Activate ();
				break;
			}
		}

		for (int j = pieceScript.getPosY () - 1; j > -1; j--) {
			int i = pieceScript.getPosX ();

			if ((j < 0) || (gameScript.getArrayComponent (i, j) == -pieceScript.multip))
				break;

			cube = board.GetComponent<boardController> ().getCube (i, j);
			if (gameScript.getArrayComponent (i, j) == 0) {
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.yellow);
				cube.GetComponent<cubeController> ().Activate ();
			}
			if (gameScript.getArrayComponent (i, j) == pieceScript.multip) {
				cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
				cube.GetComponent<cubeController> ().Activate ();
				break;
			}
		}
	}
bool castling(int i, int j){
	
		if (!pieceScript.mov ()) {
			if ((i == castlingX) && (j == castlingY)) {
				GameObject king;
				GameObject[] Figures = GameObject.FindGameObjectsWithTag ("figure");
				foreach (GameObject go in Figures)
					if ((go.layer == 8) && (go.GetComponent<pieceController> ().color == pieceScript.color)) {
						king = go;

						if (!king.GetComponent<pieceController> ().mov ()) {
							gameScript.setCastlingValue (i, j, pieceScript.color);
							return true;
						}
					}
			}
		}
		return false;
}
}