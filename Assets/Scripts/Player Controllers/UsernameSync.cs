using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

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
		gameObject.name = myUsername;
		CmdGameObjectName(gameObject, myUsername);
	}

	void Update() {
		//Everyone who does not have authority over this object needs to be constantly updating the username value from the visible text
		//There should be a more efficient way to do this, however it's not that resource heavy.
		if(!hasAuthority) {
			myUsername = publicUsername.text;
		}
		if (!isLocalPlayer) return;
		CmdSetName(publicUsername.text);
		CmdGameObjectName(gameObject, myUsername);
	}

	void LateUpdate() {
		if (isLocalPlayer) {
         //CmdGameObjectName(gameObject, myUsername);
     }
	}

	[Command]
	void CmdGameObjectName(GameObject me, string username) {
		RpcName(me, username);
	}

	[ClientRpc]
	void RpcName(GameObject me, string username) {
		if (isLocalPlayer) return;
		me.name = username;
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
