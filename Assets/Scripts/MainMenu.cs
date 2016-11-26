using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class MainMenu : MonoBehaviour {

	public Text welcomeMessage;

	// Use this for initialization
	void Start () {
		welcomeMessage = welcomeMessage.GetComponent<Text> ();
		welcomeMessage.text = "Welcome " + Globals.username +"!";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
