using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : MonoBehaviour {

	public float spread = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, spread);
	}
}
