using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[HideInInspector]
public class MapCube : MonoBehaviour {

	public GameObject turretBuilt;	//current cube's turret prefab

	public void BuildTurret(GameObject turretGO){
		turretBuilt = GameObject.Instantiate(turretGO,transform.position,Quaternion.identity);
	}

	void UpgradeTurret(){

	}
}
