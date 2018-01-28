using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

	public KeyCode positiveKey;
	public KeyCode negativeKey;
	public GameObject disableThis;
	public Text greyThis;
	public bool isDown = false;
	public MainMenuGameStarter gameStarter;
	public Sprite vignetteSprite;

	void Start() {
		if (isDown)
			Enable ();
		else
			Disable ();
	}

	void Update () {
		if (Input.GetKeyDown (negativeKey)) {
			Disable ();
		} else if (Input.GetKeyDown (positiveKey)) {
			Enable ();
			gameStarter.UpdateVignette (vignetteSprite);
		}
	}

	void Disable() {
		Color currentColor = greyThis.color;
		currentColor.a = 0.5f;
		greyThis.color = currentColor;

		disableThis.SetActive (false);
		isDown = false;
	}

	void Enable() {
		Color currentColor = greyThis.color;
		currentColor.a = 1.0f;
		greyThis.color = currentColor;

		disableThis.SetActive (true);
		isDown = true;
	}
}
