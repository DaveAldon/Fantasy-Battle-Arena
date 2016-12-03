using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetwork : NetworkBehaviour
{
	[SyncVar] private Vector2 SyncedPosition;

	[SerializeField] Transform PlayerTransform;
	[SerializeField] float PlayerSyncRate = 15f;

	void FixedUpdate()
	{
		SendPosition();
		ReceivePosition();
	}

	[Command]
	void CmdSendPosition(Vector2 Position)
	{
		SyncedPosition = Position;
	}

	[ClientCallback]
	void SendPosition()
	{
		if (isLocalPlayer)
		{
			CmdSendPosition(transform.position);
		}
	}

	void ReceivePosition()
	{
		if (!isLocalPlayer)
		{
			transform.position = Vector2.Lerp(transform.position, SyncedPosition, Time.deltaTime * PlayerSyncRate);
		}
	}
}
