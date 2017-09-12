using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pawn : MonoBehaviour {



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


		int X = pieceScript.getPosX ();
		int Y = pieceScript.getPosY () + 1 * pieceScript.multip;
		if (X > -1 && X < 8 && Y > -1 && Y < 8)
		if (gameScript.getArrayComponent (X, Y) == 0) {
			cube = boardScript.getCube (X, Y);
			cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.yellow);
			cube.GetComponent<cubeController> ().Activate ();
		

		X = pieceScript.getPosX ();
		Y = pieceScript.getPosY () + 2 * pieceScript.multip;
		if (X > -1 && X < 8 && Y > -1 && Y < 8)
		if (!pieceScript.mov () && gameScript.getArrayComponent (X, Y) == 0) {
			cube = board.GetComponent<boardController> ().getCube (X, Y);
			cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.yellow);
			cube.GetComponent<cubeController> ().Activate ();
		}
	}

		X = pieceScript.getPosX() + 1 * pieceScript.multip;
		Y = pieceScript.getPosY() + 1 * pieceScript.multip;
		if (X > -1 && X < 8 && Y > -1 && Y < 8)
		if (gameScript.getArrayComponent (X, Y) == pieceScript.multip) {
			cube = boardScript.getCube (X, Y);
			cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
			cube.GetComponent<cubeController> ().Activate ();
		}

		X = pieceScript.getPosX() - 1 * pieceScript.multip;
		Y = pieceScript.getPosY() + 1 * pieceScript.multip;
		if (X > -1 && X < 8 && Y > -1 && Y < 8)
		if (gameScript.getArrayComponent (X, Y) == pieceScript.multip) {
			cube = boardScript.getCube (X, Y);
			cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
			cube.GetComponent<cubeController> ().Activate ();
		}
		// this object was clicked - do something
		//GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
	}   
}
