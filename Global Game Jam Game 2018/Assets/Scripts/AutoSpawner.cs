using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpawner : MonoBehaviour {

	public int howMuch = 1;
	public Vector3 spread = new Vector3(1,0,1);
	public Transform what;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < howMuch; i++) {
			Transform newThing = GameObject.Instantiate<Transform>(this.what);
			Vector3 newPosition = transform.position;
			newPosition.x += Random.Range (-spread.x, spread.x);
			newPosition.y += Random.Range (-spread.y, spread.y);
			newPosition.z += Random.Range (-spread.z, spread.z);
			newThing.position = newPosition;
		}
		GameObject.Destroy (gameObject);
	}
}
