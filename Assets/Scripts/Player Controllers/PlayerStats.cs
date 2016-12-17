using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour {

	[SyncVarAttribute]
	public int kills = 0;
	[SyncVarAttribute]
	public int deaths = 0;
	[SyncVarAttribute]
	public int team;
	[SyncVarAttribute]
	public string whoLastHitMe;
	public int whatTeamLastHitMe;

	public void updateKills(int kill) {
		kills += kill;
	}

	public int getKills() {
		return kills;
	}

	public void updateDeaths(int death) {
		deaths += death;
	}

	public int getDeaths() {
		return deaths;
	}

	public void updateLastHit(string who) {
		whoLastHitMe = who;
	}

	public int getWhatTeamHitMe() {
		return whatTeamLastHitMe;
	}

	public void updateTeamLastHitMe(int teamNumber) {
		whatTeamLastHitMe = teamNumber;
	}

	public string getLastHit() {
		return whoLastHitMe;
	}

	public void updateTeam(int teamNumber) {
		team = teamNumber;
	}

	public int getTeam() {
		return team;
	}
}
