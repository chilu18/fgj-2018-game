using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomDude : MonoBehaviour {

	public Material infectedMaterial;
	public Material disinfectedMaterial;
	public Renderer renderer;
	public Animator animator;
	private Vector3 currentWalkTarget;

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
		agent = gameObject.GetComponent<NavMeshAgent> ();
		LookupPlaceOfInterest ();
		this.animator.SetFloat ("speed", 1);
		this.animator.SetBool ("isZombie", false);
	}
	
	// Update is called once per frame
	void Update () {
		this.LookoutForInfectedPeople ();
		AreWeThereYet ();
	}

	void AreWeThereYet() {
		if (Mathf.Abs(this.transform.position.magnitude - currentWalkTarget.magnitude) < 0.5f * 0.5f) {
			LookupPlaceOfInterest ();
		}
	}

	void LookupPlaceOfInterest() {
		PointOfInterest[] pois = GameObject.FindObjectsOfType<PointOfInterest> ();
		PointOfInterest selected = pois [Random.Range (0, pois.Length)];
		currentWalkTarget = selected.transform.position;
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
				Collider c = hit.collider;
				bool targetIsPatientZero = c.GetComponent<PatientZero> ();
				bool targetIsInfected = c.GetComponent<RandomDude> () && c.GetComponent<RandomDude> ().IsInfected ();

				bool wantToEatTarget = this.IsInfected () && !targetIsPatientZero && !targetIsInfected;
				bool wantToEscsapeTarget = !this.IsInfected () && !targetIsPatientZero && targetIsInfected;

				if (wantToEatTarget) {
					currentWalkTarget = hit.point;
					agent.SetDestination (hit.point);
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
	}

	private void GoToSafehouse() {
		Safehouse safehouse = FindObjectOfType<Safehouse> ();
		currentWalkTarget = safehouse.transform.position;
		agent.SetDestination (safehouse.transform.position);
	}

	public void Infect () {
		this.renderer.material = infectedMaterial;
		this.animator.SetBool ("isZombie", true);
		LookupPlaceOfInterest ();
		SetSpeed (0.7f);
	}

	public void DisInfect() {
		this.renderer.material = disinfectedMaterial;
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

		other.Infect ();
		LookupPlaceOfInterest ();
		Game.CheckForWin ();
	}

	public void TeleportToSafety() {
		GameObject.Destroy (gameObject);
	}
}
