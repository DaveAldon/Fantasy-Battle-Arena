using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Shooting : NetworkBehaviour
{
	public int m_PlayerNumber = 1;            // Used to identify the different players.
	public Rigidbody2D m_Shell;                 // Prefab of the shell.
	public Transform m_FireTransform;         // A child of the tank where the shells are spawned.
	private float m_CurrentLaunchForce = 10f;
	private float direction = 1;

	[SyncVar]
	public int m_localID;

	private Rigidbody2D m_Rigidbody2D;          // Reference to the rigidbody component.

	private void Awake()
	{
		// Set up the references.
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	[ClientCallback]
	private void Update()
	{
		if (!isLocalPlayer)
			return;
		
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			direction = -1;
		}

		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			direction = 1;
		}
		else if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			// ... launch the shell.
			Fire();
		}
	}

	private void Fire()
	{
		CmdFire(m_Rigidbody2D.velocity, m_CurrentLaunchForce, m_FireTransform.right, m_FireTransform.position, m_FireTransform.rotation);
	}

	[Command]
	private void CmdFire(Vector2 rigidbodyVelocity, float launchForce, Vector2 right, Vector2 position, Quaternion rotation)
	{
		// Create an instance of the shell and store a reference to it's rigidbody.
		Rigidbody2D shellInstance =
			Instantiate(m_Shell, position, rotation) as Rigidbody2D;

		// Create a velocity that is the tank's velocity and the launch force in the fire position's forward direction.
		Vector2 velocity = rigidbodyVelocity + launchForce * right * direction;

		// Set the shell's velocity to this velocity.
		shellInstance.velocity = velocity;

		NetworkServer.Spawn(shellInstance.gameObject);
	}
}