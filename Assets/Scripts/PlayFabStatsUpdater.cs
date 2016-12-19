using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Internal;
using System.Collections.Generic;

public class PlayFabStatsUpdater : MonoBehaviour {

	public string username;
	public int kills;
	public int killsOld;
	public Texture btnTexture;

	public static Dictionary<string, Dictionary<string, int>> characterStatistics = new Dictionary<string, Dictionary<string, int>>();
	public static List<CharacterResult> playerCharacters = new List<CharacterResult>();

	void Start () {
		//InvokeRepeating("SavePlayerState", 10, 10);
	}

	void OnGUI() {
		 if (GUI.Button(new Rect(10, 10, 50, 50), btnTexture))
		 updateValues();
	}

	void updateValues() {
		foreach(string playerName in GetComponent<GameStats>().getPlayersLoggedIn()) {
			var player = GameObject.Find(playerName).GetComponent<PlayerStats>();
			username = playerName;
			kills = player.getKills();

			UpdateStatistics();
		}
	}

	public void UpdateStatistics()
    {
		List<StatisticUpdate> stat = new List<StatisticUpdate>();
		StatisticUpdate item = new StatisticUpdate();
		item.StatisticName = "Kills";
		item.Value = kills;
		stat.Add(item);

		var request = new UpdatePlayerStatisticsRequest {
			Statistics = stat			
		};
	
		PlayFab.PlayFabClientAPI.UpdatePlayerStatistics(request, StatResult, OnPlayFabError);
    }

	void StatResult(UpdatePlayerStatisticsResult result) {
		
	}

	public void onDestroy() {
		//SavePlayerState();
	}

	void OnPlayFabError(PlayFabError error) {
		Debug.Log("Error: " + error.ErrorMessage);
	}

	private void StatsUpdated(PlayFab.ClientModels.UpdateCharacterStatisticsResult result) {
		Debug.Log("Stats Updated");
	}

	private void PlayerDataSaved(UpdateUserDataResult result) {
		Debug.Log("Player data saved");
	}
}
