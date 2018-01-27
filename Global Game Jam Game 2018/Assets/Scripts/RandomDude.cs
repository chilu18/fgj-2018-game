using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomDude : MonoBehaviour {

	public Material infectedMaterial;
	public Material disinfectedMaterial;
	private Vector3 direction = Vector3.zero;
	private float speed = 1.5f;
	private Vector3 currentWalkTarget;

	private Vector3[] lookoutViewRayRelativePoints = new Vector3[] {
		new Vector3(0,0,0),
		new Vector3(0.5f,0,0),
		new Vector3(-0.5f,0,0)
	};
	private float lookoutDistance = 4;
	private NavMeshAgent agent;


	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent> ();
		LookupPlaceOfInterest ();
	}
	
	// Update is called once per frame
	void Update () {
		this.LookoutForInfectedPeople ();
		AreWeThereYet ();
	}

	void AreWeThereYet() {
		if (Mathf.Abs(this.transform.position.magnitude - currentWalkTarget.magnitude) < 0.05) {
			LookupPlaceOfInterest ();
		}
	}

	void LookupPlaceOfInterest() {
		do {
			currentWalkTarget = new Vector3 (Random.Range (-14, 14), 0.5f, Random.Range (-8, 8));
		} while(Physics.CheckSphere (currentWalkTarget, 0.4f));
		agent.SetDestination (currentWalkTarget);
	}

	void LookoutForInfectedPeople() {
		Vector3 currentDirection = transform.TransformDirection (Vector3.forward);
		foreach(Vector3 point in lookoutViewRayRelativePoints) {
			Ray ray = new Ray (transform.position, currentDirection + transform.TransformDirection (point));
			Debug.DrawRay (ray.origin, ray.direction * lookoutDistance, Color.blue);

			RaycastHit hit;
			Physics.Raycast (ray, out hit, lookoutDistance);

			if(hit.collider != null) {
				if (this.IsInfected () && hit.collider.GetComponent<RandomDude> () && !hit.collider.GetComponent<RandomDude> ().IsInfected ()) {
					Vector3 eatDirection = hit.point - transform.position;
					Debug.DrawRay (transform.position, eatDirection, Color.black);
					this.direction = eatDirection;
					currentWalkTarget = hit.point;
					agent.SetDestination (hit.point);
				} else {
					Vector3 escapeDirection = transform.position - hit.point;
					Debug.DrawRay (transform.position, escapeDirection, Color.red);
					this.direction = escapeDirection;
				}
			}
		}
	}

	public void Infect () {
		this.GetComponent<Renderer> ().material = infectedMaterial;
	}

	public void DisInfect() {
		this.GetComponent<Renderer> ().material = disinfectedMaterial;
	}

	public bool IsInfected() {
		return this.GetComponent<Renderer>().sharedMaterial == infectedMaterial;
	}

	void OnCollisionStay(Collision c) {
		this.direction = c.impulse;
		this.direction.Normalize ();
	}



	void OnCollisionEnter(Collision c) {
		if (!this.IsInfected() || !c.gameObject.GetComponent<RandomDude> ()) {
			return;
		}

		RandomDude other = c.gameObject.GetComponent<RandomDude> ();

		other.Infect ();
		LookupPlaceOfInterest ();
		Game.CheckForWin ();
	}
}
