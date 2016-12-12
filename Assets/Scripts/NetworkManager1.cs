using UnityEngine;
using System.Collections;

public class NetworkManager1 : MonoBehaviour {
	
	bool isRefreshing = false;
	float refreshRequestLength = 3.0f;
	HostData[] hostData;
	string registerGameName = "Official_Server";
	public bool team1 = false;
	public bool team2 = false;
	public bool choose = false;
	private void Start()
	{
		Application.runInBackground = true;
	}
	
	private void SpawnPlayer()
	{
		//Chooses player from booleans
		if (team1 == true) 
		{
			Network.Instantiate (Resources.Load ("2DPlayer"), new Vector2 (0f, 2.5f), Quaternion.identity, 0);
			team1 = false;
			choose = false;
		}
		if (team2 == true) 
		{
			Network.Instantiate (Resources.Load ("2DPlayer"), new Vector2 (0f, 2.5f), Quaternion.identity, 0);
			team2 = false;
			choose = false;
		}
		
	}
	private void StartServer()
	{
		Network.InitializeServer(16, 25002, false);
		MasterServer.RegisterHost(registerGameName, "Game", "Test Server");
	}
	public IEnumerator RefreshHostList()
	{
		Debug.Log ("Refreshing.....");
		MasterServer.RequestHostList(registerGameName);
		float timeStarted = Time.time;
		float timeEnd = Time.time * refreshRequestLength;
		
		while (Time.time < timeEnd) {
			hostData = MasterServer.PollHostList ();
			yield return new WaitForEndOfFrame();
		}
		
		if (hostData == null || hostData.Length == 0) 
		{
			Debug.Log ("No Active Servers Found");
		} else
			Debug.Log (hostData.Length + " has been found");
	}
	void OnServerInitialized()
	{
		Debug.Log ("Servers Initilized");
		choose = true;
	}
	void OnPlayerDisconnected(NetworkPlayer player)
	{
		//if player disconnects destroy its last player 
		Debug.Log ("Player disconnected from: " + player.ipAddress + ":" + player.port);
		Network.RemoveRPCs (player);
		Network.DestroyPlayerObjects (player);
	}
	void onApplicationQuit()
	{
		if (Network.isServer) 
		{
			Network.Disconnect(200);
			MasterServer.UnregisterHost();
		}
		
		if (Network.isClient) 
		{
			Network.Disconnect(200);
		}
	}
	void OnMasterServerEvent(MasterServerEvent masterServerEvent)
	{
		if (masterServerEvent == MasterServerEvent.RegistrationSucceeded) {
			Debug.Log ("Registration Success");
		}
	}
	public void OnGUI()
	{
		//Down here we set booleans to were we can spawn the player
		if (Network.isClient) 
		{
			if (GUI.Button(new Rect (25f, 270f, 150f, 30f), "team1"))
			{
				SpawnPlayer();
				team1 = true;
			}
			if (GUI.Button(new Rect (25f, 480f, 150f, 30f), "team2"))
			{
				SpawnPlayer();
				team2 = true;
			}
		}
		if (choose == true) {
			if (GUI.Button(new Rect (10f, 270f, 60f, 60f), "team1"))
			{
				SpawnPlayer();
				team1 = true;
			}
			if (GUI.Button(new Rect (10f, 340f, 60f, 60f), "team2"))
			{
				SpawnPlayer();
				team2 = true;
			}	
		}
		if (Network.isClient || Network.isServer)
			return;
		//Starts server
		if (GUI.Button (new Rect (25f, 25f, 150f, 30f), "Start Server")) 
		{
			StartServer();
		}
		//Joins server
		if (GUI.Button (new Rect (25f, 65f, 150f, 30f), "Refresh Server List")) 
		{
			StartCoroutine("RefreshHostList");
		}
		if (hostData != null) {
			for(int i = 0; i < hostData.Length; i++)
			{
				if(GUI.Button (new Rect(25f, 125f, 150f, 150f), hostData[i].gameName))
				{
					Debug.Log("Joined Server as Client");
					Network.Connect(hostData[i]);
				}
			}
		}
		
	}
}
