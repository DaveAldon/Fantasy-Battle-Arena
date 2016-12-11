using UnityEngine;

public class Bullet : MonoBehaviour {
    public int ownerTeam;
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
            health.TakeDamage(10);
        }

        Destroy(gameObject);
    }
}
