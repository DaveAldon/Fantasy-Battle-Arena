using UnityEngine;

public class Bullet : MonoBehaviour {
    public int ownerTeam;
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.GetComponent<Shooting>().team == ownerTeam) {
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
