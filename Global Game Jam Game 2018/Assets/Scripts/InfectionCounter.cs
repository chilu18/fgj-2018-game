using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfectionCounter : MonoBehaviour {

	void Update () {
		int infected = 0;
		int notInfected = 0;
		RandomDude[] dudes = GameObject.FindObjectsOfType<RandomDude> ();
		foreach (RandomDude dude in dudes) {
			if (dude.IsInfected ()) {
				infected++;	
			}
		}

		this.GetComponent<Text> ().text = "People infected: " + infected + "/" + dudes.Length;
	}
}
