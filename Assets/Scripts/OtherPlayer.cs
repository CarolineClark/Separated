using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Net.Sockets;


public class OtherPlayer : MonoBehaviour {

	SocketClient client;
	string followingPlayer;
	private IEnumerator coroutine;
	Thread WorkerThread;

	void Start () {
		client = new SocketClient ();
		client.SetupSocket ();
		followingPlayer = "4";

		//WorkerThread = new Thread(new ThreadStart(ReadFromSocket));
		//WorkerThread.Start();
	}

	void Update() {
		ReadFromSocket();
	}

	void setPlayerFollowing(string id) {
		followingPlayer = id;
	}

	private void ReadFromSocket() {
		string data = client.ReadSocket ();
		if (!data.Equals ("")) {
			Debug.Log ("other player info found!! data = " + data);
			string[] values = data.Split (',');
			if (values.Length == 3) {
				string id = values [0];
				string x = values [1];
				string y = values [2];
				if (id.Equals (this.followingPlayer)) {
					transform.position = new Vector3 (float.Parse (x), float.Parse (y), -0.1f);
				}
			}
		}
	}

	private void ReadFromSocketForever() {
		while (true) {
			ReadFromSocket ();
		}
	}
}
