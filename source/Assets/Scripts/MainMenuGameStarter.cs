using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuGameStarter : MonoBehaviour {

	public GameObject selectUserMenu;
	public GameObject pressAnyKeyMenu;
	public MenuButton virologistSelected;
	public Image vignette;
	bool isOnSelectUserMenu = false;
	
	// Update is called once per frame
	void Update () {
		if (!isOnSelectUserMenu) {
			CheckPressAnyArrowKey ();
		} else {
			CheckEnterOrSpacebarPressed ();
		}
	}

	void CheckPressAnyArrowKey() {
		if (Game.instance.hasStartedOnce ||
			Input.GetKeyDown (KeyCode.LeftArrow) ||
			Input.GetKeyDown (KeyCode.RightArrow) ||
			Input.GetKeyDown (KeyCode.UpArrow) ||
			Input.GetKeyDown (KeyCode.DownArrow)) {
			Game.instance.hasStartedOnce = true;
			isOnSelectUserMenu = true;
			selectUserMenu.SetActive (true);
			pressAnyKeyMenu.SetActive (false);
		}
	}

	void CheckEnterOrSpacebarPressed() {
		if (Input.GetKeyDown (KeyCode.Space) ||
			Input.GetKeyDown (KeyCode.KeypadEnter)) {
			if (virologistSelected.isDown) {
				Game.instance.isPlayingPatientZero = false;
				Game.instance.isPlayingVirologist = true;
			} else {
				Game.instance.isPlayingPatientZero = true;
				Game.instance.isPlayingVirologist = false;
			}
			SceneManager.LoadScene("Game Scene", LoadSceneMode.Single);
		}
	}

	public void UpdateVignette(Sprite sprite) {
		vignette.sprite = sprite;
	}
}
