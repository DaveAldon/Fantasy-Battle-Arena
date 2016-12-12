using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    public const int maxHealth = 100;
    [SyncVar(hook = "OnChangeHealth")] //If currentHealth changes, dump its contents into the function called "OnChangeHealth"
    public int currentHealth = maxHealth;
    public RectTransform healthBar;

    public void TakeDamage(int amount)
    {
        if (!isServer) return;
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            RpcRespawn(); // called on the Server, but invoked on the Clients
        }
    }

    void OnChangeHealth (int currentHealth )
    {
        healthBar.sizeDelta = new Vector2(currentHealth , healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            //Move back to a respawn location
            transform.position = Vector2.zero;
        }
    }
}