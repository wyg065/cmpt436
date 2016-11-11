using UnityEngine;
using System.Collections;
using System;
using System.Net.Sockets;

public class spawnChip : MonoBehaviour {

	private GameObject playerPiece;
	public GameObject player1Piece;
	public GameObject player2Piece;

	private GameObject gameMaster;

	//Server Variables
	public TcpClient clientSocket;
	public NetworkStream serverStream;

	//Class Constants
	private const String WYATT_LOCAL_IP_ADDRESS = "172.16.1.65";
	private const int GAME_SERVER_PORT = 8888;

	// Use this for initialization
	void Start () {
		gameMaster = GameObject.Find ("GameMaster");

		//Initialize server stuff
		clientSocket = new TcpClient ();
		clientSocket.Connect (WYATT_LOCAL_IP_ADDRESS, GAME_SERVER_PORT);
		serverStream = default(NetworkStream);
		serverStream = clientSocket.GetStream();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		if (!gameMaster.GetComponent<gameMaster> ().wait) {
			if (gameMaster.GetComponent<gameMaster> ().turn == 2){
				updateBoard ();
				gameMaster.GetComponent<gameMaster> ().turn = 1;
			} else if (gameMaster.GetComponent<gameMaster> ().turn == 1) {
				updateBoard ();
				gameMaster.GetComponent<gameMaster> ().turn = 2;
			}
		}
	}

	void updateBoard() {
		
//		string readData = null;
		byte[] outStream = System.Text.Encoding.ASCII.GetBytes ("M4k3 4m3r1c4 Gr8 4g41n");
		serverStream.Write (outStream, 0, outStream.Length);
		serverStream.Flush ();

		if (gameMaster.GetComponent<gameMaster> ().turn == 1) {
			playerPiece = player1Piece;
		}
		if (gameMaster.GetComponent<gameMaster> ().turn == 2) {
			playerPiece = player2Piece;
		}

		if ((this.gameObject.transform.position.x == -5.25f) && (gameMaster.GetComponent<gameMaster> ().board [0, 0] == 0)) {
			Instantiate(playerPiece, new Vector3(transform.position.x, 6.0f, transform.position.z), Quaternion.identity);
			specificUpdate (0);
		}
		if ((this.gameObject.transform.position.x == -3.5f) && (gameMaster.GetComponent<gameMaster> ().board [0, 1] == 0)) {
			Instantiate(playerPiece, new Vector3(transform.position.x, 6.0f, transform.position.z), Quaternion.identity);
			specificUpdate (1);
		}
		if ((this.gameObject.transform.position.x == -1.75f) && (gameMaster.GetComponent<gameMaster> ().board [0, 2] == 0)) {
			Instantiate(playerPiece, new Vector3(transform.position.x, 6.0f, transform.position.z), Quaternion.identity);
			specificUpdate (2);
		}
		if ((this.gameObject.transform.position.x == 0.0f) && (gameMaster.GetComponent<gameMaster> ().board [0, 3] == 0)) {
			Instantiate(playerPiece, new Vector3(transform.position.x, 6.0f, transform.position.z), Quaternion.identity);
			specificUpdate (3);
		}
		if ((this.gameObject.transform.position.x == 1.75f) && (gameMaster.GetComponent<gameMaster> ().board [0, 4] == 0)) {
			Instantiate(playerPiece, new Vector3(transform.position.x, 6.0f, transform.position.z), Quaternion.identity);
			specificUpdate (4);
		}
		if ((this.gameObject.transform.position.x == 3.5f) && (gameMaster.GetComponent<gameMaster> ().board [0, 5] == 0)) {
			Instantiate(playerPiece, new Vector3(transform.position.x, 6.0f, transform.position.z), Quaternion.identity);
			specificUpdate (5);
		}
		if ((this.gameObject.transform.position.x == 5.25f) && (gameMaster.GetComponent<gameMaster> ().board [0, 6] == 0)) {
			Instantiate(playerPiece, new Vector3(transform.position.x, 6.0f, transform.position.z), Quaternion.identity);
			specificUpdate (6);
		}
			
	}

	void specificUpdate (int space) {
		//adds piece to 2darray
			for (int i = 0; i < 6; i++) {
				if (gameMaster.GetComponent<gameMaster> ().board [i, space] != 0) {
					gameMaster.GetComponent<gameMaster> ().board [i-1, space] = gameMaster.GetComponent<gameMaster> ().turn;
					break;
				}
				if (i == 5) {
					gameMaster.GetComponent<gameMaster> ().board [i, space] = gameMaster.GetComponent<gameMaster> ().turn;
					break;
				}
			}

		String temp = "";
		for (int i = 0; i < 6; i++)
		{
			for (int j = 0; j < 7; j++) {
				temp = temp + gameMaster.GetComponent<gameMaster> ().board [i, j] + " ";
			}
			print (temp);
			temp = "";
		}
		print ("================================");

		checkWins (gameMaster.GetComponent<gameMaster> ().turn);
	}


	void checkWins(int team) {
		//checks up and down wins
		for (int i = 1; i < 4; i++) {
			for (int j = 0; j < 7; j++) {
				if (gameMaster.GetComponent<gameMaster> ().board [i-1, j] == team && gameMaster.GetComponent<gameMaster> ().board [i, j] == team
					&& gameMaster.GetComponent<gameMaster> ().board [i+1, j] == team && gameMaster.GetComponent<gameMaster> ().board [i+2, j] == team) {
					print (team+" wins");
					gameMaster.GetComponent<gameMaster> ().wait = true;
				}
			}
		}

		//checks left and right wins
		for (int i = 0; i < 6; i++) {
			for (int j = 1; j < 5; j++) {
				if (gameMaster.GetComponent<gameMaster> ().board [i, j-1] == team && gameMaster.GetComponent<gameMaster> ().board [i, j] == team
					&& gameMaster.GetComponent<gameMaster> ().board [i, j+1] == team && gameMaster.GetComponent<gameMaster> ().board [i, j+2] == team) {
					print (team+" wins");
					gameMaster.GetComponent<gameMaster> ().wait = true;
				}
			}
		}

		//checks diagonal bottom-left to top-right
		for (int i = 3; i < 6; i++) {
			for (int j = 0; j < 4; j++) {
				if (gameMaster.GetComponent<gameMaster> ().board [i, j] == team && gameMaster.GetComponent<gameMaster> ().board [i-1, j+1] == team
					&& gameMaster.GetComponent<gameMaster> ().board [i-2, j+2] == team && gameMaster.GetComponent<gameMaster> ().board [i-3, j+3] == team) {
					print (team+" wins");
					gameMaster.GetComponent<gameMaster> ().wait = true;
				}
			}
		}

		//checks diagonal bottom-right to top-left
		for (int i = 3; i < 6; i++) {
			for (int j = 4; j < 7; j++) {
				if (gameMaster.GetComponent<gameMaster> ().board [i, j] == team && gameMaster.GetComponent<gameMaster> ().board [i-1, j-1] == team
					&& gameMaster.GetComponent<gameMaster> ().board [i-2, j-2] == team && gameMaster.GetComponent<gameMaster> ().board [i-3, j-3] == team) {
					print (team+" wins");
					gameMaster.GetComponent<gameMaster> ().wait = true;
				}
			}
		}
	}
}
