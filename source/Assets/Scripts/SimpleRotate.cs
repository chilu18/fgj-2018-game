﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour {
	void Update () {
		transform.Rotate (new Vector3(0,0,5f) * 30 * Time.deltaTime);
	}
}
