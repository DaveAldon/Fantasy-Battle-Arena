using UnityEngine;
using UnityEngine.Networking;

public class Shooting : NetworkBehaviour
{
	public int m_PlayerNumber = 1; // Used to identify the different players.
	public Rigidbody2D m_Shell; // Prefab of the shell.
	public Transform m_FireTransform; // A child of the player where the bullets are spawned.
	private float m_CurrentLaunchForce = 15f;

	[SyncVar]
	public int m_localID;

	private Rigidbody2D m_Rigidbody2D; // Reference to the rigidbody component.

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	[Command]
	public void CmdPlayerShot (string defenderName, string attackerName)
	{
		if(!hasAuthority) return;
		Debug.Log(defenderName + " has been shot by " + attackerName);
	}

	public void Fire(int direction, string sourceUsername)
	{
		CmdFire(sourceUsername, m_Rigidbody2D.velocity, m_CurrentLaunchForce, m_FireTransform.right, m_FireTransform.position, m_FireTransform.rotation, direction);
	}

	[Command]
	private void CmdFire(string sourceUsername, Vector2 rigidbodyVelocity, float launchForce, Vector2 right, Vector2 position, Quaternion rotation, int direction)
	{
		// Create an instance of the shell and store a reference to its rigidbody. Additially, its team and owner affiliations are set.
		Rigidbody2D shellInstance =
			Instantiate(m_Shell, position, rotation) as Rigidbody2D;
			shellInstance.GetComponent<Bullet>().ownerTeam = gameObject.GetComponent<PlayerStats>().getTeam();
			shellInstance.GetComponent<Bullet>().ownerName = sourceUsername;
			
		// Create a velocity that is the sender's velocity, and the launch force in a forward direction.
		Vector2 velocity = rigidbodyVelocity + launchForce * right * direction;
		shellInstance.velocity = velocity;

		NetworkServer.Spawn(shellInstance.gameObject);
		Destroy(shellInstance.gameObject, 2.0f); //Destory the bullet after 2 seconds
	}
}