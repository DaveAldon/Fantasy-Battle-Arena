using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class PlayFabStatsUpdater : MonoBehaviour {

	public string username;
	public int kills;
	public int killsOld;

	void Start () {
		username = Globals.username;

		GetUserDataRequest request = new GetUserDataRequest();
		PlayFabClientAPI.GetUserData(request, LoadPlayerData, OnPlayFabError);
		InvokeRepeating("SavePlayerState", 10, 10);
	}

	private void LoadPlayerData(GetUserDataResult result) {
		Debug.Log("Player data loaded.");
		if(result.Data.ContainsKey("Kills")) {
			Globals.TotalKills = int.Parse(result.Data["Kills"].Value);
		}
		else Globals.TotalKills = 0;
		killsOld = Globals.TotalKills;
	}

	private void SavePlayerState() {
		if(Globals.TotalKills != killsOld) {
			Debug.Log("Saving Player Data...");
			UpdateUserDataRequest request = new UpdateUserDataRequest();
			request.Data = new Dictionary<string, string>();
			request.Data.Add("Kills", Globals.TotalKills.ToString());
			PlayFabClientAPI.UpdateUserData(request, PlayerDataSaved, OnPlayFabError);

			Dictionary<string, int> stats = new Dictionary<string, int>();
			stats.Add("Kills", Globals.TotalKills);
			storeStats(stats);
		}
	}

	public void storeStats(Dictionary<string, int> stats) {
		PlayFab.ClientModels.UpdateCharacterStatisticsRequest request = new PlayFab.ClientModels.UpdateCharacterStatisticsRequest();
		request.CharacterStatistics = stats;
		
		PlayFabClientAPI.UpdateCharacterStatistics(request, StatsUpdated, OnPlayFabError);
	}

	public void onDestroy() {
		SavePlayerState();
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
