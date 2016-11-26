using UnityEngine;
using System.Collections;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabManager : MonoBehaviour {

	public string PlayFabId;

	void Login(string titleId)
	{
		LoginWithCustomIDRequest request = new LoginWithCustomIDRequest()
		{
			TitleId = titleId,
			CreateAccount = true,
			CustomId = SystemInfo.deviceUniqueIdentifier
		};

		PlayFabClientAPI.LoginWithCustomID(request, (result) => {
			PlayFabId = result.PlayFabId;
			Debug.Log("Got PlayFabID: " + PlayFabId);

			if(result.NewlyCreated)
			{
				Debug.Log("(new account)");
			}
			else
			{
				Debug.Log("(existing account)");
			}
		},
			(error) => {
				Debug.Log("Error logging in player with custom ID:");
				Debug.Log(error.ErrorMessage);
			});
	}

	// Use this for initialization
	void Start () {
		Login (PlayFabSettings.TitleId);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
