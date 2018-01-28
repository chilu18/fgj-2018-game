using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Virologist : MonoBehaviour {

	private float speed = 0.1f;
	private float rotationSpeed = 3.5f;

	private Vector3 direction = new Vector3();
		
	public bool isMoving = false;
	public Animator animator;

	public bool isOnAutoplay = false;
	public NavMeshAgent agent;
	public NavMeshObstacle obstacle;

	void Awake() {
		this.agent.enabled = !Game.instance.isPlayingVirologist;
		this.obstacle.enabled = Game.instance.isPlayingVirologist;
	}

	// Use this for initialization
	void Start () {
		animator.SetBool("isZombie", false);
	}

	void FixedUpdate() {
		if (Game.instance.isPlayingVirologist) {
			this.GetComponent<Rigidbody> ().velocity = speed * direction;
		} else {
			this.animator.SetFloat ("speed", 1);
		}
	}

	private void GoToSafehouse() {
		Safehouse[] safehouses = FindObjectsOfType<Safehouse> ();
		Safehouse safehouse = safehouses [Random.Range (0, safehouses.Length)];
		agent.SetDestination (safehouse.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (!Game.instance.isPlayingVirologist) {
			GoToSafehouse ();
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
		if (c.gameObject.GetComponent<Safehouse> ()) {
			Game.VirologistWin ();
		} else if (c.gameObject.GetComponent<RandomDude> ()) {
			if (c.gameObject.GetComponent<RandomDude> ().IsInfected ()) {
				Game.PatientZeroWin ();
			}
		} else if (c.gameObject.GetComponent<PatientZero> ()) {
			Game.PatientZeroWin ();
		}
	}
}
