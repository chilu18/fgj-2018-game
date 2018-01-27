using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Safehouse[] shelters = FindObjectsOfType<Safehouse> ();
		int saveThis = Random.Range (0, shelters.Length);
		int i = 0;
		foreach (Safehouse shelter in shelters) {
			if (i != saveThis) {
				Destroy (shelter.gameObject);
			}
			i++;
		}
	}
}
