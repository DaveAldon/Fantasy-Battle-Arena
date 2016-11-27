using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Shooting : NetworkBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	private int direction = 1;
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			direction = -1;
		}

		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			direction = 1;
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			CmdFire(direction);
		}
	}

	[Command]
	void CmdFire(int direction)
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		//bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * 1000 * direction);
		bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * 6 * direction;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn (bullet);

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);        
	}
}
