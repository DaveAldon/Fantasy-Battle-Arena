using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

	void Start() {
		//target = GameObject.Find ("").transform;
	}

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = target.position.x;
        pos.y = target.position.y;

        transform.position = pos;
    }
}
