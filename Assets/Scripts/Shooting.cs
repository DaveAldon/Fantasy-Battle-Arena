using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Shooting : NetworkBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			Fire();
		}
	}

	void Fire()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet

		bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * 1000);

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);        
	}
}
