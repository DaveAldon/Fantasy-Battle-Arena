using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {
    public int ownerTeam;
    
    [SyncVar]
    public string ownerName;

    void Start() {
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        //We don't want to do anything if the collided object doesn't have a Shooting script, or if the object is on your team
        if((coll.GetComponent<Shooting>() == null) || (coll.GetComponent<Shooting>().team == ownerTeam)) {
            return;
        }
        
        var hit = coll.gameObject;
        var health = hit.GetComponent<Health>();
        if (health  != null)
        {
            coll.GetComponent<Shooting>().CmdPlayerShot(coll.GetComponent<UsernameSync>().myUsername, ownerName);
            health.TakeDamage(10);
        }

        Destroy(gameObject);
    }
}
