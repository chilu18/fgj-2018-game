using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour {

	private GameObject followPoint;

	// Use this for initialization
	void Start () {
		followPoint = GameObject.FindGameObjectWithTag ("PatientZeroCameraFollowPoint");
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Lerp(transform.rotation, followPoint.transform.rotation, Time.deltaTime * 5f);
		transform.position = Vector3.Lerp(transform.position, followPoint.transform.position, Time.deltaTime * 5f);
	}
}
