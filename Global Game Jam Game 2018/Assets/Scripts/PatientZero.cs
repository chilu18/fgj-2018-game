﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatientZero : MonoBehaviour {

	private float speed = 0.1f;
	private float rotationSpeed = 3.5f;

	private Vector3 direction = new Vector3();
		
	public bool isMoving = false;
	public Animator animator;

	public bool isOnAutoplay = false;
	public NavMeshAgent agent;

	void Awake() {
		this.agent.enabled = !Game.instance.isPlayingPatientZero;
	}

	// Use this for initialization
	void Start () {
		animator.SetBool("isZombie", true);
	}

	void FixedUpdate() {
		if (Game.instance.isPlayingPatientZero) {
			this.GetComponent<Rigidbody> ().velocity = speed * direction;
		}
	}

	void WalkToClosestHuman() {
		RandomDude[] possibleHumans = FindObjectsOfType<RandomDude> ();
		RandomDude closestHuman = null;
		foreach (RandomDude possibleHuman in possibleHumans) {
			if (possibleHuman.IsInfected ())
				continue;
			
			if (!closestHuman ||
			   Vector3.Distance (possibleHuman.transform.position, transform.position) < Vector3.Distance (closestHuman.transform.position, transform.position)) {
				closestHuman = possibleHuman;
			}
		}

		agent.SetDestination (closestHuman.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (!Game.instance.isPlayingPatientZero) {
			WalkToClosestHuman ();
			return;
		}
		
		isMoving = false;
		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.Translate (Vector3.forward * -speed);
			isMoving = true;
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			transform.Translate (Vector3.forward * speed);
			isMoving = true;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Rotate (Vector3.up * -rotationSpeed);
			isMoving = true;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Rotate (Vector3.up * rotationSpeed);
			isMoving = true;
		}
		animator.SetFloat("speed", isMoving == false ? 0.0f : 1.0f);

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
