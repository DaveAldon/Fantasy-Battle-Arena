using UnityEngine.Networking;
using UnityEngine.UI;

public class UsernameSync : NetworkBehaviour {

	//We want this username to sync throughout all of the clients so that they can differentiate each player
	[SyncVar]
	public string myUsername;
	public Text publicUsername;

	void Start () {
		if (!isLocalPlayer) return;
		//Initialization of the player's username string and visible text box
		myUsername = Globals.username;
		publicUsername.text = myUsername;
	}

	void Update() {
		//Everyone who does not have authority over this object needs to be constantly updating the username value from the visible text
		//There should be a more efficient way to do this, however it's not that resource heavy.
		if(!hasAuthority) {
			myUsername = publicUsername.text;
		}
		CmdSetName(publicUsername.text);
	}

	[Command]
	void CmdSetName(string username) {
		RpcSetName(username);
	}

	[ClientRpc]
    void RpcSetName(string username)
    {
        if (isLocalPlayer) return;
		publicUsername.text = username;
    }
}
