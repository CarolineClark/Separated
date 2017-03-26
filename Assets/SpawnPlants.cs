using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlants : MonoBehaviour {

	private IEnumerator coroutine;

	void Start()
	{
		print ("Starting " + Time.time);

		coroutine = WaitAndGrowPlant(2.0f);
		StartCoroutine(coroutine);

		print ("Before WaitAndPrint Finishes " + Time.time);
	}
		
	private IEnumerator WaitAndGrowPlant(float waitTime) {
		while (true) {
			yield return new WaitForSeconds(waitTime);
			CreatePlant ();
		}
	}

	private void CreatePlant() {
//		GameObject go = (GameObject)Instantiate(Resources.Load("Obstacle"));
//		go.transform.position = transform.position;
	}
}
