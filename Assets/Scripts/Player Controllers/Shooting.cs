﻿using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Shooting : NetworkBehaviour
{
	public int team;
	public string ownerName;
	public int m_PlayerNumber = 1;            // Used to identify the different players.
	public Rigidbody2D m_Shell;                 // Prefab of the shell.
	public Transform m_FireTransform;         // A child of the player where the bullets are spawned.
	private float m_CurrentLaunchForce = 15f;

	public Text myOwnerName;

	[SyncVar]
	public int m_localID;

	private Rigidbody2D m_Rigidbody2D;          // Reference to the rigidbody component.

	private void Awake()
	{
		// Set up the references.
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	[Command]
	public void CmdPlayerShot (string _playerID, string _sourceID)
	{
		Debug.Log(_playerID + " has been shot by " + _sourceID);
	}

	public void Fire(int direction, string sourceUsername)
	{
		CmdFire(sourceUsername, m_Rigidbody2D.velocity, m_CurrentLaunchForce, m_FireTransform.right, m_FireTransform.position, m_FireTransform.rotation, direction);
	}

	[Command]
	private void CmdFire(string sourceUsername, Vector2 rigidbodyVelocity, float launchForce, Vector2 right, Vector2 position, Quaternion rotation, int direction)
	{
		// Create an instance of the shell and store a reference to it's rigidbody.
		Rigidbody2D shellInstance =
			Instantiate(m_Shell, position, rotation) as Rigidbody2D;
			
			shellInstance.GetComponent<Bullet>().ownerTeam = team;
			shellInstance.GetComponent<Bullet>().ownerName = sourceUsername;
			
			//shellInstance.GetComponent<Bullet>().ownerName = GetComponent<PlayerController>().publicUsername.text;

		// Create a velocity that is the tank's velocity and the launch force in the fire position's forward direction.
		Vector2 velocity = rigidbodyVelocity + launchForce * right * direction;

		// Set the shell's velocity to this velocity.
		shellInstance.velocity = velocity;

		NetworkServer.Spawn(shellInstance.gameObject);

		//Destory the bullet after 2 seconds
		//Destroy(shellInstance.gameObject, 2.0f);
	}
}