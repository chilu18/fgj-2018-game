using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {
	public float waitTime = 1.0f;

	IEnumerator Start () {
		yield return new WaitForSeconds(waitTime);
		Destroy (gameObject);
	}
}
