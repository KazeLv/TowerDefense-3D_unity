using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController instance;

	public Transform[] roadPoints;

	//Enemy spawner variables
	public static int enemyAlive = 0;
	public OffensiveWave[] OffensiveWaves;
	public Transform trans_start;
	public float waveInterval;

	//Turret build variables
	public Animator moneyAnimator;
	public GameObject buildEffect;
	private TurretData turretSelected;
	public TurretData laserBeamerData;
	public TurretData missileLauncherData;
	public TurretData standardTurretData;

	//UI variables
	private int money = 1000;
	public Text moneyText;
	public Text laserCostText;
	public Text missileCostText;
	public Text turretCostText;
	

	// Use this for initialization
	void Start () {
		instance = this;
		StartCoroutine(SpawnEnemy());

		//UI data init
		moneyText.text = "Coins:"+money;
		laserCostText.text = "cost:"+laserBeamerData.cost;
		missileCostText.text = "cost:"+missileLauncherData.cost;
		turretCostText.text = "cost:"+standardTurretData.cost;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			if(!EventSystem.current.IsPointerOverGameObject()){
				//Turret build control
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				bool isCollider = Physics.Raycast(ray,out hit,1000,LayerMask.GetMask("MapCube"));
				if(isCollider&&turretSelected!=null){
					GameObject cubeHit = hit.collider.gameObject;
					MapCube mapCube = cubeHit.GetComponent<MapCube>();
					if(mapCube.turretBuilt==null){
						if(money>=turretSelected.cost){
							//Build the turret selected
							mapCube.BuildTurret(turretSelected.turretPrefab);
							GameObject.Instantiate(buildEffect,cubeHit.transform.position,Quaternion.identity);
							ChangeMoney(-turretSelected.cost);
						}else{
							//Tell palyer the money is not enough
							moneyAnimator.SetTrigger("flicker");
						}
					}else{
						//Upgrade or change a new turret
					}
				}
			}
		}
	}

    public Transform[] GetRoadPoints()
    {
        return roadPoints;
    }
	
	IEnumerator SpawnEnemy(){
		foreach(OffensiveWave wave in OffensiveWaves){
			for(int i=0;i<wave.enemyCount;i++){
				GameObject.Instantiate(wave.enemyPrefab,trans_start.position,Quaternion.identity);
				enemyAlive++;
				if(i!=wave.enemyCount-1){
					yield return new WaitForSeconds(wave.interval);
				}
			}
			while(enemyAlive>0) yield return 0;
			yield return new WaitForSeconds(waveInterval);
		}	
	}

	//Turret build methods
	public void OnLaserBeamerSelected(bool clicked){
		if(clicked){
			turretSelected = laserBeamerData;
		}
	}

	public void OnMissileLauncherSelected(bool clicked){
		if(clicked){
			turretSelected = missileLauncherData;
		}
	}

	public void OnStandardTurretSelected(bool clicked){
		if(clicked){
			turretSelected = standardTurretData;
		}
	}

	void ChangeMoney(int change){
		money+=change;
		moneyText.text = "Coins:"+money;
	}
}
