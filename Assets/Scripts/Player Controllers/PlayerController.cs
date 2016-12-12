using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

 public class PlayerController : NetworkBehaviour {
 
    public float speed =8.0f;
    public Animator anim;
	private int currentState = 0;
	public int direction = 1;
	public bool lastFacedLeft;
    private bool m_Jump;  
   
   [SyncVar]
	public string myUsername;
    public Text publicUsername;

    public RectTransform healthBar;

    void Start() {

        anim = gameObject.GetComponent<Animator> ();

		if (isLocalPlayer) { //if I am the owner of this prefab
			Camera.main.GetComponent<CameraFollow>().target = transform;
			myUsername = Globals.username;
			GetComponent<Shooting>().ownerName = myUsername;
			publicUsername.text = myUsername;
    	}
		//this.name = publicUsername.text;
     }

	private void Update()
	{
		if (isLocalPlayer)
		{
			if (!m_Jump)
			{
				m_Jump = Input.GetKeyDown(KeyCode.Space);
			}
		}

		// This is executed on the sever, and results in a RPC on the client
		if (hasAuthority) {
			CmdSetName(myUsername); //This needs to be set once on connect, not every frame. But at least it works currently
			CmdMove(direction, m_Jump);
		}
	}

	private void FixedUpdate() {
		if(isLocalPlayer) {
			if(Input.GetKey(KeyCode.A)) {
				direction = -1;
				currentState = 2;
				lastFacedLeft = true;
			}
			else if(Input.GetKey(KeyCode.D)) {
				direction = 1;
				currentState = 3;
				lastFacedLeft = false;
			}
			else if(Input.GetKey(KeyCode.Space)) {
				currentState = 3;
			}
			else {
				direction = 0;
				if(lastFacedLeft) {
					currentState = 0;
				} else {
					currentState = 1;
				}
			}

			//We seperate this command from the movement inputs because we want to be able to attack while moving
			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				if(lastFacedLeft) {
					GetComponent<Shooting>().Fire(-1, GetComponent<UsernameSync>().myUsername);
				}
				else GetComponent<Shooting>().Fire(1, GetComponent<UsernameSync>().myUsername);
			}

       		CmdSpriteChange(currentState);

			m_Jump = false;

			Move(direction, m_Jump);
		}
	}

	[Command]
	void CmdSetName(string username) {
		RpcSetName(username);
	}

	[ClientRpc]
    void RpcSetName(string name)
    {
        if (isLocalPlayer)
			return;
           
		publicUsername.text = name;
    }

	[Command]
	void CmdMove(int dir, bool jump)
	{
		RpcMove(dir, jump);
	}

	[ClientRpc]
	void RpcMove(int dir, bool jump)
	{
		if (isLocalPlayer)
			return;

		direction = dir;
		m_Jump = jump;
		Move(dir, jump);
	}

	public void Move(int dir, bool jump)
	{
		Vector2 newScale = transform.localScale;
                 newScale.x = 1.0f;
                 transform.localScale = newScale;  

		transform.position += transform.right * dir * speed * Time.deltaTime;
	}

	[Command]
	public void CmdSpriteChange(int sprite)
	{
		RpcSpriteChange(sprite);
	}

	[ClientRpc]
	void RpcSpriteChange(int sprite)
	{
		if (sprite == 0)
		{
			anim.Play("IdleLeft");
		}
		else if (sprite == 1)
		{
			anim.Play("Idle");
		}
		else if (sprite == 2)
		{
			anim.Play("WalkLeft");
		}
		else if (sprite == 3)
		{
			anim.Play("Walk");
		}
	}
 }