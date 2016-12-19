using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	public Text welcomeMessage;

	void Start () {
		welcomeMessage = welcomeMessage.GetComponent<Text> ();
		welcomeMessage.text = "Welcome " + Globals.username +"!";	
	}
}
