using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public Transform[] spawnLocations;
	public GameObject[] whatToSpawnPrefab;
	public GameObject[] whatToSpawnClone;
	public bool spawning;
	public float spawnDelay; 

	void Start(){
		StartCoroutine("waitThreeSeconds");
	}


	void startSpawning(){

		whatToSpawnClone[0] = Instantiate(whatToSpawnPrefab[0], spawnLocations[0].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
		
	}

	IEnumerator waitThreeSeconds(){
		while (spawning){
			yield return new WaitForSeconds(spawnDelay);
			startSpawning();
		}

	}
}
