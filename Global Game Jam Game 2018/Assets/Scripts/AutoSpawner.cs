using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpawner : MonoBehaviour {

	public int howMuch = 1;
	public Transform what;
	public float personalSpace = 0.8f;

	void Start () {
		PointOfInterest[] pois = GameObject.FindObjectsOfType<PointOfInterest> ();
		for (int i = 0; i < howMuch; i++) {
			Transform newThing = GameObject.Instantiate<Transform>(this.what);
			newThing.parent = this.transform;

			Vector3 newPosition = GetRandomSpawnPoint ();
			newThing.position = newPosition;
		}
	}

	Vector3 GetRandomSpawnPoint() {
		PointOfInterest[] pois = GameObject.FindObjectsOfType<PointOfInterest> ();
		int i = 0;
		Vector3 goodPosition;

		do {
			PointOfInterest randomPoi = pois [Random.Range (0, pois.Length)];
			float spread = randomPoi.spread;
			goodPosition = randomPoi.transform.position +
				Vector3.forward * BiasedRandom(-spread,spread) +
				Vector3.right * BiasedRandom(-spread,spread);

		} while (Physics.CheckSphere(goodPosition, personalSpace) && i++ < 1000);

		return goodPosition;
	}

	float BiasedRandom(float from, float to) {
		return Random.Range(from,to) * Random.Range(0.0f,1.0f);
	}
}
