using UnityEngine.Networking;
using System.Collections.Generic;

public class GameStats : NetworkBehaviour {

	[SyncVarAttribute]
	public int Team1KillCount = 0;
	[SyncVarAttribute]
	public int Team2KillCount = 0;

	public int killsToWin = 3;
	public List<string> PlayersLoggedIn = new List<string>();

	public List<string> getPlayersLoggedIn() {
		return PlayersLoggedIn;
	}

	[CommandAttribute]
	public void CmdUpdatePlayersLoggedIn(string username) {
		RpcUpdatePlayersLoggedIn(username);
	}

	[ClientRpc]
    void RpcUpdatePlayersLoggedIn(string username)
    {
        PlayersLoggedIn.Add(username);
    }
	
	public int getTeam1KillCount() {
		return Team1KillCount;
	}

	public void updateTeamKillCount(int teamNumber, int kill) {
		if(teamNumber == 1) Team1KillCount += kill;
		else if(teamNumber == 2) Team2KillCount += kill;
	}

	public int getTeamKillCount(int teamNumber) {
		if(teamNumber == 1) return Team1KillCount;
		else return Team2KillCount;
	}
}
