using UnityEngine.Networking;
using UnityEngine.UI;

public class UsernameSync : NetworkBehaviour {

	public Text publicUsername;

	[SyncVar]
	public string myUsername = "Ay";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Super inefficient
		myUsername = publicUsername.text;
	}
}
