using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfectionCounter : MonoBehaviour {
	public Image vignette;
	public Sprite patientZeroVignette;
	public Sprite virologistVignette;

	private string colorString;

	void Start () {
		if (Game.instance.isPlayingVirologist) {
			vignette.sprite = virologistVignette;
			colorString = "74d7d4";
		} else {
			vignette.sprite = patientZeroVignette;
			colorString = "ce0201";
		}
	}

	void Update () {
		float infected = 0f;
		float totalPersons = FindObjectOfType<AutoSpawner> ().howMuch;

		RandomDude[] dudes = GameObject.FindObjectsOfType<RandomDude> ();
		foreach (RandomDude dude in dudes) {
			if (dude.IsInfected ()) {
				infected++;	
			}
		}

		float saved = totalPersons - dudes.Length;
		float infectedPercentage = Mathf.Round(infected / totalPersons * 100);
		string escapedAmount = saved.ToString();
		this.GetComponent<Text> ().text = "INFECTION RATE <size=30><color=#" + colorString + ">" + infectedPercentage.ToString() + "%</color></size>\n<size=30><color=#" + colorString + ">" + escapedAmount.ToString() + "</color></size> ESCAPED";
	}
}
