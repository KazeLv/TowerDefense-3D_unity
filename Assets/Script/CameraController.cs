using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float speedMove;
	public float speedScroll;
	private float btnH;
	private float btnV;
	private float scroll;
	
	// Update is called once per frame
	void Update () {
		btnH = Input.GetAxis("Horizontal");
		btnV = Input.GetAxis("Vertical");
		scroll = Input.GetAxis("Mouse ScrollWheel");

		transform.Translate(new Vector3(btnH,0,btnV)*speedMove,Space.World);
		transform.Translate(new Vector3(0,0,scroll)*speedScroll,Space.Self);
	}
}
