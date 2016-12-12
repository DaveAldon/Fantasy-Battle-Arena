using UnityEngine;
using UnityEngine.Networking ;
 
public class TeamManager : NetworkManager
{
	public int playerNumber = 0;
	
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		Debug.Log("ass");
		playerNumber ++ ;
		var player = (GameObject)GameObject.Instantiate(playerPrefab, new Vector2(0,0) , Quaternion.identity);

		player.GetComponent<Shooting>().team = playerNumber;
		
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}
	
}
