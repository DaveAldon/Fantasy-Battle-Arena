using UnityEngine;
using UnityEngine.Networking;
using System;

public class NetworkAnimStates : NetworkBehaviour {
	public Animations CurrentAnim = Animations.Idle;
	public GameObject Player2d;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Player2d.GetComponent<Animation>().CrossFade(Enum.GetName(typeof(Animations), CurrentAnim));
	}

	public void SyncAnimation(string AnimName) {
		CurrentAnim = (Animations)Enum.Parse(typeof(Animations), AnimName);
	}
}

public enum Animations {
	Idle,
	Walk,
	Jump,
	Fall
}
