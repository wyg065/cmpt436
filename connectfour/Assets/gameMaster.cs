using UnityEngine;
using System.Collections;
using System;
using System.Net.Sockets;
using System.Threading;

public class gameMaster : MonoBehaviour
{

	public int[,] board;
	public int turn;
	public bool wait;
	public volatile bool waitForServerResponse;
	public GameObject player2Piece;
	public string stringFromServer;

	//Server Variables
	public TcpClient clientSocket;
	public NetworkStream serverStream;
	public Thread serverThread;

	//Class Constants
	private const String WYATT_LOCAL_IP_ADDRESS_PORNHUB = "192.168.0.147";
	private const int GAME_SERVER_PORT = 8888;

	// Use this for initialization
	void Start ()
	{
		waitForServerResponse = false;
		stringFromServer = "";
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
		serverStream = clientSocket.GetStream ();

		//send user name
		byte[] outStream = System.Text.Encoding.ASCII.GetBytes ("jorge");
		serverStream.Write (outStream, 0, outStream.Length);
		serverStream.Flush ();

		serverThread = new Thread (serverRead);
		serverThread.Start ();
	}

	void serverRead ()
	{
		while (true) {
			int buffSize = 0;
			byte[] inStream = new byte[10025];
			buffSize = clientSocket.ReceiveBufferSize;
			serverStream.Read (inStream, 0, inStream.Length);
			string returnData = System.Text.Encoding.ASCII.GetString (inStream);
			stringFromServer = returnData;
			waitForServerResponse = true;
		}
	}

	void updateBoard (int location)
	{
		switch (location) {
		case 0:
			Instantiate (player2Piece, new Vector3 (-5.25f, 6.0f, transform.position.z), Quaternion.identity);
			specificUpdate (location);
			break;

		case 1:
			Instantiate (player2Piece, new Vector3 (-3.5f, 6.0f, transform.position.z), Quaternion.identity);
			specificUpdate (location);
			break;

		case 2:
			break;

		case 3:
			break;

		case 4:
			break;

		case 5:
			break;

		case 6:
			break;

		default:
			break;
		}
	}

	void specificUpdate (int space)
	{
		//adds piece to 2darray
		for (int i = 0; i < 6; i++) {
			if (board [i, space] != 0) {
				board [i - 1, space] = turn;
				break;
			}
			if (i == 5) {
				board [i, space] = turn;
				break;
			}
		}

		//		String temp = "";
		//		for (int i = 0; i < 6; i++)
		//		{
		//			for (int j = 0; j < 7; j++) {
		//				temp = temp + gameMaster.GetComponent<gameMaster> ().board [i, j] + " ";
		//			}
		//			print (temp);
		//			temp = "";
		//		}
		//		print ("================================");

//		checkWins (turn);
	}

	int[,] stringToBoard (string boardString)
	{
		int[,] functionBoard = new int[6, 7];
		int counter = 0;
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 7; j++) {
				functionBoard [i, j] = int.Parse ((boardString [counter]).ToString ());
				counter++;
			}
		}
			
		String temp = "";
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 7; j++) {
				temp = temp + functionBoard [i, j] + " ";
			}
			print (temp);
			temp = "";
		}
		print ("================================");

		return functionBoard;
	}

	// Update is called once per frame
	void Update ()
	{
		if (waitForServerResponse) {
			int[,] temp = stringToBoard (stringFromServer);
			int location = 0;
			for (int i = 0; i < 6; i++) {
				for (int j = 0; j < 7; j++) {
					if (board [i, j] != temp [i, j]) {
						location = j;
						break;
					}
				}
			}
			updateBoard(location);
			waitForServerResponse = false;
		}	
	}
}
