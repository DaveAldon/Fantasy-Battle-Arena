using UnityEngine;
using UnityEngine.Networking;

 public class PlayerController : NetworkBehaviour {
 
     public float speed =8.0f;
//     private string axisName = "Horizontal";
     public Animator anim;

	 private int currentState = 0;

	 public int direction;

	 //private Vector2 m_Move;
     private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
 
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
				//currentState = 2;
			}
		}

		// This is executed on the sever, and results in a RPC on the client
		if (hasAuthority) {
			CmdMove(direction, m_Jump);
			CmdSetAnim();
		}
		//UpdateAnimator(currentState);
	}

	private void FixedUpdate() {
		if(isLocalPlayer) {

			if(Input.GetKey(KeyCode.A)) {
				direction = -1;
				currentState = 1;
			}
			if(Input.GetKey(KeyCode.D)) {
				direction = 1;
				currentState = 1;
			}
			if(Input.GetKey(KeyCode.Space)) {
				currentState = 2;
			}
			if((Input.GetKey(KeyCode.A) == false) && (Input.GetKey(KeyCode.D) == false)) {
				direction = 0;
				currentState = 0;
			}

			m_Jump = false;

			Move(direction, m_Jump);
		}
	}

	[Command]
	public void CmdSetAnim()
	{
		if(!isServer)
		{
			UpdateAnimator(currentState);
		}
		RpcSetAnim();
	}

	[ClientRpc]
	public void RpcSetAnim()
	{
		UpdateAnimator(currentState);
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
		//if (move.magnitude > 1f) move.Normalize();

		Vector2 newScale = transform.localScale;
                 newScale.x = 1.0f;
                 transform.localScale = newScale;  

		transform.position += transform.right * dir * speed * Time.deltaTime;
		//move = transform.InverseTransformDirection(move);

		// send input and other state parameters to the animator
		//UpdateAnimator(currentState);
	}
	void UpdateAnimator(int state) {
		switch(state) {
			case 0:
				anim.SetFloat("Speed", Mathf.Abs(direction));
				break;
			case 1:
				anim.SetFloat("Speed", Mathf.Abs(direction));
				break;
			case 2:
				anim.SetTrigger("Jump");
				break;
			default:
                anim.SetFloat("Speed", Mathf.Abs(direction));
                break;
		}
	}
 }