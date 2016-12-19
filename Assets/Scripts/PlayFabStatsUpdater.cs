using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class PlayFabStatsUpdater : MonoBehaviour {

	public string username;
	public int kills;
	public int killsOld;
	public Texture btnTexture;

	void Start () {
		kills = 0;
		killsOld = 0;
		GetStatistics();
		InvokeRepeating("updateValues", 10, 10);
	}

	/*
	void OnGUI() {
		 if (GUI.Button(new Rect(10, 10, 50, 50), btnTexture))
		 updateValues();
	}
	*/

	void updateValues() {
		kills = GetComponent<PlayerStats>().getKills();
		if(kills != killsOld) {
			username = gameObject.name;
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
		killsOld = kills;
    }

	void GetStatistics() {
		List<string> stat = new List<string>();
		stat.Add("Kills");
		var request = new GetPlayerStatisticsRequest {
			StatisticNames = stat
		};
		PlayFab.PlayFabClientAPI.GetPlayerStatistics(request, GetStatResult, OnPlayFabError);
	}

	void GetStatResult(GetPlayerStatisticsResult result) {
		GetComponent<PlayerStats>().updateKills(result.Statistics[0].Value);
	}
	
	void StatResult(UpdatePlayerStatisticsResult result) {
		
	}

	public void onDestroy() {
		updateValues();
	}

	void OnPlayFabError(PlayFabError error) {
		Debug.Log("Error: " + error.ErrorMessage);
	}
}
