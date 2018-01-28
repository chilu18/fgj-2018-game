using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour {

	public GameObject followPoint;
	public bool isVirologist;
	public bool isPatientZero;

	void Awake () {
		this.gameObject.SetActive (
			(isPatientZero && Game.instance.isPlayingPatientZero) ||
			(isVirologist && Game.instance.isPlayingVirologist)
		);
	}

	// Use this for initialization
	void Start () {
		transform.SetParent (null);
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Lerp(transform.rotation, followPoint.transform.rotation, Time.deltaTime * 5f);
		transform.position = Vector3.Lerp(transform.position, followPoint.transform.position, Time.deltaTime * 5f);
	}
}
