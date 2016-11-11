using UnityEngine;
using System.Collections;
using System;

public class gameMaster : MonoBehaviour {

	public int[,] board;
	public int turn;
	public bool wait;

	// Use this for initialization
	void Start () {
		turn = 1;
		wait = false;
		board = new int[6, 7];
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 7; j++) {
				board [i, j] = 0;
			}
		}

		String temp = "";
		for (int i = 0; i < 6; i++)
		{
			for (int j = 0; j < 7; j++) {
				temp = temp + board [i, j] + " ";
			}
			print (temp);
			temp = "";
		}
		print ("================================");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
