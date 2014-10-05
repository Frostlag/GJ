using UnityEngine;
using System.Collections;

public class Earth : MonoBehaviour {

	float oldY;

	public float stilltime;
	public float movedist;
	public float movedowntime;
	public float startime;
	public float speed;

	void Start () {

		oldY = transform.position.y;
		Invoke ("StartAnimating", startime);

	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y - oldY > movedist) {
			Vector3 v = transform.position;
			v.y = oldY + movedist;	
			transform.position = v;
			Stop ();
		}
		else if (transform.position.y < oldY) {
			Destroy(gameObject);
		}
	}

	void StartAnimating(){
		this.rigidbody2D.velocity = new Vector3 (0, speed);
	}


	void Stop(){
		rigidbody2D.velocity = Vector3.zero;
		Invoke ("Down",stilltime);
	}

	void Down(){
		rigidbody2D.velocity = new Vector3 (0, -speed / 20);
	}
	
}
