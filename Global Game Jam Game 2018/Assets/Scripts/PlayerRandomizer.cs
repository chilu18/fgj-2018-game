using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRandomizer : MonoBehaviour {

	public bool high = true;

	void Awake() {
		PointOfInterest[] pois = FindObjectsOfType<PointOfInterest> ();
		PointOfInterest target = pois[Random.Range(high ? 0 : pois.Length/2, high ? pois.Length/2 : pois.Length)];
		transform.position = target.transform.position;
	}
}
