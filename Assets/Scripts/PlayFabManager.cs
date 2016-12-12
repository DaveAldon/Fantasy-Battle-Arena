using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class PlayFabManager : MonoBehaviour {

	public InputField username, password, email;
	InputField usr, pass, ema;

	void Start () {
		usr = username.GetComponent<InputField>();
		pass = password.GetComponent<InputField>();
		ema = email.GetComponent<InputField>();
	}

	public void TaskOnClickLogin() {
		Login (usr.text, pass.text);
		Globals.username = usr.text;
		Globals.password = pass.text;
	}

	public void TaskOnClickRegister() {
		Register (usr.text, pass.text, ema.text);
		Globals.username = usr.text;
		Globals.password = pass.text;
	}

	void Register(string usr, string pass, string ema) {
		RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest () {
			TitleId = "CB46", //PlayFabSettings.TitleId,
			Username = usr,
			DisplayName = usr,
			Password = pass,
			Email = ema
		};

		PlayFabClientAPI.RegisterPlayFabUser(request, (result) => {
			Globals.PlayFabId = result.PlayFabId;
			Debug.Log("Got PlayFabID: " + Globals.PlayFabId);
		},
			(error) => {
				Debug.Log("Error logging in player with username:");
				Debug.Log(error.ErrorMessage);
			});
	}

	void Login(string usr, string pass)
	{
		LoginWithPlayFabRequest request = new LoginWithPlayFabRequest()
		{
			TitleId = PlayFabSettings.TitleId,
			Username = usr,
			Password = pass
		};

		PlayFabClientAPI.LoginWithPlayFab(request, (result) => {
			Globals.PlayFabId = result.PlayFabId;
			Debug.Log("Got PlayFabID: " + Globals.PlayFabId);

			if(result.NewlyCreated)
			{
				Debug.Log("(new account)");
			}
			else
			{
				Debug.Log("(existing account)");
			}
			LoadScene();
		},
			(error) => {
				Debug.Log("Error logging in player with username:");
				Debug.Log(error.ErrorMessage);
			});
	}

	void LoadScene() {
		SceneManager.LoadScene("Menu");
		/*
		var CanvasObject = GameObject.Find("Canvas");
		GetComponent<NetworkManager>().enabled = true;
		CanvasObject.GetComponent<Canvas> ().enabled = false;
		*/
	}
}
