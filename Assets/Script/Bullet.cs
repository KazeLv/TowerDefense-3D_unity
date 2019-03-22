using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public int damage = 50;
	public float speed = 1f;

	private Transform targetTrans;

	public void SetTarget(Transform t){
		targetTrans = t;
	}

	void Update () {
		transform.LookAt(targetTrans);
		transform.Translate(Vector3.forward*speed*Time.deltaTime);
	}

	void OnTriggerEnter(Collider col){
		if(col.tag=="Enmey"){
			col.GetComponent<Enemy>().Hurt(damage);
			GameObject.Destroy(gameObject);
		}
	}

}
