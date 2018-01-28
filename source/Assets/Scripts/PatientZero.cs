using System.Collections;
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

	private RandomDude[] allRandomDudes;

	void Awake() {
		this.agent.enabled = !Game.instance.isPlayingPatientZero;
		this.GetComponent<AudioListener>().enabled = Game.instance.isPlayingPatientZero;
	}

	// Use this for initialization
	void Start () {
		animator.SetBool("isZombie", true);
		allRandomDudes = FindObjectsOfType<RandomDude> ();
	}

	void FixedUpdate() {
		if (Game.instance.isPlayingPatientZero) {
			this.GetComponent<Rigidbody> ().velocity = speed * direction;
		} else {
			this.animator.SetFloat ("speed", 1);
		}
	}

	void WalkToClosestHuman() {
		RandomDude[] possibleHumans = allRandomDudes;
		RandomDude closestHuman = null;
		foreach (RandomDude possibleHuman in possibleHumans) {
			if (possibleHuman == null || possibleHuman.IsInfected ())
				continue;
			
			if (!closestHuman ||
			   Vector3.Distance (possibleHuman.transform.position, transform.position) < Vector3.Distance (closestHuman.transform.position, transform.position)) {
				closestHuman = possibleHuman;
			}
		}

		Virologist virologist = FindObjectOfType<Virologist> ();
		if (closestHuman == null || Vector3.Distance (virologist.transform.position, transform.position) < Vector3.Distance (closestHuman.transform.position, transform.position)) {
			agent.SetDestination (virologist.transform.position);
		} else {
			agent.SetDestination (closestHuman.transform.position);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!Game.instance.isPlayingPatientZero) {
			WalkToClosestHuman ();
			return;
		}
		
		isMoving = false;
		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.Translate (Vector3.forward * -speed * Time.deltaTime * 30);
			isMoving = true;
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			transform.Translate (Vector3.forward * speed * Time.deltaTime * 30);
			isMoving = true;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Rotate (Vector3.up * -rotationSpeed * Time.deltaTime * 30);
			isMoving = true;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime * 30);
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
	}
}
