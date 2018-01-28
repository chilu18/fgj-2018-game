using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverText : MonoBehaviour {

	public GameObject virologistWonText;
	public GameObject patientZerotWonText;
	public GameObject[] otherObjectsToShow;
	private bool gameHasEnded = false;

	public void ShowGameOverTexts(bool virologistWon) {
		gameHasEnded = true;
		if (virologistWon) {
			virologistWonText.SetActive (true);
		} else {
			patientZerotWonText.SetActive (true);
		}

		foreach (GameObject go in otherObjectsToShow) {
			go.SetActive (true);
		}
	}

	void Update() {
		if (!gameHasEnded)
			return;

		if (Input.GetKeyDown (KeyCode.Space) ||
			Input.GetKeyDown (KeyCode.KeypadEnter)) {
			SceneManager.LoadScene ("Menu", LoadSceneMode.Single);
			Time.timeScale = 1;
		}
	}
}
