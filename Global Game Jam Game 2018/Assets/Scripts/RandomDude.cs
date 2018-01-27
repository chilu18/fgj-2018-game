using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDude : MonoBehaviour {

	public Material infectedMaterial;
	public Material disinfectedMaterial;
	private Vector3 direction = Vector3.zero;
	private float speed = 1.5f;

	private Vector3[] lookoutViewRayRelativePoints = new Vector3[] {
		new Vector3(0,0,0),
		new Vector3(0.5f,0,0),
		new Vector3(-0.5f,0,0)
	};
	private float lookoutDistance = 4;


	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate() {
		this.GetComponent<Rigidbody>().velocity = speed * transform.TransformDirection(Vector3.forward);
	}
	
	// Update is called once per frame
	void Update () {
		this.Poopoile ();
		this.LookoutForInfectedPeople ();
		this.RotateABitToCorrectDirection ();
	}

	void RotateABitToCorrectDirection() {
		Vector3 wantedDirection = this.direction;
		Vector3 currentDirection = transform.TransformDirection(Vector3.forward);
		Vector3 slowRotate = currentDirection + ((wantedDirection - currentDirection) * .1f);
		this.transform.LookAt (this.transform.position + slowRotate);
	}

	void Poopoile() {
		this.direction.x += Random.Range (-.2f, .2f);
		this.direction.z += Random.Range (-.2f, .2f);
		this.direction.Normalize ();
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
		Game.CheckForWin ();
	}
}
