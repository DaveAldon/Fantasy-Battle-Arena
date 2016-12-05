using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkAnimController : MonoBehaviour {
	public float V;
	public float H;
	public NetworkAnimStates States;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		V = Mathf.Abs(Input.GetAxis(PC2D.Input.HORIZONTAL));
		H = Mathf.Abs(Input.GetAxis(PC2D.Input.VERTICAL));

		if(V < 0) {
			States.SyncAnimation("Walk");
		}
		else if (V > 0) {
			States.SyncAnimation("Walk");
		}
		else States.SyncAnimation("Idle");
	}
}
