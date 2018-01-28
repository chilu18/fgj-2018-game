using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDudeRadomMusicPlayer : MonoBehaviour {

	public RandomDude main;
	public AudioSource audioSource;

	public AudioClip[] infectedClips;
	public AudioClip[] scaredClips;
	public AudioClip[] eatingClips;
	
	void Start() {
		PlaySomeRandomSoundClipAfterSomeTime ();
	}

	void PlaySomeRandomSoundClipAfterSomeTime () {
		Invoke ("PlaySomeRandomSoundClip", Random.Range (0.0f, 5.0f));
	}

	void PlaySomeRandomSoundClip() {
		if (main.IsInfected ()) {
			AudioClip randomClip = infectedClips [Random.Range (0, infectedClips.Length)];
			audioSource.PlayOneShot (randomClip);
			audioSource.pitch = Random.Range (0.5f, 2.5f);
		}

		PlaySomeRandomSoundClipAfterSomeTime ();
	}

	public void PlayScaredClip() {
		AudioClip randomClip = scaredClips [Random.Range (0, scaredClips.Length)];
		audioSource.PlayOneShot (randomClip);
		audioSource.pitch = Random.Range (0.8f, 1.5f);
	}

	public void PlayMinchingSound() {
		AudioClip randomClip = eatingClips [Random.Range (0, eatingClips.Length)];
		audioSource.PlayOneShot (randomClip);
		audioSource.pitch = Random.Range (0.8f, 1.5f);
	}
}
