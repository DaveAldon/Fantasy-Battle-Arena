using UnityEngine;
using UnityEngine.Networking ;
 
public class TeamManager : NetworkManager
{
	int playerNumber = 0;
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		playerNumber ++ ;
		var player = (GameObject)GameObject.Instantiate(playerPrefab, new Vector2(0,0) , Quaternion.identity);
		if (playerNumber == 1 ) { player.GetComponent<Shooting>().team = 1 ; }
		if (playerNumber == 2 ) { player.GetComponent<Shooting>().team = 2 ; }
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}
}
