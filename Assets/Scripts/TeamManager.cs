using UnityEngine;
using UnityEngine.Networking ;
using System.Collections;
 
public class TeamManager : NetworkManager
{
	public int playerNumber = 0;

	//Put this in a music class later
	public AudioClip fightSound;
	
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		//Put this in a music class later
		AudioSource.PlayClipAtPoint(fightSound, gameObject.transform.position);

		playerNumber ++ ;
		if(playerNumber > 2) {
			playerNumber = 1;
		}

		if(playerNumber == 1) {
			var player = (GameObject)GameObject.Instantiate(playerPrefab, GameObject.Find("Team1Spawn").GetComponent<Transform>().position, Quaternion.identity);
			player.GetComponent<PlayerStats>().updateTeam(playerNumber);
			NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
			StartCoroutine(GameObjectProperties(player));
		}
		else {
			var player = (GameObject)GameObject.Instantiate(playerPrefab, GameObject.Find("Team2Spawn").GetComponent<Transform>().position , Quaternion.identity);
			player.GetComponent<PlayerStats>().updateTeam(playerNumber);
			NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
			StartCoroutine(GameObjectProperties(player));
		}
	}
	

	IEnumerator GameObjectProperties(GameObject player) {
        yield return new WaitForSeconds(1.0f);
		player.name = player.GetComponent<UsernameSync>().myUsername;
		player.GetComponent<PlayerStats>().updateTeam(playerNumber);
		GameObject.Find("GameStats").GetComponent<GameStats>().CmdUpdatePlayersLoggedIn(player.GetComponent<UsernameSync>().myUsername);
	}
}
