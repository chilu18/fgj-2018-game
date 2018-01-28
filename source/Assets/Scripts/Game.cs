using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game {

	public static Game instance = new Game();
	public bool isPlayingPatientZero = true;
	public bool isPlayingVirologist = false;
	public bool hasStartedOnce = false;

	public static void VirologistWin () {
		Debug.Log ("VIROLOGIST WIN!");
		GameObject.FindObjectOfType<GameOverText> ().ShowGameOverTexts (true);
		Game.EndGame ();
	}

	public static void PatientZeroWin () {
		Debug.Log ("PATIENT ZERO WIN!");
		GameObject.FindObjectOfType<GameOverText> ().ShowGameOverTexts (false);
		Game.EndGame ();
	}

	private static void EndGame() {
		Time.timeScale = 0;
		GameObject.FindObjectOfType<Virologist> ().enabled = false;
		GameObject.FindObjectOfType<PatientZero> ().enabled = false;
	}
}
