using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipAudio : MonoBehaviour {

	public AudioClip sound;

	void Awake (){
		playAlienSound ();
	}

	public void playAlienSound(){
		if (sound != null) {
			SoundManager.instance.Loop(sound);
		}
	}
}
