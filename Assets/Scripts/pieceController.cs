using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pieceController : MonoBehaviour {

	public int boardPositionX;
	public int boardPositionY;

	public bool color;

	public int multip = 1;

	private bool beenMoved = false;
	private bool turn;

	private GameObject board;
	private GameObject cube;
	private GameObject gameControllerG;
	private gameController gameScript;

	// Use this for initialization
	void Start () {

		if (color)
			multip *= -1;

		board = GameObject.FindWithTag ("board");

		gameControllerG = GameObject.FindWithTag ("controller");

		gameScript = gameControllerG.GetComponent<gameController> ();

	}
	
	// Update is called once per frame
	void Update () {
		
		if ((color) && (!turn) || (!color) && (turn))
			gameObject.GetComponent<MeshCollider> ().enabled = false;
		else
			gameObject.GetComponent<MeshCollider> ().enabled = true;
		turn = gameScript.Turn ();
	}

	public void moved (){
		beenMoved = true;
	}


	public int getPosX(){
		return boardPositionX;
	}
	public int getPosY(){
		return boardPositionY;
	}
	public bool getTurn(){
		return turn;
	}
	public bool mov()
	{
		return beenMoved;
	}

	public void setPos(int x, int y){
		boardPositionX = x;
		boardPositionY = y;
	}

}
