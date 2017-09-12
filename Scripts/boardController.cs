using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardController : MonoBehaviour {

	public GameObject[] board = new GameObject[64];

	public GameObject getCube(int x, int y){
		return board [x + y * 8];
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
