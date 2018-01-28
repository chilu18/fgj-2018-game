using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveForever : MonoBehaviour {

	public string uniqid = "";
	// Use this for initialization
	void Start () {
		foreach (LiveForever maybeMe in FindObjectsOfType<LiveForever> ()) {
			if (uniqid == maybeMe.uniqid && maybeMe != this) {
				Destroy (gameObject);
			}
		}
		Object.DontDestroyOnLoad (gameObject);
	}

}
