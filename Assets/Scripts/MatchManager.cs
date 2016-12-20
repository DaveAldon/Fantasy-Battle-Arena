using UnityEngine;

public class MatchManager : MonoBehaviour {

	string gameEventsMessage;
	public GameObject winScreen;
	public GameObject loseScreen;
	public string username;
	GameObject gameStatsHolder;
	
	void Start () {
		winScreen.SetActive(false);
		loseScreen.SetActive(false);
		username = Globals.username;
	}

	void Update() {
		if(GetComponent<GameStats>().getTeamKillCount(1) > 3) ReachedWinFirst(1);
		if(GetComponent<GameStats>().getTeamKillCount(2) > 3) ReachedWinFirst(2);
	}

	public void ReachedWinFirst(int team) {
		if(team == GameObject.Find(username).GetComponent<PlayerStats>().getTeam()) {
			winScreen.SetActive(true);
		} else loseScreen.SetActive(true);
	}

	void OnGUI() {
		gameEventsMessage = "Team 1: " + GetComponent<GameStats>().getTeamKillCount(1).ToString() + " Team 2: " + GetComponent<GameStats>().getTeamKillCount(2).ToString();
		GUI.TextField(new Rect(10, 10, 300, 25), gameEventsMessage);
	}
}
