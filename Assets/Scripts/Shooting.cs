using UnityEngine;
using UnityEngine.Networking;

public class Shooting : NetworkBehaviour
{
	public int m_PlayerNumber = 1;            // Used to identify the different players.
	public Rigidbody2D m_Shell;                 // Prefab of the shell.
	public Transform m_LeftFireTransform;         // A child of the player where the bullets are spawned.
	public Transform m_RightFireTransform;
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

		if (Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKeyDown(KeyCode.A)))
		{
			direction = -1;
		}

		if (Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.D)))
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
		if(direction == 1) {
			CmdFire(m_Rigidbody2D.velocity, m_CurrentLaunchForce, m_LeftFireTransform.right, m_LeftFireTransform.position, m_LeftFireTransform.rotation);
		}
		else {
			CmdFire(m_Rigidbody2D.velocity, m_CurrentLaunchForce, m_RightFireTransform.right, m_RightFireTransform.position, m_RightFireTransform.rotation);
		}
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

		//Destory the bullet after 2 seconds
		Destroy(shellInstance.gameObject, 2.0f);
	}
}