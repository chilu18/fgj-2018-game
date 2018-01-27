using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfectionCounter : MonoBehaviour {

	void Update () {
		int infected = 0;
		int notInfected = 0;
		int totalPersons = FindObjectOfType<AutoSpawner> ().howMuch;
		RandomDude[] dudes = GameObject.FindObjectsOfType<RandomDude> ();
		foreach (RandomDude dude in dudes) {
			if (dude.IsInfected ()) {
				infected++;	
			}
		}

		string text = "People infected: " + infected + "/" + dudes.Length;
		if (dudes.Length < totalPersons) {
			int saved = totalPersons - dudes.Length;
			text += "\n(" + saved + ") people saved";
		}
		this.GetComponent<Text> ().text = text;
	}
}
