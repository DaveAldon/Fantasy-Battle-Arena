using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
	public Vector2 temp;
	public float X = 10;

	void Start () {
		temp = transform.position;
	}

	void Update () {
		if(Input.GetKey("left")) {
			// Gamer pushes left arrow key
			// Set texture to normal position
			temp.x += -X * Time.deltaTime;
		}
		else if (Input.GetKey("right")) {
			// Gamer pushes right arrow key
			// Flip texture
			temp.x += X * Time.deltaTime;
		}
		transform.position = temp;
	}
}