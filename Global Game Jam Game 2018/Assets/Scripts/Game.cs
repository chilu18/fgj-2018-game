using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game {

	public static Game instance = new Game();
	public bool isPlayingPatientZero = true;
	public bool isPlayingVirologist = false;

	public static void CheckForWin () {
		foreach (RandomDude dude in GameObject.FindObjectsOfType<RandomDude>()) {
			if (!dude.IsInfected ())
				return;
		}

		Debug.Log ("WIN!");
		Time.timeScale = 0;
	}


}
