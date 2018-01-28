﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomDude : MonoBehaviour {


	public Material[] infectedMaterials;
	public Material[] disinfectedMaterials;
	public Renderer renderer;
	public Animator animator;
	public Vector3 currentWalkTarget;
	public int wtFrom;
	public int dudeType = 0;
	public Transform bloodPartice;
	public Transform sweatParticle;

	private Vector3[] lookoutViewRayRelativePoints = new Vector3[] {
		new Vector3(0,0,0),
		new Vector3(0.1f,0,0),
		new Vector3(0.3f,0,0),
		new Vector3(0.5f,0,0),
		new Vector3(0.7f,0,0),
		new Vector3(-0.7f,0,0),
		new Vector3(-0.5f,0,0),
		new Vector3(-0.3f,0,0),
		new Vector3(-0.1f,0,0),
	};
	private float lookoutDistance = 10;
	private NavMeshAgent agent;
	private bool stayingCool = false;


	// Use this for initialization
	void Start () {
		dudeType = Random.Range (0, 4);
		agent = gameObject.GetComponent<NavMeshAgent> ();
		LookupPlaceOfInterest ();
		this.animator.SetFloat ("speed", 1);
		this.animator.SetBool ("isZombie", false);
		DisInfect ();
	}
	
	// Update is called once per frame
	void Update () {
		this.LookoutForInfectedPeople ();
		AreWeThereYet ();
	}

	void AreWeThereYet() {
		if (Mathf.Abs(this.transform.position.magnitude - currentWalkTarget.magnitude) < 1) {
			LookupPlaceOfInterest ();
		}
	}

	void LookupPlaceOfInterest() {
		PointOfInterest[] pois = GameObject.FindObjectsOfType<PointOfInterest> ();
		PointOfInterest selected = pois [Random.Range (0, pois.Length)];
		wtFrom = 1;
		currentWalkTarget = selected.transform.position;
		agent.SetDestination (currentWalkTarget);
	}

	void LookoutForInfectedPeople() {
		Vector3 currentDirection = transform.TransformDirection (Vector3.forward);
		foreach(Vector3 point in lookoutViewRayRelativePoints) {
			Ray ray = new Ray (transform.position + currentDirection * 0.55f, currentDirection + transform.TransformDirection (point));
			Debug.DrawRay (ray.origin, ray.direction * lookoutDistance, Color.blue);

			RaycastHit hit;
			Physics.Raycast (ray, out hit, lookoutDistance);

			if(hit.collider != null) {
				Collider c = hit.collider;
				bool targetIsPatientZero = c.GetComponent<PatientZero> ();
				bool targetIsRandomDude = c.GetComponent<RandomDude> ();
				bool targetIsVirologist = c.GetComponent<Virologist> ();
				bool targetIsInfected = targetIsRandomDude && c.GetComponent<RandomDude> ().IsInfected ();

				bool wantToEatTarget = this.IsInfected () &&
				                       (
				                           (targetIsRandomDude && !targetIsPatientZero && !targetIsInfected) ||
				                           targetIsVirologist
				                       );
				bool wantToEscsapeTarget = !this.IsInfected () && !targetIsPatientZero && targetIsInfected;

				if (wantToEatTarget) {
					if (!stayingCool) {
						currentWalkTarget = hit.collider.transform.position;
						agent.SetDestination (hit.collider.transform.position);
						StayCool ();
					}
				} else if(wantToEscsapeTarget) {
					GoToSafehouse ();
					FreakOut ();
				}
			}
		}
	}

	private IEnumerator StayCool() {
		this.stayingCool = true;
		yield return new WaitForSeconds(1.0f);
		this.stayingCool = false;
	}

	private void FreakOut() {
		SetSpeed (2.0f);
		Transform sweat = Instantiate<Transform> (sweatParticle);
		sweat.transform.position = transform.position;
	}

	private void GoToSafehouse() {
		Safehouse[] safehouses = FindObjectsOfType<Safehouse> ();
		Safehouse safehouse = safehouses [Random.Range (0, safehouses.Length)];
		currentWalkTarget = safehouse.transform.position;
		agent.SetDestination (safehouse.transform.position);
	}

	public void Infect () {
		this.renderer.material = infectedMaterials[dudeType];
		this.animator.SetBool ("isZombie", true);
		LookupPlaceOfInterest ();
		SetSpeed (Game.instance.isPlayingVirologist ? 3.0f : 0.3f);
		this.agent.radius = 0.49f;

		Transform blood = Instantiate<Transform> (bloodPartice);
		blood.transform.position = transform.position;
	}

	public void DisInfect() {
		this.renderer.material = disinfectedMaterials[dudeType];
		this.animator.SetBool ("isZombie", false);
	}

	void SetSpeed(float newSpeed) {
		this.agent.speed = newSpeed * 3.5f;
		this.animator.speed = newSpeed;
	}

	public bool IsInfected() {
		return this.animator.GetBool ("isZombie");
	}

	void OnCollisionEnter(Collision c) {
		if (!this.IsInfected() || !c.gameObject.GetComponent<RandomDude> ()) {
			return;
		}

		RandomDude other = c.gameObject.GetComponent<RandomDude> ();
		if (!other.IsInfected ()) {
			other.Infect ();
		}
		LookupPlaceOfInterest ();
	}

	public void TeleportToSafety() {
		GameObject.Destroy (gameObject);
	}
}