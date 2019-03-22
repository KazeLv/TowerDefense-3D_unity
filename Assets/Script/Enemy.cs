using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float hp;
	public float speed;
	private Transform[] roadPoints;
	private int posIndex;

	public GameObject hurtEffect;

    // Use this for initialization
    void Start () {
		roadPoints=GameController.instance.GetRoadPoints();
		posIndex=0;
	}
	
	// Update is called once per frame
	void Update () {
		Move();

	}

	void OnDestroy(){
		//Update the count of enemy belonging to this wave
		GameController.enemyAlive--;
	}

	void Move(){
		if(posIndex>=roadPoints.Length) return;
		transform.Translate((roadPoints[posIndex].position-transform.position).normalized*Time.deltaTime*speed);
		if(Vector3.Distance(roadPoints[posIndex].position,transform.position)<0.1f) posIndex++;
		if(posIndex>=roadPoints.Length) ArriveAtTerminal();
	}

	void Dead(){
		GameObject.Destroy(this.gameObject);		//destroy gameobject
		
	}

	void ArriveAtTerminal(){
		GameObject.Destroy(this.gameObject);
	}

	public void Hurt(int damage){
		hp -= damage;
	}

}
