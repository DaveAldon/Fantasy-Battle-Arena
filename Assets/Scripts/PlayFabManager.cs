using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabManager : MonoBehaviour {

	public string PlayFabId;
	public Button loginButton, registerButton;
	public InputField username, password, email;
	InputField usr, pass, ema;
	Button btn, regBtn;

	void Start () {
		btn = loginButton.GetComponent<Button>();
		regBtn = registerButton.GetComponent<Button>();
		usr = username.GetComponent<InputField>();
		pass = password.GetComponent<InputField>();
		ema = email.GetComponent<InputField>();
	}

	public void TaskOnClickLogin() {
		Login (usr.text, pass.text);
	}

	public void TaskOnClickRegister() {
		Register (usr.text, pass.text, ema.text);
	}

	void Register(string usr, string pass, string ema) {
		RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest () {
			TitleId = PlayFabSettings.TitleId,
			Username = usr,
			DisplayName = usr,
			Password = pass,
			Email = ema
		};

		PlayFabClientAPI.RegisterPlayFabUser(request, (result) => {
			PlayFabId = result.PlayFabId;
			Debug.Log("Got PlayFabID: " + PlayFabId);
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
				Debug.Log("Error logging in player with username:");
				Debug.Log(error.ErrorMessage);
			});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
