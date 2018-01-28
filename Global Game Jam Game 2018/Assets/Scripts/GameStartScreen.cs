using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartScreen : MonoBehaviour {

	public Text virologistText;
	public Text patientZeroText;
	public Virologist virologist;
	public PatientZero patientZero;

	void Awake() {
		virologistText.enabled = Game.instance.isPlayingVirologist;
		patientZeroText.enabled = Game.instance.isPlayingPatientZero;
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.UpArrow) ||
		    Input.GetKeyDown (KeyCode.DownArrow) ||
		    Input.GetKeyDown (KeyCode.LeftArrow) ||
		    Input.GetKeyDown (KeyCode.RightArrow)) {

			patientZero.enabled = true;
			virologist.enabled = true;
			patientZero.GetComponent<CapsuleCollider> ().enabled = true;
			Destroy (gameObject);
		}
	}
}
