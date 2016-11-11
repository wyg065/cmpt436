using UnityEngine;
using System.Collections;
using System;
using System.Net.Sockets;

public class gameMaster : MonoBehaviour {

	public int[,] board;
	public int turn;
	public bool wait;

	//Server Variables
	public TcpClient clientSocket;
	public NetworkStream serverStream;

	//Class Constants
	private const String WYATT_LOCAL_IP_ADDRESS_PORNHUB = "192.168.0.147";
	private const int GAME_SERVER_PORT = 8888;

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

		//Initialize server stuff
		clientSocket = new TcpClient ();
		clientSocket.Connect (WYATT_LOCAL_IP_ADDRESS_PORNHUB, GAME_SERVER_PORT);
		serverStream = default(NetworkStream);
		serverStream = clientSocket.GetStream();

		//send user name
		byte[] outStream = System.Text.Encoding.ASCII.GetBytes ("Jorge");
		serverStream.Write (outStream, 0, outStream.Length);
		serverStream.Flush ();

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
