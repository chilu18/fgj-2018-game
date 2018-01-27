using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game {

	public static Game instance = new Game();
	public bool isPlayingPatientZero = true;
	public bool isPlayingVirologist = false;

	public static void VirologistWin () {
		Debug.Log ("VIROLOGIST WIN!");
		Time.timeScale = 0;
	}

	public static void PatientZeroWin () {
		Debug.Log ("PATIENT ZERO WIN!");
		Time.timeScale = 0;
	}
}
