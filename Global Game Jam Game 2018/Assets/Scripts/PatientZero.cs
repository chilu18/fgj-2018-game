﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientZero : MonoBehaviour {

	private float speed = 3;

	private Vector3 direction = new Vector3();

	public Animator animator;

	// Use this for initialization
	void Start () {
		animator.SetBool("isZombie", true);
	}

	void FixedUpdate() {
		this.GetComponent<Rigidbody>().velocity = speed * direction;
	}
	
	// Update is called once per frame
	void Update () {
		this.direction = new Vector3(0,0,0);
		if (Input.GetKey (KeyCode.DownArrow)) {
			this.direction.z -= 1;
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			this.direction.z += 1;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			this.direction.x -= 1;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			this.direction.x += 1;
		}
		animator.SetFloat("speed", this.direction == Vector3.zero ? 0.0f : 1.0f);

		Vector3 currentDirection = this.transform.TransformDirection (Vector3.forward);
		Vector3 wantedDirection = this.direction;
		this.transform.LookAt (this.transform.position + currentDirection + ((wantedDirection - currentDirection) * 0.2f));
	}

	void OnCollisionEnter(Collision c) {
		if (!c.gameObject.GetComponent<RandomDude> ()) {
			return;
		}

		InfectPoorGuy (c.gameObject.GetComponent<RandomDude> ());
	}

	void InfectPoorGuy(RandomDude other) {
		other.Infect ();
		Game.CheckForWin ();
	}
}
