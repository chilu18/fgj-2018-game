using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safehouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.GetComponent<RandomDude> ()) {
			RandomDude dude = c.gameObject.GetComponent<RandomDude> ();
			if (dude.IsInfected ())
				return;
			dude.TeleportToSafety ();
		}
	}
}
