using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeController : MonoBehaviour {

	private bool activated = false;

	private gameController gameContr;

	public int X;
	public int Y;

	// Use this for initialization
	void Start () {
		gameContr = GameObject.FindWithTag ("controller").GetComponent<gameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Activate(){
		activated = true;
	}

	public void Dectivate(){
		activated = false;
	}

	public bool isActivated(){
		return activated;
	}

	void OnMouseDown(){
		if (!activated)
			return;
		gameContr.move (X, Y);
	}
}
