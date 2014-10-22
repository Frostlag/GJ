using UnityEngine;
using System.Collections;

public class Earth : MonoBehaviour {

	float oldY;

	public float stilltime;
	public float movedist;
	public float movedowntime;
	public float startime;
	public float speed;

	// Override
	void Start () {

		oldY = transform.position.y;
		Invoke ("startAnimating", startime);

	}
	
	// Override
	void Update () {
		if (transform.position.y - oldY > movedist) {
			Vector3 v = transform.position;
			v.y = oldY + movedist;	
			transform.position = v;
			stop ();
		}
		else if (transform.position.y < oldY) {
			Destroy(gameObject);
		}
	}

	void startAnimating(){
		this.rigidbody2D.velocity = new Vector3 (0, speed);
	}


	void stop(){
		rigidbody2D.velocity = Vector3.zero;
		Invoke ("down",stilltime);
	}

	void down(){
		rigidbody2D.velocity = new Vector3 (0, -speed / 20);
	}
	
}
