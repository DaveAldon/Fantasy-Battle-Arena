using UnityEngine;

public class MatchManager : MonoBehaviour {

	string gameEventsMessage;

	GameObject gameStatsHolder;
	// Use this for initialization
	void Start () {
	}

	public void ReachedWinFirst(int team) {
		
	}

	void OnGUI() {
		gameEventsMessage = "Team 1: " + GetComponent<GameStats>().getTeamKillCount(1).ToString() + " Team 2: " + GetComponent<GameStats>().getTeamKillCount(2).ToString();
		GUI.TextField(new Rect(10, 10, 300, 25), gameEventsMessage);
	}
}
