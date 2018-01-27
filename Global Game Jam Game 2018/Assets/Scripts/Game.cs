using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game {
	public static void CheckForWin () {
		foreach (RandomDude dude in GameObject.FindObjectsOfType<RandomDude>()) {
			if (!dude.IsInfected ())
				return;
		}

		Debug.Log ("WIN!");
		Time.timeScale = 0;
	}
}
