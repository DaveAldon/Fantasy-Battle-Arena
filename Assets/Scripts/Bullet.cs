using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {
    
    [SyncVar]
    public string ownerName;
    public int ownerTeam;

    void OnTriggerEnter2D(Collider2D coll)
    {
        //We don't want to do anything if the collided object doesn't have a Shooting script, or if the object is on your team
        if((coll.GetComponent<Shooting>() == null) || (coll.GetComponent<PlayerStats>().getTeam() == ownerTeam)) return;
        var hit = coll.gameObject;
        var health = hit.GetComponent<Health>();
        if (health  != null)
        {
            coll.GetComponent<Shooting>().CmdPlayerShot(coll.GetComponent<UsernameSync>().myUsername, ownerName);
            coll.GetComponent<PlayerStats>().updateLastHit(ownerName);
            coll.GetComponent<PlayerStats>().updateTeamLastHitMe(ownerTeam);
            health.TakeDamage(10);
        }

        Destroy(gameObject);
    }
}
