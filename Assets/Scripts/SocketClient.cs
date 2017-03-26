using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

public class SocketClient {
	internal Boolean socketReady = false;
	TcpClient mySocket;
	NetworkStream theStream;
	StreamWriter theWriter;
	StreamReader theReader;
	String Host = "192.168.0.5";
	Int32 Port = 12345;

	// **********************************************
	public void SetupSocket() {
		try {
			mySocket = new TcpClient(Host, Port);
			theStream = mySocket.GetStream();
			theWriter = new StreamWriter(theStream);
			theReader = new StreamReader(theStream);
			socketReady = true;
		}
		catch (Exception e) {
			Debug.Log("Socket error: " + e);
		}
	}

	public void WriteSocket(string theLine) {
		if (!socketReady)
			return;
		String foo = theLine + "\r\n";
		theWriter.Write(foo);
		theWriter.Flush();
	}

	public String ReadSocket() {
		if (!socketReady)
			return "";
		if (theStream.DataAvailable)
			return theReader.ReadLine();
		return "";
	}

	public void CloseSocket() {
		if (!socketReady)
			return;
		theWriter.Close();
		theReader.Close();
		mySocket.Close();
		socketReady = false;
	}
}