using UnityEngine;
using UnityEngine.Networking ;
using System.Collections;
 
public class TeamManager : NetworkManager
{
	public int playerNumber = 0;
	
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		playerNumber ++ ;
		var player = (GameObject)GameObject.Instantiate(playerPrefab, new Vector2(0,0) , Quaternion.identity);
		player.GetComponent<PlayerStats>().updateTeam(playerNumber);
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		StartCoroutine(GameObjectName(player));
	}

	IEnumerator GameObjectName(GameObject player) {
        yield return new WaitForSeconds(1.0f);
		player.name = player.GetComponent<UsernameSync>().myUsername;
	}
}
