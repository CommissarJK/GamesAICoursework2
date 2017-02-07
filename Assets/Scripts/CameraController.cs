using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	int scrollCounter = 0;
	bool scrollingUp = false;
	bool scrollingDown = false;

	void FixedUpdate () {
		//keyboard input
		if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow)){
			transform.position += new Vector3 (0f, 0f, (0.8f*transform.rotation.x));		}
		if (Input.GetKey("a")|| Input.GetKey(KeyCode.LeftArrow)){
			transform.Translate((-0.8f*transform.rotation.x),0f,0f);
		}
		if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow)){
			transform.position += new Vector3 (0f, 0f, (-0.8f*transform.rotation.x));
		}
		if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow)){
			transform.Translate((0.8f*transform.rotation.x),0f,0f);
		}

		//mouse scrolling
		if (Input.GetAxis("Mouse ScrollWheel") > 0f){
			scrollCounter = 20;
			scrollingUp = true;
			scrollingDown = false;
		} else if (Input.GetAxis("Mouse ScrollWheel") < 0f){
			scrollCounter = 20;
			scrollingUp = false;
			scrollingDown = true;
		}

		if (scrollCounter > 0) {
			scrollCounter--;
		} else {
			scrollingUp = false;
			scrollingDown = false;
		}
			
		if (scrollingUp){
			if (transform.rotation.x >= 0.38) {
				transform.Translate (0f, 0f, 0.3f);
				transform.Rotate (-0.5f, 0f, 0f);
			}
		} else if (scrollingDown){
			if (transform.rotation.x <= 0.55) {
				transform.Translate (0f, 0f, -0.3f);
				transform.Rotate (0.5f, 0f, 0f);
			}
		}
		//Debug.Log (transform.rotation.x);

	}
}
