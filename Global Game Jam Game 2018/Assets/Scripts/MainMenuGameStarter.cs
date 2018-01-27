using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuGameStarter : MonoBehaviour {

	public GameObject selectUserMenu;
	public GameObject pressAnyKeyMenu;
	public MenuButton virologistSelected;
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
		if (Input.GetKeyDown (KeyCode.LeftArrow) ||
			Input.GetKeyDown (KeyCode.RightArrow) ||
			Input.GetKeyDown (KeyCode.UpArrow) ||
			Input.GetKeyDown (KeyCode.DownArrow)) {
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
}
