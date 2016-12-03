using UnityEngine;
using UnityEngine.UI;

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
