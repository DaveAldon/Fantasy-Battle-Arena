using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        var hit = coll.gameObject;
        var health = hit.GetComponent<Health>();
        if (health  != null)
        {
            health.TakeDamage(10);
        }

        Destroy(gameObject);
    }
}
