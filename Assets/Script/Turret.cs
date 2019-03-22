using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
	public List<GameObject> enemyList = new List<GameObject>();

	public GameObject ammunitionPre;
	public Transform firePos;

	private float timer = 0;
	public int attackPeriod = 1;

	void Start(){
		timer = attackPeriod;
	}

	void Update(){
		timer+=Time.deltaTime;
		if(timer>=attackPeriod&&enemyList.Count>0){
			timer = 0f;
			Fire();
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.tag=="Enemy"){
			enemyList.Add(col.gameObject);
		}
	}

	void OnTriggerExit(Collider col){
		if(col.tag=="Enemy"){
			enemyList.Add(col.gameObject);
		}
	}

	void Fire(){
		GameObject go = GameObject.Instantiate(ammunitionPre,firePos.position,Quaternion.identity);
		go.GetComponent<Bullet>().SetTarget(enemyList[0].transform);
	}
}
