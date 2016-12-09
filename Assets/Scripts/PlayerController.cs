using UnityEngine;
using UnityEngine.Networking;

 public class PlayerController : NetworkBehaviour {
 
     public float speed =8.0f;
     public Animator anim;
	 private int currentState = 0;
	 public int direction;
	 public bool isLeft;
	 public bool isRight;
	 public bool isIdle;
	 public bool lastFacedLeft;
     private bool m_Jump;   
	  
     // Use this for initialization
     void Start () {
         anim = gameObject.GetComponent<Animator> ();
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
			CmdMove(direction, m_Jump);
		}
	}

	private void FixedUpdate() {
		if(isLocalPlayer) {

			if(Input.GetKey(KeyCode.A)) {
				direction = -1;
				currentState = 1;
				isLeft = true;
				isRight = false;
				isIdle = false;
				lastFacedLeft = true;
			}
			if(Input.GetKey(KeyCode.D)) {
				GetComponent<SpriteRenderer>().flipX = false;
				direction = 1;
				currentState = 2;
				isLeft = false;
				isRight = true;
				isIdle = false;
				lastFacedLeft = false;
			}
			if(Input.GetKey(KeyCode.Space)) {
				currentState = 3;
			}
			if((Input.GetKey(KeyCode.A) == false) && (Input.GetKey(KeyCode.D) == false)) {
				direction = 0;
				currentState = 0;
				isIdle = true;
				isLeft = false;
				isRight = false;
			}

       		CmdSpriteChange(currentState);
    		SpriteChange(currentState);

			m_Jump = false;

			Move(direction, m_Jump);
		}
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
			if(lastFacedLeft) {
			anim.Play("IdleLeft");
			} else anim.Play("Idle");
		}
		else if (sprite == 1)
		{
			anim.Play("WalkLeft");
		}
		else if (sprite == 2)
		{
			anim.Play("Walk");
		}
	}

	void SpriteChange(int sprite)
	{
		if (sprite == 0)
		{
			if(lastFacedLeft) {
			anim.Play("IdleLeft");
			} else anim.Play("Idle");
		}
		else if (sprite == 1)
		{
			anim.Play("WalkLeft");
		}
		else if (sprite == 2)
		{
			anim.Play("Walk");
		}
	}
 }