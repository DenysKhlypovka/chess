using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour {

	public GameObject whiteKing;
	public GameObject blackKing;

	public GameObject[,] pieces = new GameObject[8,8];

	private GameObject board;
	private GameObject activePiece;


	private pieceController pieceScript;	//посилання на скрипт, який висить на ГО фігури

	private int[,] pieceColorArray = new int[8,8];

	private int castlingX, castlingY;

	private bool castlingColor;
	//private bool check = false;
	private bool turn = true;


	void Start () {

		GameObject[] Pieces = GameObject.FindGameObjectsWithTag ("figure");	//масив посилань на ГО фігур

		board = GameObject.FindWithTag ("board");

		for(int i = 0; i < 32; i++)
			pieces [Pieces [i].GetComponent<pieceController> ().getPosX (), 
				Pieces [i].GetComponent<pieceController> ().getPosY ()] = Pieces [i];	//створення відповідності між фігурами і їх положенням
		

		
		for (int i = 0; i < 8; i++)			//ініціалізація масиву для зберігання розташування фігур різного кольору
			for (int j = 0; j < 2; j++)
				pieceColorArray [i, j] = -1;		//black
		
		for (int i = 0; i < 8; i++)
			for (int j = 2; j < 6; j++)
				pieceColorArray [i, j] = 0;	
		
		for (int i = 0; i < 8; i++)
			for (int j = 6; j < 8; j++)
				pieceColorArray [i, j] = 1;			//white

	}


	void Update () {
		
	}

	public void nextTurn(){
		turn = !turn;
	}

	public bool Turn(){
		return turn;
	}

	public int getArrayComponent(int x, int y){
		return pieceColorArray [x, y];
	}

	public void setCastlingValue(int x, int y, bool color){	//властивості rook, викликається із скрипта на ГО rook
		castlingX = x;
		castlingY = y;
		castlingColor = color;
	}

	public void clear(){	

//		check = false;	

		castlingX = -1;
		castlingY = -1;

		int color = 1;

		for (int i = 0; i < 8; i++) {		//очистка поля
			for (int j = 0; j < 8; j++) {
				GameObject cube = board.GetComponent<boardController> ().getCube (i, j);
				cube.GetComponent<cubeController> ().Dectivate ();
				if (color == 1)
					cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.white);
				else
					cube.GetComponent<Renderer> ().material.SetColor ("_Color", Color.black);
				color *= -1;
			}
			color *= -1;
		}
	}

	public void setActivePiece(GameObject piece){	//для здійснення ходу, необхідно визначити, яка фігура ходить
		activePiece = piece;
		pieceScript = activePiece.GetComponent<pieceController> ();
	}

	public void move(int X, int Y){
		if ( pieceScript.multip * pieceColorArray[X, Y] > 0){	//якщо на клітинці фігура ворога
			Destroy (pieces [X, Y]);
		}

		activePiece.transform.Translate (Y - pieceScript.getPosY(), 0, X - pieceScript.getPosX());	//переміщення фігури

		pieceColorArray[pieceScript.getPosX(), pieceScript.getPosY()] = 0;	//зміна значення усіх змінних відповідно до виконаного ходу

		if (pieceScript.color)
			pieceColorArray[X, Y] = 1;
		else
			pieceColorArray[X, Y] = -1;

		pieces [pieceScript.boardPositionX, pieceScript.boardPositionY] = null;
		pieces [X, Y] = activePiece;

		pieceScript.setPos(X, Y);
		pieceScript.moved ();

		if (X == castlingX && Y == castlingY && turn == castlingColor)	
			Castling (X, Y);

		clear ();
		nextTurn();

	}

	public void Castling(int x, int y){		//рокировка
		if (y == blackKing.GetComponent<pieceController>().boardPositionY)	//білий/чорний king
			setActivePiece (blackKing);	
		else
			setActivePiece (whiteKing);
		if (x == 3)	//лівий/правий rook 
			x--;
		else
			x++;
		clear ();
		move (x, y);
		nextTurn();
	}



	//код-прототип механізму "Шах"
//	void Check(){		
//		int x, y;
//		int multip;
//
//		if (!turn) {
//			x = blackKing.GetComponent<pieceController> ().boardPositionX;
//			y = blackKing.GetComponent<pieceController> ().boardPositionY;
//			multip = 1;
//		} else {
//			x = whiteKing.GetComponent<pieceController> ().boardPositionX;
//			y = whiteKing.GetComponent<pieceController> ().boardPositionY;
//			multip = -1;
//		}
//
//		if (figures [x - 1, y + 1 * multip] == multip)
//		if (pieces [x - 1, y + 1 * multip].GetComponent<pawn> ())
//			check = true;
//
//		if (figures [x - 1, y + 1 * multip] == multip)
//		if (pieces [x - 1, y + 1 * multip].GetComponent<pawn> ())
//			check = true;
//	
//
//		for (int i = x - 2; i < x + 3; i++)
//		for (int j = y - 2; j < y + 3; j++) {
//				if ((i > -1) && (i < 8) && (j > -1) && (j < 8))
//				if (((i - x) * (j - y) == 2) || ((i - x) * (j - y) == -2))
//				if (pieces [i, j])
//				if (pieces [i, j].GetComponent<knight> ())
//					check = true;
//			}
//	}

//	public bool CheckTest(){	//шах.перевірка
//		return check;
//	}
}
