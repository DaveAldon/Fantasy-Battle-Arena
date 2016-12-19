using UnityEngine;
using UnityEngine.Networking ;
using System.Collections;
 
public class TeamManager : NetworkManager
{
	public int playerNumber = 0;
	
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		playerNumber ++ ;
		if(playerNumber > 2) {
			playerNumber = 1;
		}
		var player = (GameObject)GameObject.Instantiate(playerPrefab, new Vector2(0,0) , Quaternion.identity);
		player.GetComponent<PlayerStats>().updateTeam(playerNumber);
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		StartCoroutine(GameObjectProperties(player));
	}

	IEnumerator GameObjectProperties(GameObject player) {
        yield return new WaitForSeconds(1.0f);
		player.name = player.GetComponent<UsernameSync>().myUsername;
		player.GetComponent<PlayerStats>().updateTeam(playerNumber);
		GameObject.Find("GameStats").GetComponent<GameStats>().CmdUpdatePlayersLoggedIn(player.GetComponent<UsernameSync>().myUsername);
	}
}
